using System;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Web;
using Elmah;
using Portolo.Framework.Exception;
using Portolo.Framework.Utils;
using ApplicationException = System.ApplicationException;

namespace Portolo.Framework.Common
{
    public class ServiceInvoker : IServiceInvoker
    {
        private static readonly ChannelFactoryManager FactoryManager = new ChannelFactoryManager();

        public TResponse InvokeService<T, TResponse>(Func<T, TResponse> invokeHandler)
            where T : class
        {
            // TODO (brett) - Replace with a real IOC container
            var nonWcfTypes = new[]
            {
                new { Name="Portolo.DataNormalization.IDataNormalization", Impl = "Portolo.DataNormalization.DataNormalization" },
                new { Name="Portolo.POV.IPOV", Impl = "Portolo.POV.POV" },
                new { Name="Portolo.DataQuality.IDataQuality", Impl="Portolo.DataQuality.DataQuality" },
                new { Name="Portolo.Pricing.IPricingService", Impl="Portolo.Pricing.PricingService" },
                new { Name="Portolo.Procurement.IProcurement", Impl="Portolo.Procurement.Procurement" },
                new { Name="Portolo.RoutingGuide.IRoutingGuide", Impl="Portolo.RoutingGuide.RoutingGuide" },
                new { Name="Portolo.MasterSettings.IMasterSettings", Impl="Portolo.MasterSettings.MasterSettings" },
                new { Name="Portolo.Shipment.IShipment", Impl="Portolo.Shipment.Shipment" },
                new { Name="Portolo.Security.ISecurity", Impl= "Portolo.Security.Security" },
                new { Name="Portolo.OCClaims.IOCClaims", Impl= "Portolo.OCClaims.OCClaims" },
                new { Name="Portolo.Primary.IPrimaryService", Impl= "Portolo.Primary.PrimaryService" },
                new { Name="Portolo.Claims.IClaims", Impl= "Portolo.Claims.Claims" },
            };

            var localImpl = nonWcfTypes.FirstOrDefault(type => type.Name.Equals(typeof(T).ToString(), StringComparison.OrdinalIgnoreCase));
            if (localImpl != null)
            {
                var handle = Activator.CreateInstance(AppDomain.CurrentDomain, typeof(T).Assembly.ToString(), localImpl.Impl);

                return invokeHandler((T)handle.Unwrap());
            }

            TResponse response = default;
            var services = ConfigurationManager.GetSection("ServicesConfigurationSection") as ServicesConfigurationSection;
            var endpointElement = services.IfNotNull(s => s.Services.Where(sev => sev.Contract == typeof(T).ToString()).FirstOrDefault());
            if (endpointElement == null)
            {
                this.CreateServiceException(ref response, null, null, "Endpoint element, " + typeof(T) + ", not found in web.config");
                return response;
            }

            var userName = endpointElement.DefaultEmail;
            var password = endpointElement.DefaultPassword;
            var dbConnection = endpointElement.DefaultDbConnection;
            dynamic user = HttpContext.Current.User;

            if (HttpContext.Current.User.IfNotNull(u => u.Identity.IsAuthenticated) && !endpointElement.SingleInstance)
            {
                if (endpointElement.ApplicationLogin)
                {
                    userName = new UserDbConnectionController().CreateUserConnection(user.Email,
                                                                                     user.Password,
                                                                                     user.DbConnection,
                                                                                     user.OwnerID,
                                                                                     user.ServiceAccess);
                }
                else
                {
                    userName = user.Email;
                }

                password = user.Password;
            }
            else
            {
                if (endpointElement.ApplicationLogin)
                {
                    if (string.IsNullOrEmpty(dbConnection))
                    {
                        this.CreateServiceException(ref response, null, null, "defaultdbconnection can't be blank in web.config");
                        return response;
                    }

                    if (endpointElement.ServiceModuleId == 0)
                    {
                        this.CreateServiceException(ref response, null, null, "servicemoduleid must be define in web.config");
                        return response;
                    }

                    userName = new UserDbConnectionController().CreateUserConnection(userName,
                                                                                     password,
                                                                                     dbConnection,
                                                                                     0,
                                                                                     endpointElement.ServiceModuleId.ToString());
                }
            }

            var arg = FactoryManager.CreateChannel<T>(userName, password, endpointElement);
            var communicationObject = (ICommunicationObject)arg;
            try
            {
                response = invokeHandler(arg);
                communicationObject.Close();
            }
            catch (FaultException<ServiceFault> e)
            {
                this.CreateServiceException(ref response, communicationObject, new ApplicationException(e.Detail.Message), e.Detail.Message);
            }
            catch (CommunicationException e)
            {
                this.CreateServiceException(ref response, communicationObject, e, "Communication Exception");
            }
            catch (TimeoutException e)
            {
                this.CreateServiceException(ref response, communicationObject, e, "Timeout Exception");
            }
            catch (System.Exception e)
            {
                this.CreateServiceException(ref response, communicationObject, e, "Unhandle Exception");
            }

            return response;
        }

        public void CreateServiceException<TResponse>(ref TResponse response, ICommunicationObject communicationObject, System.Exception ex, string displayMessage)
        {
            if (communicationObject != null)
            {
                communicationObject.Abort();
            }

            response = Activator.CreateInstance<TResponse>();
            var responseStatus = response.GetType().GetProperties().FirstOrDefault(p => p.Name == "Status");
            responseStatus.SetValue(response, ResponseStatus.Error);
            var responseMessage = response.GetType().GetProperties().FirstOrDefault(p => p.Name == "Message");
            responseMessage.SetValue(response, displayMessage);
            if (ex == null)
            {
                ex = new System.Exception(displayMessage);
            }

            ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }
}

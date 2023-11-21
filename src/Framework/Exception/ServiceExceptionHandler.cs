using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Portolo.Framework.Log;

namespace Portolo.Framework.Exception
{
    public class ServiceExceptionHandler<T> : IErrorHandler, IServiceBehavior
    {
        public bool HandleError(System.Exception exception)
        {
            if (exception != null)
            {
                var errorConfig = (ErrorLogConfiguration)ConfigurationManager.GetSection("ErrorLogConfiguration");
                return true;
            }

            return true;
        }

        public void ProvideFault(System.Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
            {
                return;
            }

            var businessFault = (T)Activator.CreateInstance(typeof(T), error);
            var faultEx = new FaultException<T>(businessFault, new FaultReason(string.Format("nVision Service Error: {0}", error)));
            var msgFault = faultEx.CreateMessageFault();
            fault = Message.CreateMessage(version, msgFault, faultEx.Action);
        }

        public void AddBindingParameters(ServiceDescription serviceDescription,
                                         ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Bind the error handler to the service behavior.
        /// </summary>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errHandler = new ServiceExceptionHandler<T>();

            foreach (var dispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var dispatcher = dispatcherBase as ChannelDispatcher;
                if (dispatcher == null)
                {
                    continue;
                }

                dispatcher.ErrorHandlers.Add(errHandler);
            }
        }

        /// <summary>
        /// Validate whether all operation contracts have the FaultContracts defined.
        /// </summary>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var svcEndPoint in serviceDescription.Endpoints)
            {
                // Don't check mex
                if (svcEndPoint.Contract.Name.ToLower() != "imetadataexchange")
                {
                    foreach (var opDesc in svcEndPoint.Contract.Operations)
                    {
                        // Operation contract has no faults associated with them
                        if (opDesc.Faults.Count == 0)
                        {
                            var msg = string.Format("BaseErrorHandler behavior requires a FaultContract(typeof(" + typeof(T).Name + "))" +
                                                    " on each operation contract. The {0} contains no FaultContracts.",
                                                    opDesc.Name);

                            throw new InvalidOperationException(msg);
                        }
                    }
                }
            }
        }
    }
}
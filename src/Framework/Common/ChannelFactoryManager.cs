using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Portolo.Framework.Common
{
    public class ChannelFactoryManager
    {
        private static readonly Dictionary<string, ChannelFactory> Factories = new Dictionary<string, ChannelFactory>();
        private static readonly object SyncRoot = new object();

        public virtual T CreateChannel<T>(string username, string password, ServiceConfigurationElement endpointElement)
            where T : class
        {
            var local = this.GetFactory<T>(username, password, endpointElement).CreateChannel();
            return local;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual ChannelFactory<T> GetFactory<T>(string username, string password, ServiceConfigurationElement endpointElement)
            where T : class
        {
            lock (SyncRoot)
            {
                var key = string.Format("{0}", typeof(T).FullName);
                if (endpointElement.EnableBinding == Bindings.wsHttpBinding)
                {
                    key = string.Format("{0}-{1}-{2}", typeof(T).FullName, username, password);
                }

                ChannelFactory factory;
                if (!Factories.TryGetValue(key, out factory))
                {
                    factory = this.CreateFactoryInstance<T>(username, password, endpointElement);
                    Factories.Add(key, factory);
                }

                return factory as ChannelFactory<T>;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (SyncRoot)
                {
                    foreach (var type in Factories.Keys)
                    {
                        var factory = Factories[type];
                        try
                        {
                            factory.Close();
                        }
                        catch
                        {
                            factory.Abort();
                        }
                    }

                    Factories.Clear();
                }
            }
        }

        private ChannelFactory CreateFactoryInstance<T>(string username, string password, ServiceConfigurationElement endpointElement)
        {
            ChannelFactory factory = null;
            switch (endpointElement.EnableBinding)
            {
                case Bindings.basicHttpBinding:
                    factory = ServiceChannelConfig<T>.RegisterChannelBasicHttpBinding(endpointElement.EndPointAddress);
                    break;
                case Bindings.netTcpBinding:
                    factory = ServiceChannelConfig<T>.RegisterChannelNetTcpBinding(endpointElement.EndPointAddress);
                    break;
                default:
                    factory = ServiceChannelConfig<T>.RegisterChannelWSHttpBinding(endpointElement.EndPointAddress,
                                                                                   username,
                                                                                   password,
                                                                                   endpointElement.DnsIdentifier);
                    break;
            }

            factory.Open();
            return factory;
        }

        private void ChannelFaulted(object sender, EventArgs e)
        {
            var channel = (IClientChannel)sender;
            try
            {
                channel.Close();
            }
            catch
            {
                channel.Abort();
            }

            throw new ApplicationException("Exc_ChannelFailure");
        }

        private void FactoryFaulted(object sender, EventArgs args)
        {
            var factory = (ChannelFactory)sender;
            try
            {
                factory.Close();
            }
            catch
            {
                factory.Abort();
            }

            throw new ApplicationException("Exc_ChannelFactoryFailure");
        }
    }
}
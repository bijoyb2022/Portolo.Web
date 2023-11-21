using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;

namespace Portolo.Framework.Common
{
    public class ServiceChannelConfig<T>
    {
        /// <summary>
        /// Register a new Channel Factory using the BasicHttpBinding.
        /// </summary>
        /// <param name="endPointAddress">The endpoint address of the service.</param>
        /// <param name="maxReceivedMessageSize">The maximum size, in bytes, for a message that can be received on the new channel.</param>
        /// <param name="maxDepth">The maximum nested node depth per read.</param>
        /// <param name="maxArrayLength">
        /// The maximum allowed array length of data being received by Windows Communication
        /// Foundation (WCF) from a client.
        /// </param>
        /// <returns>A Factory that creates channels of the. <T> type.</returns>
        public static ChannelFactory<T> RegisterChannelBasicHttpBinding(string endPointAddress,
                                                                        long maxReceivedMessageSize = 2147483647,
                                                                        int maxDepth = 32,
                                                                        int maxArrayLength = 16384)
        {
            var binding = new BasicHttpBinding();

            binding.MaxReceivedMessageSize = maxReceivedMessageSize;
            binding.ReaderQuotas.MaxDepth = maxDepth;
            binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
            binding.SendTimeout = new TimeSpan(0, 10, 30);
            var security = new BasicHttpSecurity();
            var transport = new HttpTransportSecurity();
            var message = new BasicHttpMessageSecurity();

            security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

            transport.ClientCredentialType = HttpClientCredentialType.Windows;
            transport.ProxyCredentialType = HttpProxyCredentialType.None;
            transport.Realm = string.Empty;

            message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            message.AlgorithmSuite = SecurityAlgorithmSuite.Default;

            security.Transport = transport;
            security.Message = message;
            binding.Security = security;
            return new ChannelFactory<T>(binding, new EndpointAddress(new Uri(endPointAddress)));
        }

        /// <summary>
        /// Register a new Channel Factory using the BasicHttpBinding.
        /// </summary>
        /// <param name="endPointAddress">The endpoint address of the service.</param>
        /// <param name="maxReceivedMessageSize">The maximum size, in bytes, for a message that can be received on the new channel.</param>
        /// <param name="maxDepth">The maximum nested node depth per read.</param>
        /// <param name="maxArrayLength">
        /// The maximum allowed array length of data being received by Windows Communication
        /// Foundation (WCF) from a client.
        /// </param>
        /// <returns>A Factory that creates channels of the. <T> type.</returns>
        public static ChannelFactory<T> RegisterChannelBasicHttpBinding(string endPointAddress,
                                                                        TimeSpan sendTimeout,
                                                                        long maxReceivedMessageSize = 2147483647,
                                                                        int maxDepth = 32,
                                                                        int maxArrayLength = 16384)
        {
            var binding = new BasicHttpBinding();

            binding.MaxReceivedMessageSize = maxReceivedMessageSize;
            binding.ReaderQuotas.MaxDepth = maxDepth;
            binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
            binding.SendTimeout = sendTimeout;

            return new ChannelFactory<T>(binding, new EndpointAddress(new Uri(endPointAddress)));
        }

        /// <summary>
        /// Register a new Channel Factory using the WsHttpBinding.
        /// </summary>
        /// <param name="endPointAddress">The endpoint address of the service.</param>
        /// <param name="maxReceivedMessageSize">The maximum size, in bytes, for a message that can be received on the new channel.</param>
        /// <param name="maxDepth">The maximum nested node depth per read.</param>
        /// <param name="maxArrayLength">
        /// The maximum allowed array length of data being received by Windows Communication
        /// Foundation (WCF) from a client.
        /// </param>
        /// <returns>A Factory that creates channels of the. <T> type.</returns>
        public static ChannelFactory<T> RegisterChannelWSHttpBinding(string endPointAddress,
                                                                     long maxReceivedMessageSize = 2147483647,
                                                                     int maxDepth = 32,
                                                                     int maxArrayLength = 16384)
        {
            var binding = new WSHttpBinding(SecurityMode.None);

            binding.MaxReceivedMessageSize = maxReceivedMessageSize;
            binding.ReaderQuotas.MaxDepth = maxDepth;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.SendTimeout = new TimeSpan(0, 10, 30);
            binding.OpenTimeout = new TimeSpan(0, 10, 30);
            binding.CloseTimeout = new TimeSpan(0, 10, 30);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 30);
            binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.None;
            binding.MessageEncoding = WSMessageEncoding.Mtom;

            return new ChannelFactory<T>(binding, new EndpointAddress(new Uri(endPointAddress)));
        }

        /// <summary>
        /// Register a new Channel Factory using the WsHttpBinding which supports ClientCredentials attributes
        /// Implemntation done to achieve Security using ClientCredential Type as message.
        /// </summary>
        /// <param name="endPointAddress">The endpoint address of the service.</param>
        /// <param name="maxReceivedMessageSize">The maximum size, in bytes, for a message that can be received on the new channel.</param>
        /// <param name="maxDepth">The maximum nested node depth per read.</param>
        /// <param name="maxArrayLength">
        /// The maximum allowed array length of data being received by Windows Communication
        /// Foundation (WCF) from a client.
        /// </param>
        /// <returns>A Factory that creates channels of the. <T> type.</returns>
        public static ChannelFactory<T> RegisterChannelWSHttpBinding(string endPointAddress,
                                                                     string username,
                                                                     string password,
                                                                     string dnsIdentifier,
                                                                     long maxReceivedMessageSize = 2147483647,
                                                                     int maxDepth = 32,
                                                                     int maxArrayLength = 16384)
        {
            var binding = new WSHttpBinding();
            binding.MaxReceivedMessageSize = maxReceivedMessageSize;
            binding.ReaderQuotas.MaxDepth = maxDepth;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.SendTimeout = new TimeSpan(0, 10, 30);
            binding.OpenTimeout = new TimeSpan(0, 10, 30);
            binding.CloseTimeout = new TimeSpan(0, 10, 30);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 30);
            binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.UserName;
            var endPointIdentity = EndpointIdentity.CreateDnsIdentity(dnsIdentifier);
            var remoteAddress = new EndpointAddress(new Uri(endPointAddress), endPointIdentity);

            var factory = new ChannelFactory<T>(binding, remoteAddress);

            factory.Credentials.UserName.UserName = username;
            factory.Credentials.UserName.Password = password;
            factory.Credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
            factory.Credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;

            return factory;
        }

        /// <summary>
        /// Register a new Channel Factory using the WsHttpBinding.
        /// </summary>
        /// <param name="endPointAddress">The endpoint address of the service.</param>
        /// <param name="maxReceivedMessageSize">The maximum size, in bytes, for a message that can be received on the new channel.</param>
        /// <param name="maxDepth">The maximum nested node depth per read.</param>
        /// <param name="maxArrayLength">
        /// The maximum allowed array length of data being received by Windows Communication
        /// Foundation (WCF) from a client.
        /// </param>
        /// <returns>A Factory that creates channels of the. <T> type.</returns>
        public static ChannelFactory<T> RegisterChannelWSHttpBinding(string endPointAddress,
                                                                     TimeSpan sendTimeout,
                                                                     long maxReceivedMessageSize = 2147483647,
                                                                     int maxDepth = 32,
                                                                     int maxArrayLength = 16384)
        {
            var binding = new WSHttpBinding();

            binding.MaxReceivedMessageSize = maxReceivedMessageSize;
            binding.ReaderQuotas.MaxDepth = maxDepth;
            binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
            binding.SendTimeout = sendTimeout;
            binding.Security.Message.ClientCredentialType = MessageCredentialType.Certificate;

            return new ChannelFactory<T>(binding, new EndpointAddress(new Uri(endPointAddress)));
        }

        /// <summary>
        /// Register a new Channel Factory using the NetTcpBinding.
        /// </summary>
        /// <param name="endPointAddress">The endpoint address of the service.</param>
        /// <param name="maxReceivedMessageSize">The maximum size, in bytes, for a message that can be received on the new channel.</param>
        /// <param name="maxDepth">The maximum nested node depth per read.</param>
        /// <param name="maxArrayLength">
        /// The maximum allowed array length of data being received by Windows Communication
        /// Foundation (WCF) from a client.
        /// </param>
        /// <returns>A Factory that creates channels of the. <T> type.</returns>
        public static ChannelFactory<T> RegisterChannelNetTcpBinding(string endPointAddress,
                                                                     long maxReceivedMessageSize = 2147483647,
                                                                     int maxDepth = 32,
                                                                     int maxArrayLength = 16384)
        {
            var binding = new NetTcpBinding();

            binding.MaxReceivedMessageSize = maxReceivedMessageSize;
            binding.ReaderQuotas.MaxDepth = maxDepth;
            binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
            binding.ReaderQuotas.MaxStringContentLength = 2147483647;
            binding.SendTimeout = new TimeSpan(0, 10, 30);
            binding.OpenTimeout = new TimeSpan(0, 10, 30);
            binding.CloseTimeout = new TimeSpan(0, 10, 30);
            binding.ReceiveTimeout = new TimeSpan(0, 10, 30);
            binding.Security.Mode = SecurityMode.None;
            return new ChannelFactory<T>(binding, new EndpointAddress(new Uri(endPointAddress)));
        }

        /// <summary>
        /// Register a new Channel Factory using the NetTcpBinding.
        /// </summary>
        /// <param name="endPointAddress">The endpoint address of the service.</param>
        /// <param name="maxReceivedMessageSize">The maximum size, in bytes, for a message that can be received on the new channel.</param>
        /// <param name="maxDepth">The maximum nested node depth per read.</param>
        /// <param name="maxArrayLength">
        /// The maximum allowed array length of data being received by Windows Communication
        /// Foundation (WCF) from a client.
        /// </param>
        /// <returns>A Factory that creates channels of the. <T> type.</returns>
        public static ChannelFactory<T> RegisterChannelNetTcpBinding(string endPointAddress,
                                                                     TimeSpan sendTimeout,
                                                                     long maxReceivedMessageSize = 2147483647,
                                                                     int maxDepth = 32,
                                                                     int maxArrayLength = 16384)
        {
            var binding = new NetTcpBinding();

            binding.MaxReceivedMessageSize = maxReceivedMessageSize;
            binding.ReaderQuotas.MaxDepth = maxDepth;
            binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
            binding.SendTimeout = sendTimeout;

            return new ChannelFactory<T>(binding, new EndpointAddress(new Uri(endPointAddress)));
        }
    }
}
using System;
using System.ServiceModel;

namespace Portolo.Framework.Common
{
    public class Channel : IDisposable
    {
        private readonly ICommunicationObject channel;

        private Channel(ICommunicationObject channel)
        {
            this.channel = channel;
        }

        public static Channel AsDisposable(object client) => new Channel((ICommunicationObject)client);

        public void Dispose()
        {
            var success = false;

            try
            {
                if (this.channel.State != CommunicationState.Faulted)
                {
                    this.channel.Close();
                    success = true;
                }
            }
            finally
            {
                if (!success)
                {
                    this.channel.Abort();
                }
            }
        }
    }
}
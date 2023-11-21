using System;
using System.ServiceModel.Configuration;

namespace Portolo.Framework.Exception
{
    public class ServiceExceptionHandlerBehavior<T> : BehaviorExtensionElement
    {
        public override Type BehaviorType => typeof(ServiceExceptionHandler<T>);

        protected override object CreateBehavior() => new ServiceExceptionHandler<T>();
    }
}
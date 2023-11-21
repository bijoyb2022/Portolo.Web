using System;

namespace Portolo.Framework.Common
{
    public interface IServiceInvoker
    {
        TResponse InvokeService<T, TResponse>(Func<T, TResponse> invokeHandler)
            where T : class;
    }
}
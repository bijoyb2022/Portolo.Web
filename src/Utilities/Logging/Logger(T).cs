namespace Portolo.Utility.Logging
{
    public class Logger<T> : Logger
    {
        public Logger()
            : base(typeof(T).FullName)
        {
        }
    }
}

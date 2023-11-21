using System;
using Newtonsoft.Json;

namespace Portolo.Framework.Common.Caching
{
    public abstract class CacheWrapper : ICacheWrapper
    {
        public bool IsCacheEnabled { get; } = true;

        public void Set<T>(string key, T objectToCache, TimeSpan? expiry = null)
            where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (this.IsCacheEnabled)
            {
                try
                {
                    var serializedObjectToCache = JsonConvert.SerializeObject(objectToCache,
                                                                              Formatting.Indented,
                                                                              new JsonSerializerSettings
                                                                              {
                                                                                  ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                                                                  PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                                                                  TypeNameHandling = TypeNameHandling.All
                                                                              });

                    this.SetValue(key, serializedObjectToCache, expiry);
                }
                catch (System.Exception e)
                {
                    //ErrorSignal.FromCurrentContext().Raise(new System.Exception(string.Format("Cannot Set {0}", key), e));
                }
            }
        }

        public T Get<T>(string key)
            where T : class
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (this.IsCacheEnabled)
            {
                try
                {
                    var stringObject = this.GetValue(key);
                    if (stringObject == null)
                    {
                        return default;
                    }

                    var obj = JsonConvert.DeserializeObject<T>(stringObject,
                                                               new JsonSerializerSettings
                                                               {
                                                                   ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                                                   PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                                                   TypeNameHandling = TypeNameHandling.All
                                                               });
                    return obj;
                }
                catch (System.Exception e)
                {
                    //ErrorSignal.FromCurrentContext().Raise(new System.Exception(string.Format("Cannot Get {0}", key), e));
                }
            }

            return null;
        }

        public void Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            if (this.IsCacheEnabled)
            {
                try
                {
                    this.DeleteValue(key);
                }
                catch (System.Exception e)
                {
                    //ErrorSignal.FromCurrentContext().Raise(new System.Exception(string.Format("Cannot Delete key {0}", key), e));
                }
            }
        }

        public void DeleteByPattern(string prefixKey)
        {
            if (string.IsNullOrEmpty(prefixKey))
            {
                throw new ArgumentNullException("prefixKey");
            }

            if (this.IsCacheEnabled)
            {
                try
                {
                    this.DeleteValueByPattern(prefixKey);
                }
                catch (System.Exception e)
                {
                    //ErrorSignal.FromCurrentContext().Raise(new System.Exception(string.Format("Cannot DeleteByPattern key {0}", prefixKey), e));
                }
            }
        }

        protected abstract void SetValue(string key, string objectToCache, TimeSpan? expiry = null);
        protected abstract string GetValue(string key);
        protected abstract void DeleteValue(string key);
        protected abstract void DeleteValueByPattern(string prefixKey);
    }
}
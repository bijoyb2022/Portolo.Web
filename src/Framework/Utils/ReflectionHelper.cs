using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.ApplicationInsights;

namespace Portolo.Framework.Utils
{
    public static class ReflectionHelper
    {
        public static IEnumerable<Assembly> LoadAppDomainReferences()
        {
            var finalAssemblies = new List<Assembly>();

            try
            {
                var loadedAssemblies = AppDomain
                    .CurrentDomain
                    .GetAssemblies()
                    .Where(a => a.FullName.StartsWith("TMS"))
                    .ToList();

                var referencedAssemblyNames = loadedAssemblies
                    .SelectMany(a => a.GetReferencedAssemblies())
                    .Where(a => a.FullName.StartsWith("TMS"))
                    .Distinct()
                    .Where(y => loadedAssemblies.All(a => a.FullName != y.FullName))
                    .ToList();

                foreach (var referencedAssemblyName in referencedAssemblyNames)
                {
                    finalAssemblies.Add(AppDomain.CurrentDomain.Load(referencedAssemblyName));
                }

                finalAssemblies.AddRange(loadedAssemblies);
            }
            catch (Exception e)
            {
                var client = new TelemetryClient();

                var rtle = e as ReflectionTypeLoadException;
                if (rtle == null)
                {
                    client.TrackException(e);
                }
                else
                {
                    foreach (var loaderException in rtle.LoaderExceptions)
                    {
                        client.TrackException(loaderException);
                    }
                }
            }

            return finalAssemblies.Where(f => f != null);
        }

        public static IEnumerable<Type> GetAllTypesImplementingOpenGenericType(Type openGenericType, IEnumerable<Type> types)
        {
            foreach (var x in types)
            {
                var y = x.BaseType;
                foreach (var z in x.GetInterfaces())
                {
                    if ((y?.IsGenericType == true && openGenericType.IsAssignableFrom(y.GetGenericTypeDefinition()))
                        || (z.IsGenericType && openGenericType.IsAssignableFrom(z.GetGenericTypeDefinition())))
                    {
                        yield return x;
                    }
                }
            }
        }
    }
}

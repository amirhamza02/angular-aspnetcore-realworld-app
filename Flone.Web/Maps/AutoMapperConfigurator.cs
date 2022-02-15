using AutoMapper;
using System.Reflection;

namespace Flone.Web.Maps
{
    public class AutoMapperConfigurator
    {
        private static readonly object Lock = new object();
        private static MapperConfiguration? _configuration;

        public static MapperConfiguration Configure()
        {
            lock (Lock)
            {
                if (_configuration != null) return _configuration;

                var thisType = typeof(AutoMapperConfigurator);
                var configInterfaceType = typeof(IAutoMapperTypeConfigurator);


                var configurators = thisType.GetTypeInfo().Assembly.GetTypes()
                    .Where(x => !string.IsNullOrWhiteSpace(x.Namespace))
                    .Where(x => x.Namespace.Contains(thisType.Namespace))
                    .Where(x => x.GetTypeInfo().GetInterface(configInterfaceType.Name) != null)
                    .Select(x => (IAutoMapperTypeConfigurator)Activator.CreateInstance(x))
                    .ToArray();

                void AggregatedConfigurator(IMapperConfigurationExpression config)
                {
                    foreach (var configurator in configurators)
                    {
                        configurator.Configure(config);
                    }
                }

                _configuration = new MapperConfiguration(AggregatedConfigurator);
                return _configuration;
            }
        }
    }
}

using PatchKit.Core.DependencyInjection;

namespace PatchKit.Api.Properties
{
    public class AssemblyDependencyInjection
    {
        static AssemblyDependencyInjection()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.AddDependency(Core.Properties.AssemblyDependencyInjection.Container);

            containerBuilder.RegisterType<IBaseApiConnectionFactory, BaseApiConnectionFactory>();
            containerBuilder.RegisterType<IApiConnection, ApiConnection>(ApiConnectionSettings.DefaultApi);
            containerBuilder.RegisterType<IKeysApiConnection, KeysApiConnection>(ApiConnectionSettings.DefaultKeysApi);

            Container = containerBuilder.Container;
        }

        public static Container Container { get; }
    }
}
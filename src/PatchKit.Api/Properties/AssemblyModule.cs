using PatchKit.Core.DependencyInjection;

namespace PatchKit.Api.Properties
{
    public static class AssemblyModule
    {
        static AssemblyModule()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.OverwriteBindings(Core.Properties.AssemblyModule.Container.Bindings);

            containerBuilder.OverwriteBinding<IBaseApiConnectionFactory, BaseApiConnectionFactory>();
            containerBuilder.OverwriteBinding<IApiConnection, ApiConnection>(ApiConnectionSettings.DefaultApi);
            containerBuilder.OverwriteBinding<IKeysApiConnection, KeysApiConnection>(ApiConnectionSettings.DefaultKeysApi);

            Container = containerBuilder.Container;
        }

        public static Container Container { get; }
    }
}
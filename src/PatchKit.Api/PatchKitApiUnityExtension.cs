using Microsoft.Practices.Unity;
using PatchKit.Core;

namespace PatchKit.Api
{
    public class PatchKitApiUnityExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.AddNewExtension<PatchKitCoreUnityExtension>();

            Container.RegisterType<IBaseApiConnectionFactory, BaseApiConnectionFactory>();
            Container.RegisterType<IApiConnection, ApiConnection>(
                new InjectionConstructor(ApiConnectionSettings.DefaultApi));
            Container.RegisterType<IKeysApiConnection, KeysApiConnection>(
                new InjectionConstructor(ApiConnectionSettings.DefaultKeysApi));
        }
    }
}
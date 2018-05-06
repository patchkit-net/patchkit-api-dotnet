using JetBrains.Annotations;
using PatchKit.Logging;
using PatchKit.Network;

namespace PatchKit.Api
{
    /// <summary>
    /// PatchKit Keys Api Connection.
    /// </summary>
    public sealed partial class KeysApiConnection : IKeysApiConnection
    {
        private readonly IBaseApiConnection _baseApiConnection;

        public KeysApiConnection(ApiConnectionSettings settings,
            [NotNull] IBaseApiConnectionFactory baseApiConnectionFactory)
        {
            _baseApiConnection = baseApiConnectionFactory.Create(settings);
        }

        private static void SetPathParam(ref string path, string name, string value)
        {
            path = path.Replace($"{{{name}}}", value);
        }

        private static void SetQueryParam(ref string query, string name, string value)
        {
            if (query != string.Empty)
            {
                query += "&";
            }

            query += $"{name}={value}";
        }
    }
}
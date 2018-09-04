using JetBrains.Annotations;
using PatchKit.Logging;
using PatchKit.Network;

namespace PatchKit.Api
{
    /// <summary>
    /// PatchKit Main Api Connection.
    /// </summary>
    public sealed partial class ApiConnection : IApiConnection
    {
        private readonly IBaseApiConnection _baseApiConnection;

        public ApiConnection(ApiConnectionSettings settings,
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
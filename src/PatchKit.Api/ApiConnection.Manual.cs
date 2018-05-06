using System.Text;
using Newtonsoft.Json;
using PatchKit.Api.Models;
using PatchKit.Core;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Network;

namespace PatchKit.Api
{
    public partial class ApiConnection
    {
        public App PostUserApplication(string apiKey, string name, string platform, Timeout? timeout)
        {
            string path = "/1/apps";
            string query = "api_key=" + apiKey;
            string contentText = $"name={name}&platform={platform}";

            var content = Encoding.ASCII.GetBytes(contentText).ToImmutableArray();

            var response = _baseApiConnection.SendRequest(new ApiPostRequest(path, query, content,
                HttpPostRequestContentType.ApplicationXWWWFormUrlEncoded), timeout);

            return JsonConvert.DeserializeObject<App>(response.Body);
        }
    }
}
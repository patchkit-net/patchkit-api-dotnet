using PatchKit.Api.Models;
using PatchKit.Core;

namespace PatchKit.Api
{
    public partial interface IApiConnection
    {
        App PostUserApplication(string apiKey, string name, string platform, Timeout? timeout);
    }
}
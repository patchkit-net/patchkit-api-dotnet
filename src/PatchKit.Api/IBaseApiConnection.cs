using PatchKit.Core;

namespace PatchKit.Api
{
    public interface IBaseApiConnection
    {
        ApiResponse SendRequest(ApiGetRequest request, Timeout? timeout);

        ApiResponse SendRequest(ApiPostRequest request, Timeout? timeout);
    }
}
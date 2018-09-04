using PatchKit.Core;
using PatchKit.Core.Cancellation;

namespace PatchKit.Api
{
    public interface IBaseApiConnection
    {
        ApiResponse SendRequest(ApiGetRequest request, Timeout? timeout, CancellationToken cancellationToken);

        ApiResponse SendRequest(ApiPostRequest request, Timeout? timeout, CancellationToken cancellationToken);
    }
}
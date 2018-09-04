namespace PatchKit.Api
{
    public interface IApiCache
    {
        void Save(ApiGetRequest request, ApiResponse response);

        ApiResponse? Retrieve(ApiGetRequest request);
    }
}
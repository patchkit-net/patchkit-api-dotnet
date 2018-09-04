using System.Collections.Generic;
using PatchKit.Core;

namespace PatchKit.Api
{
    public class ApiCache : IApiCache
    {
        private readonly Dictionary<ApiGetRequest, ApiResponse> _data = new Dictionary<ApiGetRequest, ApiResponse>();

        public void Save(ApiGetRequest request, ApiResponse response)
        {
            request.ThrowArgumentExceptionIfNotValid(nameof(request));
            response.ThrowArgumentExceptionIfNotValid(nameof(response));

            _data[request] = response;
        }

        public ApiResponse? Retrieve(ApiGetRequest request)
        {
            request.ThrowArgumentExceptionIfNotValid(nameof(request));

            if (!_data.ContainsKey(request))
            {
                return null;
            }

            return _data[request];
        }
    }
}
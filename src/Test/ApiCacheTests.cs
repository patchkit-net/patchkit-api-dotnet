using FluentAssertions;
using NUnit.Framework;
using PatchKit.Api;

namespace Test
{
    public class ApiCacheTests
    {
        [Test]
        public void Retrieve_Returns_Null_For_Missing_Cache_Response()
        {
            var apiCache = new ApiCache();
            apiCache.Retrieve(new ApiGetRequest("a", "b")).Should().BeNull();
        }

        [Test]
        public void Retrieve_Returns_Cached_Response()
        {
            var request = new ApiGetRequest("a", "b");
            var response = new ApiResponse("response");

            var apiCache = new ApiCache();
            apiCache.Save(request, response);

            apiCache.Retrieve(request).Should().Be(response);
        }

        [Test]
        public void Save_Overwrites_Previously_Cached_Response()
        {
            var request = new ApiGetRequest("a", "b");
            var response = new ApiResponse("response");
            var newResponse = new ApiResponse("newresponse");

            var apiCache = new ApiCache();
            apiCache.Save(request, response);
            apiCache.Save(request, newResponse);

            apiCache.Retrieve(request).Should().Be(newResponse);
        }
    }
}
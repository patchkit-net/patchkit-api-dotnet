using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using PatchKit.Api;
using PatchKit.Core.Cancellation;

namespace Test
{
    public class ApiConnectionTest
    {
        private IBaseApiConnection _baseApiConnection;
        private IBaseApiConnectionFactory _baseApiConnectionFactory;
        private ApiConnection _apiConnection;

        [SetUp]
        public void SetUp()
        {
            _baseApiConnection = Substitute.For<IBaseApiConnection>();
            _baseApiConnectionFactory = Substitute.For<IBaseApiConnectionFactory>();

            _baseApiConnectionFactory.Create(Arg.Any<ApiConnectionSettings>()).Returns(_baseApiConnection);

            _apiConnection = new ApiConnection(default, _baseApiConnectionFactory);
        }

        [TestCase(null)]
        [TestCase("PL")]
        [TestCase("UK")]
        public void GetAppVersionContentUrls_Returns_Correct_Result(string country)
        {
            var body = "[{\"url\": \"http://first\", \"meta_url\": \"http://efg1\", \"country\": \"PL\"}, " +
                       "{\"url\": \"http://second\", \"meta_url\": \"http://efg2\"}]";

            var query = country == null ? string.Empty : $"country={country}";

            _baseApiConnection
                .SendRequest(new ApiGetRequest("1/apps/secret/versions/13/content_urls", query), null,
                    CancellationToken.Empty)
                .Returns(new ApiResponse(body));

            var contentUrls =
                _apiConnection.GetAppVersionContentUrls("secret", 13, country, null, CancellationToken.Empty);

            contentUrls.Length.Should().Be(2);
            contentUrls[0].Url.Should().Be("http://first");
            contentUrls[0].MetaUrl.Should().Be("http://efg1");
            contentUrls[0].Country.Should().Be("PL");
            contentUrls[1].Url.Should().Be("http://second");
            contentUrls[1].MetaUrl.Should().Be("http://efg2");
            contentUrls[1].Country.Should().BeNull();
        }

        [TestCase(null)]
        [TestCase("PL")]
        [TestCase("UK")]
        public void GetAppVersionDiffUrls_Returns_Correct_Result(string country)
        {
            var body = "[{\"url\": \"http://first\", \"meta_url\": \"http://efg1\", \"country\": \"PL\"}, " +
                       "{\"url\": \"http://second\", \"meta_url\": \"http://efg2\"}]";

            var query = country == null ? string.Empty : $"country={country}";

            _baseApiConnection
                .SendRequest(new ApiGetRequest("1/apps/secret/versions/13/diff_urls", query), null,
                    CancellationToken.Empty)
                .Returns(new ApiResponse(body));

            var contentUrls =
                _apiConnection.GetAppVersionDiffUrls("secret", 13, country, null, CancellationToken.Empty);

            contentUrls.Length.Should().Be(2);
            contentUrls[0].Url.Should().Be("http://first");
            contentUrls[0].MetaUrl.Should().Be("http://efg1");
            contentUrls[0].Country.Should().Be("PL");
            contentUrls[1].Url.Should().Be("http://second");
            contentUrls[1].MetaUrl.Should().Be("http://efg2");
            contentUrls[1].Country.Should().BeNull();
        }
    }
}
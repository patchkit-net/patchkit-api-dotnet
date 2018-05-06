using PatchKit.Core;

namespace PatchKit.Api
{
    public struct ApiGetRequest : IValidatable
    {
        public string Path { get; }
        public string Query { get; }

        public ApiGetRequest(string path, string query)
        {
            Path = path;
            Query = query;
        }


        public string ValidationError
        {
            get
            {
                if (Path == null)
                {
                    return "Path cannot be null.";
                }

                if (Query == null)
                {
                    return "Query cannot be null.";
                }

                return null;
            }
        }
    }
}
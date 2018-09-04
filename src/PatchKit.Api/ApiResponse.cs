using PatchKit.Core;

namespace PatchKit.Api
{
    public struct ApiResponse : IValidatable
    {
        public ApiResponse(string body)
        {
            Body = body;
        }

        public string Body { get; }

        public string ValidationError
        {
            get
            {
                if (Body == null)
                {
                    return "Body cannot be null.";
                }

                return null;
            }
        }
    }
}

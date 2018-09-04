using System;
using PatchKit.Core;
using PatchKit.Core.Collections.Immutable;
using PatchKit.Network;

namespace PatchKit.Api
{
    public struct ApiPostRequest : IValidatable
    {
        public string Path { get; }
        public string Query { get; }
        public ImmutableArray<byte> Content { get; }
        public HttpPostRequestContentType ContentType { get; }

        public ApiPostRequest(string path, string query, ImmutableArray<byte> content,
            HttpPostRequestContentType contentType)
        {
            Path = path;
            Query = query;
            Content = content;
            ContentType = contentType;
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

                if (!Enum.IsDefined(typeof(HttpPostRequestContentType), ContentType))
                {
                    return "ContentType must be defined enum value.";
                }

                return null;
            }
        }
    }
}
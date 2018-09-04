using PatchKit.Core;

namespace PatchKit.Api
{
    /// <summary>
    /// Describes API server.
    /// </summary>
    public struct ApiConnectionServer : IValidatable
    {
        public ApiConnectionServer(string host, int port, bool useHttps)
        {
            Host = host;
            Port = port;
            UseHttps = useHttps;
        }

        /// <summary>
        /// Server host url.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Port used for connection with server.
        /// </summary>
        public int Port { get; }

        /// <summary>
        /// Set to true to use https instead of http.
        /// </summary>
        public bool UseHttps { get; }

        /*internal int RealPort
        {
            get
            {
                if (Port == 0)
                {
                    return UseHttps ? 443 : 80;
                }
                return Port;
            }
        }*/
        public string ValidationError
        {
            get
            {
                if (string.IsNullOrEmpty(Host))
                {
                    return "Host cannot be null or empty.";
                }

                if (Port < 0)
                {
                    return "Port cannot be less than zero.";
                }

                return null;
            }
        }
    }
}

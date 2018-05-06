using System;
using System.Collections.Generic;
using PatchKit.Core.Collections.Immutable;

namespace PatchKit.Api
{
    /// <summary>
    /// Occurs when there are problems with connection to API.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class ApiConnectionException : Exception
    {
        /// <inheritdoc />
        public ApiConnectionException(Exception mainServerException,
            ImmutableArray<Exception> cacheServersExceptions) : base("Unable to connect to any of the API servers.")
        {
            MainServerException = mainServerException;
            CacheServersExceptions = cacheServersExceptions;
        }

        /// <summary>
        /// Exception that occured during attempt to connect to the main server.
        /// </summary>
        public Exception MainServerException { get; }

        /// <summary>
        /// Exceptions that occured during attempts to connect to the cache servers.
        /// </summary>
        public ImmutableArray<Exception> CacheServersExceptions { get; }

        /// <inheritdoc />
        public override string Message
        {
            get
            {
                var t = base.Message;

                t += "\n" +
                     "Main server exceptions:\n" +
                     MainServerException +
                     "Cache servers exceptions:\n" +
                     ExceptionsToString(CacheServersExceptions);

                return t;
            }
        }

        private static string ExceptionsToString(IEnumerable<Exception> exceptions)
        {
            var result = string.Empty;

            int i = 1;
            foreach (var t in exceptions)
            {
                result += $"{i}. {t}\n";
                i++;
            }

            if (i == 1)
            {
                result = "(none)";
            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Extra.vMixApi
{
    public class vMixApiException : Exception
    {
        public string StatusDescription { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; } = HttpStatusCode.InternalServerError;
        public Exception Error { get; internal set; }

        public vMixApiException(string message):base(message)
        {

        }

        public vMixApiException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}

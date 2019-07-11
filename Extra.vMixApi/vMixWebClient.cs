using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Extra.vMixApi
{
    internal class vMixWebClient : WebClient
    {
        public int Timeout { get; set; }
        /// <summary>
        /// The most recent response statusCode
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }
        /// <summary>
        /// The most recent response statusDescription
        /// </summary>
        public string StatusDescription { get; private set; }
        public string RequestUri { get; private set; }

        public vMixWebClient()
        {
            this.Timeout = 5000;
            this.Encoding = Encoding.UTF8;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = null;
            response = base.GetWebResponse(request);
            HttpWebResponse baseResponse = response as HttpWebResponse;
            StatusCode = baseResponse.StatusCode;
            StatusDescription = baseResponse.StatusDescription;
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = null;
            response = base.GetWebResponse(request);
            HttpWebResponse baseResponse = response as HttpWebResponse;
            StatusCode = baseResponse.StatusCode;
            StatusDescription = baseResponse.StatusDescription;
            return response;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest webRequest = base.GetWebRequest(address);
            webRequest.Timeout = this.Timeout;
            RequestUri = webRequest.RequestUri.ToString();
            return webRequest;
        }
    }
}

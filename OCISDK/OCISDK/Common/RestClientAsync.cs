using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

using Polly;    // Microsoft.Extensions.Http.Polly
using Polly.Wrap;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OCISDK.Common
{
    /// <summary>
    /// rest client async
    /// </summary>
    public class RestClientAsync : IRestClientAsync
    {
        /// <summary>
        /// Signer
        /// </summary>
        public IOciSigner Signer { get; set; }

        /// <summary>
        /// JsonSerializer
        /// </summary>
        public IJsonSerializer JsonSerializer { get; set; }

        /// <summary>
        /// WebRequestPolicy
        /// </summary>
        public IWebRequestPolicy WebRequestPolicy { get; set; }

        /// <summary>
        /// WebProxy
        /// </summary>
        public IWebProxy WebProxy { get; set; }

        /// <summary>
        /// rest option
        /// </summary>
        public RestOption Option { get; set; }
        
        /// <summary>
        /// get
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<WebResponse> Get(HttpWebRequest request)
        {
            var res = await WebRequestPolicy.GetPoliciesAsync(Option).ExecuteAndCaptureAsync(() => request.GetResponseAsync());

            if (res.Outcome == OutcomeType.Failure && (res.FinalException is WebException))
            {
                throw res.FinalException;
            }

            return res.Result;
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public async Task<WebResponse> Get(Uri targetUri)
        {
            return await Get(targetUri, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <returns></returns>
        public async Task<WebResponse> Get(Uri targetUri, HttpRequestHeaderParam httpRequestHeaderParam)
        {
            return await Get(targetUri, httpRequestHeaderParam, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public async Task<WebResponse> Get(Uri targetUri, HttpRequestHeaderParam httpRequestHeaderParam, List<string> fields)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

            var request = (HttpWebRequest)WebRequest.Create(targetUri);

            if (httpRequestHeaderParam != null)
            {
                request = httpRequestHeaderParam.SetHeader(request);
            }

            string body = "";
            if (fields != null && fields.Count != 0)
            {
                body = JsonSerializer.Serialize(fields);
            }

            var bytes = Encoding.UTF8.GetBytes(body);

            if (bytes.Length > 0)
            {
                request.Headers["x-content-sha256"] = Convert.ToBase64String(SHA256.Create().ComputeHash(bytes));
                request.ContentLength = bytes.Length;
            }

            request.Method = HttpMethod.Get.Method;
            request.Accept = "application/json";
            request.ReadWriteTimeout = Option.TimeoutSeconds;

            if (WebProxy != null)
            {
                request.Proxy = WebProxy;
            }

            if (httpRequestHeaderParam != null)
            {
                request = httpRequestHeaderParam.SetHeader(request);
            }

            if (Signer != null)
            {
                Signer.SignRequest(request);
            }

            if (bytes.Length > 0)
            {
                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            var res = await WebRequestPolicy.GetPoliciesAsync(Option).ExecuteAndCaptureAsync(() => request.GetResponseAsync());

            if (res.Outcome == OutcomeType.Failure && (res.FinalException is WebException))
            {
                throw res.FinalException;
            }

            return res.Result;
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public async Task<WebResponse> Post(Uri targetUri)
        {
            return await Post(targetUri, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<WebResponse> Post(Uri targetUri, object requestBody)
        {
            return await Post(targetUri, requestBody, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <returns></returns>
        public async Task<WebResponse> Post(Uri targetUri, object requestBody, HttpRequestHeaderParam httpRequestHeaderParam)
        {
            return await Post(targetUri, requestBody, httpRequestHeaderParam, true);
        }

        /// <summary>
        /// Post a request object to the endpoint represented by the web target and get the response.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <param name="bodyJsonSerialize"></param>
        /// <returns></returns>
        public async Task<WebResponse> Post(Uri targetUri, object requestBody, HttpRequestHeaderParam httpRequestHeaderParam, bool bodyJsonSerialize)
        {
            var request = (HttpWebRequest)WebRequest.Create(targetUri);
            if (httpRequestHeaderParam != null)
            {
                request = httpRequestHeaderParam.SetHeader(request);
            }

            byte[] arr = CreateBodyBytes(request, requestBody, bodyJsonSerialize);
            request.ContentLength = arr.Length;
            request.Headers["x-content-sha256"] = Convert.ToBase64String(SHA256.Create().ComputeHash(arr));

            request.Accept = "*/*";
            request.ContentType = "application/json";
            request.KeepAlive = true;

            if (WebProxy != null)
            {
                request.Proxy = WebProxy;
            }

            request.Method = HttpMethod.Post.Method;

            if (Signer != null)
            {
                Signer.SignRequest(request);
            }

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(arr, 0, arr.Length);
            }

            var res = await WebRequestPolicy.GetPoliciesAsync(Option).ExecuteAndCaptureAsync(() => request.GetResponseAsync());

            if (res.Outcome == OutcomeType.Failure && (res.FinalException is WebException))
            {
                throw res.FinalException;
            }

            return res.Result;
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public async Task<WebResponse> Put(Uri targetUri)
        {
            return await Put(targetUri, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<WebResponse> Put(Uri targetUri, object requestBody)
        {
            return await Put(targetUri, requestBody, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <returns></returns>
        public async Task<WebResponse> Put(Uri targetUri, object requestBody, HttpRequestHeaderParam httpRequestHeaderParam)
        {
            return await Put(targetUri, requestBody, httpRequestHeaderParam, true);
        }

        /// <summary>
        /// Put a request object to the endpoint represented by the web target and get the response.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <param name="bodyJsonSerialize"></param>
        /// <returns></returns>
        public async Task<WebResponse> Put(Uri targetUri, object requestBody, HttpRequestHeaderParam httpRequestHeaderParam, bool bodyJsonSerialize)
        {
            var request = (HttpWebRequest)WebRequest.Create(targetUri);
            if (httpRequestHeaderParam != null)
            {
                request = httpRequestHeaderParam.SetHeader(request);
            }

            byte[] arr = CreateBodyBytes(request, requestBody, bodyJsonSerialize);
            request.ContentLength = arr.Length;
            request.Headers["x-content-sha256"] = Convert.ToBase64String(SHA256.Create().ComputeHash(arr));

            request.ContentType = "*/*";
            request.KeepAlive = true;

            if (WebProxy != null)
            {
                request.Proxy = WebProxy;
            }

            request.Method = HttpMethod.Put.Method;

            if (Signer != null)
            {
                Signer.SignRequest(request);
            }

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(arr, 0, arr.Length);
            }

            var res = await WebRequestPolicy.GetPoliciesAsync(Option).ExecuteAndCaptureAsync(() => request.GetResponseAsync());

            if (res.Outcome == OutcomeType.Failure && (res.FinalException is WebException))
            {
                throw res.FinalException;
            }

            return res.Result;
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public async Task<WebResponse> Patch(Uri targetUri)
        {
            return await Patch(targetUri, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<WebResponse> Patch(Uri targetUri, object requestBody)
        {
            return await Patch(targetUri, requestBody, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <returns></returns>
        public async Task<WebResponse> Patch(Uri targetUri, object requestBody, HttpRequestHeaderParam httpRequestHeaderParam)
        {
            return await Patch(targetUri, requestBody, httpRequestHeaderParam, true);
        }

        /// <summary>
        /// Patch a request object to the endpoint represented by the web target and get the response.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="requestBody"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <param name="bodyJsonSerialize"></param>
        /// <returns></returns>
        public async Task<WebResponse> Patch(Uri targetUri, object requestBody, HttpRequestHeaderParam httpRequestHeaderParam, bool bodyJsonSerialize)
        {
            var request = (HttpWebRequest)WebRequest.Create(targetUri);
            if (httpRequestHeaderParam != null)
            {
                request = httpRequestHeaderParam.SetHeader(request);
            }

            byte[] arr = CreateBodyBytes(request, requestBody, bodyJsonSerialize);
            request.ContentLength = arr.Length;
            request.Headers["x-content-sha256"] = Convert.ToBase64String(SHA256.Create().ComputeHash(arr));

            request.ContentType = "text/plain";
            request.KeepAlive = true;

            if (WebProxy != null)
            {
                request.Proxy = WebProxy;
            }

            request.Method = "Patch";

            if (Signer != null)
            {
                Signer.SignRequest(request);
            }

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(arr, 0, arr.Length);
            }

            var res = await WebRequestPolicy.GetPoliciesAsync(Option).ExecuteAndCaptureAsync(() => request.GetResponseAsync());

            if (res.Outcome == OutcomeType.Failure && (res.FinalException is WebException))
            {
                throw res.FinalException;
            }

            return res.Result;
        }

        /// <summary>
        /// Patch a request object to the endpoint represented by the web target and get the response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="requestBody"></param>
        /// <param name="bodyJsonSerialize"></param>
        /// <returns></returns>
        private byte[] CreateBodyBytes(HttpWebRequest request, object requestBody, bool bodyJsonSerialize)
        {
            string body = "";
            byte[] bytes;
            if (requestBody is Stream)
            {
                bytes = ReadBinaryData(requestBody as Stream);
            }
            else
            {
                if (bodyJsonSerialize)
                {
                    body = JsonSerializer.Serialize(requestBody);
                }
                else
                {
                    body = requestBody as string;
                }

                bytes = Encoding.UTF8.GetBytes(body);
            }

            return bytes;
        }

        static public byte[] ReadBinaryData(Stream st)
        {
            byte[] buf = new byte[32768]; // 一時バッファ

            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = st.Read(buf, 0, buf.Length);

                    if (read > 0)
                    {
                        ms.Write(buf, 0, read);
                    }
                    else
                    {
                        break;
                    }
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public async Task<WebResponse> Delete(Uri targetUri)
        {
            return await Delete(targetUri, null);
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <returns></returns>
        public async Task<WebResponse> Delete(Uri targetUri, HttpRequestHeaderParam httpRequestHeaderParam)
        {
            return await Delete(targetUri, httpRequestHeaderParam, null);
        }

        /// <summary>
        /// Execute a delete on a resource and get the response.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public async Task<WebResponse> Delete(Uri targetUri, HttpRequestHeaderParam httpRequestHeaderParam, object requestBody)
        {
            var request = (HttpWebRequest)WebRequest.Create(targetUri);
            request.Method = HttpMethod.Delete.Method;
            request.ReadWriteTimeout = Option.TimeoutSeconds;

            if (httpRequestHeaderParam != null)
            {
                request = httpRequestHeaderParam.SetHeader(request);
            }

            if (requestBody != null)
            {
                var body = JsonSerializer.Serialize(requestBody);

                var bytes = Encoding.UTF8.GetBytes(body);

                request.Headers["x-content-sha256"] = Convert.ToBase64String(SHA256.Create().ComputeHash(bytes));
                request.ContentLength = bytes.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            if (Signer != null)
            {
                Signer.SignRequest(request);
            }

            var res = await WebRequestPolicy.GetPoliciesAsync(Option).ExecuteAndCaptureAsync(() => request.GetResponseAsync());

            if (res.Outcome == OutcomeType.Failure && (res.FinalException is WebException))
            {
                throw res.FinalException;
            }

            return res.Result;
        }

        /// <summary>
        /// Request a resource asynchronously.
        /// </summary>
        /// <param name="targetUri"></param>
        /// <returns></returns>
        public async Task<WebResponse> Head(Uri targetUri)
        {
            return await Head(targetUri, null);
        }

        /// <summary>
        /// head method
        /// </summary>
        /// <param name="targetUri"></param>
        /// <param name="httpRequestHeaderParam"></param>
        /// <returns></returns>
        public async Task<WebResponse> Head(Uri targetUri, HttpRequestHeaderParam httpRequestHeaderParam)
        {
            var request = (HttpWebRequest)WebRequest.Create(targetUri);
            request.Method = HttpMethod.Get.Method;
            request.Accept = "application/json";
            request.ReadWriteTimeout = Option.TimeoutSeconds;

            if (httpRequestHeaderParam != null)
            {
                request = httpRequestHeaderParam.SetHeader(request);
            }

            if (Signer != null)
            {
                Signer.SignRequest(request);
            }

            var res = await WebRequestPolicy.GetPoliciesAsync(Option).ExecuteAndCaptureAsync(() => request.GetResponseAsync());

            if (res.Outcome == OutcomeType.Failure && (res.FinalException is WebException))
            {
                throw res.FinalException;
            }

            return res.Result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OCISDK.Common;

namespace OCISDK.Test.Common
{
    [TestClass]
    public class HttpRequestHeaderParamTest
    {
        HttpRequestHeaderParam EmptyHttpRequestHeader = new HttpRequestHeaderParam();
        HttpRequestHeaderParam FullHttpRequestHeader = new HttpRequestHeaderParam()
        {
            IfMatch = "testIfMatch",
            IfModifiedSince = "Tue, 10 Mar 2020 15:00:00 GMT",
            IfNoneMatch = "testIfNoneMatch",
            IfUnmodifiedSince = "testIfUnmodifiedSince",
            OpcClientRequestId = "testOpcClientRequestId",
            OpcRequestId = "testOpcRequestId",
            OpcRetryToken = "testOpcRetryToken",
        };

        [TestMethod]
        public void SimpleTest()
        {
            var request = (HttpWebRequest)WebRequest.Create("http://test.com");

            request = FullHttpRequestHeader.SetHeader(request);

            var headers = request.Headers;

            Assert.AreEqual("testIfMatch", headers["if-match"]);
        }

        [DataTestMethod]
        [DataRow("if-match", "testIfMatch")]
        [DataRow("if-modified-since", "Tue, 10 Mar 2020 15:00:00 GMT")]
        [DataRow("if-none-match", "testIfNoneMatch")]
        [DataRow("if-unmodified-since", "testIfUnmodifiedSince")]
        [DataRow("opc-client-request-id", "testOpcClientRequestId")]
        [DataRow("opc-request-id", "testOpcRequestId")]
        [DataRow("opc-retry-token", "testOpcRetryToken")]
        public void TheoryFullPatternTest(string key, string value)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://test.com");

            request = FullHttpRequestHeader.SetHeader(request);

            var headers = request.Headers;

            Assert.AreEqual(value, headers[key]);
        }
    }
}

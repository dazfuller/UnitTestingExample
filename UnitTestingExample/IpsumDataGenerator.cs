using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace UnitTestingExample
{
    public class IpsumDataGenerator
    {
        private readonly IWebRequestCreate _webRequestCreate;
        private static readonly Uri GenerateUri = new Uri("http://www.randomtext.me/api/lorem/p-3/5-15");

        public IpsumDataGenerator() : this(null)
        {
        }

        public IpsumDataGenerator(IWebRequestCreate webRequestCreate)
        {
            _webRequestCreate = webRequestCreate;
        }

        public string Generate()
        {
            var webRequest = _webRequestCreate == null
                ? WebRequest.Create(GenerateUri)
                : _webRequestCreate.Create(GenerateUri);

            webRequest.Proxy = WebRequest.GetSystemWebProxy();
            webRequest.Method = "GET";
            webRequest.Timeout = 1*60*1000;

            HttpWebResponse webResponse;

            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    throw;
                }
                webResponse = (HttpWebResponse)ex.Response;
            }

            if (webResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException("Failed request to API");
            }

            return ParseResponse(webResponse);
        }

        private static string ParseResponse(WebResponse webResponse)
        {
            var stream = webResponse.GetResponseStream();
            if (stream == null) return string.Empty;

            string responseData;

            using (var reader = new StreamReader(stream))
            {
                responseData = reader.ReadToEnd();
            }

            var parsedData = JToken.Parse(responseData);
            return parsedData["text_out"].Value<string>();
        }
    }
}

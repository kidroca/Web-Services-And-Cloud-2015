namespace ConsoleHttpClient
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Queries;

    public class Requesto
    {
        public Requesto(string baseUrl)
        {
            this.BaseUrl = baseUrl;
        }

        public string BaseUrl { get; set; }

        public Query Query { get; set; }

        public async Task<List<Content>> GetContent()
        {
            string response;
            using (var webClient = new WebClient())
            {
                webClient.BaseAddress = this.BaseUrl;
                webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
                webClient.Headers.Add(HttpRequestHeader.UserAgent, "Other");

                var uri = new Uri(this.Query.GetQueryString(), UriKind.Relative);

                response = await webClient.DownloadStringTaskAsync(uri);
            }

            var content = DeserializeObject(response);

            return content;
        }

        private static List<Content> DeserializeObject(string response)
        {
            var jsonObject = JObject.Parse(response);
            var results = jsonObject["results"].Children();

            List<Content> content = new List<Content>();
            foreach (JToken result in results)
            {
                Content next = JsonConvert.DeserializeObject<Content>(result.ToString());
                content.Add(next);
            }

            return content;
        }
    }
}
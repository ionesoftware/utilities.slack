using System.IO;
using System.Net;
using System.Net.Http;
using IONESoftware.Utilities.Slack.Payloads;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IONESoftware.Utilities.Slack
{
    public class SlackWebhook
    {
        private static readonly Formatting DefaultJsonFormatting = Formatting.None;

        private static readonly JsonSerializerSettings DefaultJsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public static void PostMessage(string webookUrl, SlackMessagePayload message)
        {
            var request = WebRequest.CreateHttp(webookUrl);
            request.Method = HttpMethod.Post.ToString();
            using (var requestStream = request.GetRequestStream())
            {
                using (var streamWriter = new StreamWriter(requestStream))
                {
                    var payload = message;
                    streamWriter.Write(JsonConvert.SerializeObject(payload, DefaultJsonFormatting,
                        DefaultJsonSerializerSettings));
                }
            }

            using (request.GetResponse())
            {
                // just send request
            }
        }
    }
}
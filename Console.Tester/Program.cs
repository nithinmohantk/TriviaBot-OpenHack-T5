using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            APITester.SubmitAnswer("e268e894-b242-4c0c-876c-8edba523ca3a", "1035", "4138");

            Console.Read();
        }

    }


    public static class APITester
        {

           static HttpClient httpClient = new HttpClient();

           const string apiEndPoint = "https://msopenhack.azurewebsites.net";

            static APITester()
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "application/json");
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                httpClient.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            } 

            public static async Task  SubmitAnswer(string Id, string questionId, string answerId)
            {
                var sb = new StringBuilder();
                sb.Append("{");
                sb.Append("  \"userId\": \"" + Id + "\",");
                sb.Append("  \"questionId\": \"" + questionId + "\",");
                sb.Append("  \"answerId\": \"" + answerId + "\"");
                sb.Append("}");

                var response = await httpClient.PostAsync(apiEndPoint + "/api/trivia/answer",
                    new StringContent(sb.ToString(), Encoding.UTF8, "application/json"));

               //response.EnsureSuccessStatusCode();

                string respJson = await response.Content.ReadAsStringAsync();

                JObject jObj = JsonConvert.DeserializeObject<JObject>(respJson);
            }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace linebot02.Services;

public class OpenAIService
    {
        private const string OpenAIAPIKey = "";
        const string AzureOpenAIEndpoint = "https://____.openai.azure.com";  //👉replace it with your Azure OpenAI Endpoint
        const string AzureOpenAIModelName = "gpt35"; //👉repleace it with your Azure OpenAI Model Name
        const string AzureOpenAIToken = "040d_____52a0d"; //👉repleace it with your Azure OpenAI Token
        const string AzureOpenAIVersion = "2023-03-15-preview";  //👉replace  it with your Azure OpenAI Model Version

        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum role
        {
            assistant, user, system
        }

        private static string CallAzureOpenAIChatAPI(
            string endpoint, string DeploymentName, string apiKey, string apiVersion, object requestData)
        {
            var client = new HttpClient();

            // 設定 API 網址
            var apiUrl = $"{endpoint}/openai/deployments/{DeploymentName}/chat/completions?api-version={apiVersion}";

            // 設定 HTTP request headers
            client.DefaultRequestHeaders.Add("api-key", apiKey); //👉Azure OpenAI key
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            // 將 requestData 物件序列化成 JSON 字串
            string jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            // 建立 HTTP request 內容
            var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
            // 傳送 HTTP POST request
            var response = client.PostAsync(apiUrl, content).Result;
            // 取得 HTTP response 內容
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
            return obj.choices[0].message.content.Value;
        }

        private static string CallOpenAIChatAPI(string apiKey, object requestData)
        {
            var client = new HttpClient();

            // 設定 API 網址
            var apiUrl = $"https://api.openai.com/v1/chat/completions";

            // 設定 HTTP request headers
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}"); //👉OpenAI key
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header
            // 將 requestData 物件序列化成 JSON 字串
            string jsonRequestData = Newtonsoft.Json.JsonConvert.SerializeObject(requestData);
            // 建立 HTTP request 內容
            var content = new StringContent(jsonRequestData, Encoding.UTF8, "application/json");
            // 傳送 HTTP POST request
            var response = client.PostAsync(apiUrl, content).Result;
            // 取得 HTTP response 內容
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(responseContent);
            return obj.choices[0].message.content.Value;
        }


        public static string getResponseFromGPT(string Message, string OpenAIAPIKey )
        {
            
            return OpenAIService.CallOpenAIChatAPI( OpenAIAPIKey,
                //ref: https://learn.microsoft.com/en-us/azure/cognitive-services/openai/reference#chat-completions
                new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new {
                            role = OpenAIService.role.system ,
//                             content = @"
//                                 假設你的名字叫做'AI液男',是一位已經退伍的替代役役男(非志工)。
//                                 你過去的經歷為:'替代役期間在成功嶺參加231梯次替代役訓練完後
//                                 被分發到台中港南堤消防分隊服消防役，協助正職消防員救護、救災'
//                                 ,現在你已經不做替代役或消防的工作只是游手好閒,但你
//                                 對於民眾非常有禮貌、也能夠安撫民眾的抱怨情緒、
//                                 盡量讓民眾感到被尊重、竭盡所能的回覆民眾的疑問。

//                                 請檢視底下的民眾訊息，以最親切有禮的方式回應。

//                                 但回應時，請注意以下幾點:
//                                 * 不要說 '感謝你的來信' 之類的話，因為民眾是從對談視窗輸入訊息的，不是寫信來的
//                                 * 不能過度承諾
//                                 * 要同理民眾的情緒
//                                 * 要能夠盡量解決民眾的問題
//                                 * 不要以回覆信件的格式書寫，請直接提供對談機器人可以直接堤的回覆
//                                 ----------------------
// "
                            content = @"
                                你是一家名字是夢市集的公司的員工。
                                你待的公司主要是做軟體接案相關的工作。
                                你對於同事非常有禮貌、也能夠安撫同事的抱怨情緒、
                                盡量讓同事感到被尊重、竭盡所能的回覆同事的疑問。

                                請檢視底下的同事訊息，以最親切有禮的方式回應。

                                但回應時，請注意以下幾點:
                                * 不要說 '感謝你的來信' 之類的話，因為同事是從對談視窗輸入訊息的，不是寫信來的
                                * 不能過度承諾
                                * 要同理同事的情緒
                                * 要能夠盡量解決同事的問題
                                * 不要以回覆信件的格式書寫，請直接提供對談機器人可以直接堤的回覆
                                ----------------------
"
                        },
                        new {
                             role = OpenAIService.role.user,
                             content = Message
                        },
                    }
                });
        }
    }
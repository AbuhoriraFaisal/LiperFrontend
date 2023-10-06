using LiperFrontend.Models;
using Newtonsoft.Json;
using NuGet.Common;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LiperFrontend.Shared
{
    public class ApiCaller<T,B>
    {
        static string Base_Url = $"http://75.119.136.238:8016/api/"; // live 
        //static string Base_Url = $"http://207.180.223.113:8026/api/"; // live 
        //static string Base_Url = $"https://mob.jsjbank.com:3000/JSB_OMNI_Ph2/omniServices/"; test 

        //static string Base_Url = $"https://mob.jsjbank.com:8383/JSB_OMNI_Ph2/omniServices/cpServices/"; // live 
       static IHttpClientFactory _httpClientFactory;

        public ApiCaller(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public  static async Task<Tuple<T, string>> callApi(string service, B model)
        {
            var client = _httpClientFactory.CreateClient("apiClient");
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8
                        , "application/json");

            var request = client.PostAsync($"{Base_Url}{service}", content);
            NEPDC();
            string response = await request.Result.Content.ReadAsStringAsync();
            string responseHeaders = request.Result.Headers.ToString();
            string token = responseHeaders.Substring(responseHeaders.IndexOf(':') + 9);
            int endIndex = token.IndexOf($"\r\n");
            token = token.Substring(0, endIndex);
            var responseModel = JsonConvert.DeserializeObject<T>(response);
            return new Tuple<T, string>(responseModel, token);

        }
        public static async Task<Tuple<T, string>> CallApiPost(string service, B model, string authtoken)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8
                        , "application/json");
                    if (string.IsNullOrEmpty(authtoken))
                    {
                        authtoken = Guid.NewGuid().ToString();
                    }
                    httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authtoken);
                    //content.Headers.Add("Bearer", authtoken);
                    var request = httpclient.PostAsync($"{Base_Url}{service}", content);
                    NEPDC();
                    string response = await request.Result.Content.ReadAsStringAsync();
                    string responseHeaders = request.Result.Headers.ToString();
                    string token = responseHeaders.Substring(responseHeaders.IndexOf(':') + 9);
                    int endIndex = token.IndexOf($"\r\n");
                    token = token.Substring(0, endIndex);
                    var responseModel = JsonConvert.DeserializeObject<T>(response);

                    return new Tuple<T, string>(responseModel, token);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Internet Connection Proplem ");
            }
        }

        public static async Task<Tuple<T, string>> CallApiPostFile(string service, Country country, string authtoken)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    using (var content = new MultipartFormDataContent())
                    {
                        if (country.flagImg.Length > 0)
                        {
                            using(MemoryStream  ms = new MemoryStream())
                            {
                                country.flagImg.CopyTo(ms);
                                country.files=ms.ToArray();
                            }
                        }
                        content.Add( new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8
                        , "application/json"));
                        var request = client.PostAsync($"{Base_Url}{service}", content);
                        NEPDC();
                        string response = await request.Result.Content.ReadAsStringAsync();
                        string responseHeaders = request.Result.Headers.ToString();
                        string token = responseHeaders.Substring(responseHeaders.IndexOf(':') + 9);
                        int endIndex = token.IndexOf($"\r\n");
                        token = token.Substring(0, endIndex);
                        var responseModel = JsonConvert.DeserializeObject<T>(response);

                        return new Tuple<T, string>(responseModel, token);
                    }
                }
                //return new Tuple<T, string>(responseModel, token);
             
            }
            catch (Exception ex)
            {

                throw new Exception("Internet Connection Proplem ");
            }
        }

        public static async Task<Tuple<T, string>> CallApiPut(string service, B model, string authtoken)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8
                        , "application/json");
                    if (string.IsNullOrEmpty(authtoken))
                    {
                        authtoken = Guid.NewGuid().ToString();
                    }
                    httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authtoken);
                    //content.Headers.Add("Bearer", authtoken);
                    var request = httpclient.PutAsync($"{Base_Url}{service}", content);
                    NEPDC();
                    string response = await request.Result.Content.ReadAsStringAsync();
                    string responseHeaders = request.Result.Headers.ToString();
                    string token = responseHeaders.Substring(responseHeaders.IndexOf(':') + 9);
                    int endIndex = token.IndexOf($"\r\n");
                    token = token.Substring(0, endIndex);
                    var responseModel = JsonConvert.DeserializeObject<T>(response);

                    return new Tuple<T, string>(responseModel, token);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Internet Connection Proplem ");
            }
        }

        private static void NEPDC()
        {
            // Disabling certificate validation can expose you to a man-in-the-middle attack
            // which may allow your encrypted message to be read by an attacker
            // https://stackoverflow.com/a/14907718/740639
            ServicePointManager.ServerCertificateValidationCallback =
               delegate (
                   object s,
                   X509Certificate certificate,
                   X509Chain chain,
                   SslPolicyErrors sslPolicyErrors
               ) {
                   return true;
               };
        }

        public static async Task<Tuple<T, string>> CallApiGet(string service, B model, string authtoken)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    HttpClient httpClient = new HttpClient();
                    HttpRequestMessage Newrequest = new HttpRequestMessage();
                    if (model == null || string.IsNullOrEmpty(model.ToString()))
                    {
                        Newrequest.RequestUri = new Uri($"{Base_Url}{service}");
                    }
                    else
                    {
                        Newrequest.RequestUri = new Uri($"{Base_Url}{service}/{model}");
                    }
                    Newrequest.Method = HttpMethod.Get;
                    if (string.IsNullOrEmpty(authtoken))
                    {
                        authtoken = Guid.NewGuid().ToString();
                    }
                    Newrequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authtoken);
                    HttpResponseMessage response =
                        await httpclient.SendAsync(Newrequest, HttpCompletionOption.ResponseHeadersRead);
                    var statusCode = response.StatusCode;
                    var responseString = await response.Content.ReadAsStringAsync();
                    string responseHeaders = response.Headers.ToString();//.Result.Headers.ToString();
                    string token = responseHeaders.Substring(responseHeaders.IndexOf(':') + 9);
                    int endIndex = token.IndexOf($"\r\n");
                    token = token.Substring(0, endIndex);
                    var responseModel = JsonConvert.DeserializeObject<T>(responseString.ToString());
                    //return responseModel;
                    return new Tuple<T, string>(responseModel, token);
                    #region Comment
                    //httpclient.DefaultRequestHeaders.Accept.Clear();
                    //httpclient.DefaultRequestHeaders.Accept.Add(
                    //     new MediaTypeWithQualityHeaderValue("application/json"));

                    //httpclient.DefaultRequestHeaders.Add("APIKey", model.ToString());
                    //var request = httpclient.GetAsync($"{Base_Url}{service}");
                    //string responsee = await request.Result.Content.ReadAsStringAsync();
                    //return JsonConvert.DeserializeObject<T>(responsee);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Internet Connection Proplem");
            }

        }
        public static async Task<List<T>> CallApiGetList(string service, B model)
        
        {
            using (var httpclient = new HttpClient())
            {
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage Newrequest = new HttpRequestMessage();
                Newrequest.RequestUri = new Uri($"{Base_Url}{service}");
                Newrequest.Method = HttpMethod.Get;
                //Newrequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", model.ToString());
                HttpResponseMessage response =
                    await httpclient.SendAsync(Newrequest, HttpCompletionOption.ResponseHeadersRead);
                var responseString = await response.Content.ReadAsStringAsync();
                var statusCode = response.StatusCode;
                return JsonConvert.DeserializeObject<List<T>>(responseString.ToString());
            }
        }

        public static async Task<Tuple<T, string>> CallApiDelete(string service, B model, string authtoken)
        {
            try
            {
                using (var httpclient = new HttpClient())
                {
                    HttpClient httpClient = new HttpClient();
                    HttpRequestMessage Newrequest = new HttpRequestMessage();
                    if (model == null || string.IsNullOrEmpty(model.ToString()))
                    {
                        Newrequest.RequestUri = new Uri($"{Base_Url}{service}");
                    }
                    else
                    {
                        Newrequest.RequestUri = new Uri($"{Base_Url}{service}/{model}");
                    }
                    Newrequest.Method = HttpMethod.Delete;
                    if (string.IsNullOrEmpty(authtoken))
                    {
                        authtoken = Guid.NewGuid().ToString();
                    }
                    Newrequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authtoken);
                    HttpResponseMessage response =
                        await httpclient.SendAsync(Newrequest, HttpCompletionOption.ResponseHeadersRead);
                    var statusCode = response.StatusCode;
                    var responseString = await response.Content.ReadAsStringAsync();
                    string responseHeaders = response.Headers.ToString();//.Result.Headers.ToString();
                    string token = responseHeaders.Substring(responseHeaders.IndexOf(':') + 9);
                    int endIndex = token.IndexOf($"\r\n");
                    token = token.Substring(0, endIndex);
                    var responseModel = JsonConvert.DeserializeObject<T>(responseString.ToString());
                    //return responseModel;
                    return new Tuple<T, string>(responseModel, token);
                    #region Comment
                    //httpclient.DefaultRequestHeaders.Accept.Clear();
                    //httpclient.DefaultRequestHeaders.Accept.Add(
                    //     new MediaTypeWithQualityHeaderValue("application/json"));

                    //httpclient.DefaultRequestHeaders.Add("APIKey", model.ToString());
                    //var request = httpclient.GetAsync($"{Base_Url}{service}");
                    //string responsee = await request.Result.Content.ReadAsStringAsync();
                    //return JsonConvert.DeserializeObject<T>(responsee);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Internet Connection Proplem");
            }

        }

    }
}

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
    public class ApiCaller<T, B>
    {
        public static string Base_Url = $"http://75.119.136.238:8016/api/"; // live 
                                                                     //static string Base_Url = $"https://mob.jsjbank.com:3000/JSB_OMNI_Ph2/omniServices/"; test 

        //static string Base_Url = $"https://mob.jsjbank.com:8383/JSB_OMNI_Ph2/omniServices/cpServices/"; // live 
        static IHttpClientFactory _httpClientFactory;

        public ApiCaller(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public static async Task<Tuple<T, string>> callApi(string service, B model)
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

        public static async Task<Tuple<T, string>> CallApiPostCountryFlag(string service, Country country, string authtoken)
        {
            try
            {
                
                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {
                    
                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content
                    var nameContent = new StringContent(country.NameAR);
                    formDataContent.Add(nameContent, "NameAR");
                    nameContent = new StringContent(country.NameEN);
                    formDataContent.Add(nameContent, "NameEN");
                    nameContent = new StringContent(country.Id.ToString());
                    formDataContent.Add(nameContent, "Id");
                    nameContent = new StringContent(country.CountryCode.ToString());
                    formDataContent.Add(nameContent, "Countrycode");


                    // Convert the file to stream content
                    var fileStreamContent = new StreamContent(country.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", country.files.FileName);

                    //
                    // Send the post request to the API with the form data content
                    var response = await httpClient.PostAsync($"{Base_Url}{service}", formDataContent);
                    // Check if the request was successful
                    //if (response.IsSuccessStatusCode)
                    //{
                    // Process the response
                    var result = await response.Content.ReadAsStringAsync();
                    // Do something with the result
                    var responseModel = JsonConvert.DeserializeObject<T>(result);

                    return new Tuple<T, string>(responseModel, "");
                    //}
                    throw new Exception("Internet Connection Proplem ");
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Internet Connection Proplem ");
            }
        }

        public static async Task<Tuple<T, string>> CallApiPutCountryFlag(string service, Country country, string authtoken)
        {
            try
            {

                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {

                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content
                    var nameContent = new StringContent(country.NameAR);
                    formDataContent.Add(nameContent, "NameAR");
                    nameContent = new StringContent(country.NameEN);
                    formDataContent.Add(nameContent, "NameEN");
                    nameContent = new StringContent(country.Id.ToString());
                    formDataContent.Add(nameContent, "Id");
                    nameContent = new StringContent(country.CountryCode.ToString());
                    formDataContent.Add(nameContent, "Countrycode");


                    // Convert the file to stream content
                    var fileStreamContent = new StreamContent(country.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", country.files.FileName);

                    //
                    // Send the post request to the API with the form data content
                    var response = await httpClient.PutAsync($"{Base_Url}{service}", formDataContent);
                    // Check if the request was successful
                    //if (response.IsSuccessStatusCode)
                    //{
                    // Process the response
                    var result = await response.Content.ReadAsStringAsync();
                    // Do something with the result
                    var responseModel = JsonConvert.DeserializeObject<T>(result);

                    return new Tuple<T, string>(responseModel, "");
                    //}
                    throw new Exception("Internet Connection Proplem ");
                }

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
               )
               {
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

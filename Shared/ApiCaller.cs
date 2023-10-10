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
using System.Security.Policy;
using System.Text;

namespace LiperFrontend.Shared
{
    public class ApiCaller<T, B>
    {
        public static string Base_Url = $"http://75.119.136.238:8016/api/"; // live 
        public static string Base_Url_files = $"http://75.119.136.238:8016"; // 
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

        public static async Task<Tuple<T, string>> CallApiPostProduct(string service, Product product, string authtoken)
        {
            try
            {

                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {

                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content
                    var nameContent = new StringContent(product.NameAR);
                    formDataContent.Add(nameContent, "NameAR");
                    nameContent = new StringContent(product.Name);
                    formDataContent.Add(nameContent, "Name");
                    nameContent = new StringContent(product.Id.ToString());
                    formDataContent.Add(nameContent, "Id");
                    nameContent = new StringContent(product.Description);
                    formDataContent.Add(nameContent, "Description");
                    nameContent = new StringContent(product.DescriptionAR);
                    formDataContent.Add(nameContent, "DescriptionAR");
                    nameContent = new StringContent(product.Discount.ToString());
                    formDataContent.Add(nameContent, "Discount");
                    nameContent = new StringContent(product.subCategoryId.ToString());
                    formDataContent.Add(nameContent, "subCategoryId");
                    nameContent = new StringContent(product.Price.ToString());
                    formDataContent.Add(nameContent, "Price");
                    nameContent = new StringContent(product.isPercentage.ToString());
                    formDataContent.Add(nameContent, "isPercentage");


                    // Convert the file to stream content
                    var fileStreamContent = new StreamContent(product.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", product.files.FileName);
                   


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
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Internet Connection Proplem ");
            }
        }

        public static async Task<Tuple<T, string>> CallApiPostProductImage(string service, ProductImage image, string authtoken)
        {
            try
            {

                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {

                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content
                    
                    var nameContent = new StringContent(image.id.ToString());
                    formDataContent.Add(nameContent, "Id");
                    nameContent = new StringContent(image.productId.ToString());
                    formDataContent.Add(nameContent, "productId");
                    


                    // Convert the file to stream content
                    var fileStreamContent = new StreamContent(image.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", image.files.FileName);



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
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Internet Connection Proplem ");
            }
        }

        public static async Task<Tuple<T, string>> CallApiPutProductImage(string service, ProductImage image, string authtoken)
        {
            try
            {

                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {

                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content

                    var nameContent = new StringContent(image.id.ToString());
                    formDataContent.Add(nameContent, "id");
                    nameContent = new StringContent(image.productId.ToString());
                    formDataContent.Add(nameContent, "productId");



                    // Convert the file to stream content
                    var fileStreamContent = new StreamContent(image.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", image.files.FileName);



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
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Internet Connection Proplem ");
            }
        }

        public static async Task<Tuple<T, string>> CallApiPostAgentNotification(string service, Notification notification, string authtoken)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var topic = "agent";
                    var url = "https://fcm.googleapis.com/fcm/send";
                    var key = "key=AAAAmE1CfZA:APA91bETWAwnARKLgAFjnYZoToVIBifK3RQoiv2Lk4o1CCcZ7faLMXQf6eSLe3FeO1LjRoRvz2p2dQ5Gek3u3FHwXnJfDXbsfDKM-tYgAs_ibtmzKXEwxZ98ySHGhkHP1dm9Kxd3fJyL";
                    if (notification.userTypeId == 2)
                    {
                        topic = "liper";
                    }
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",key.ToString());

                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //httpClient.DefaultRequestHeaders.Add("content-type", "application/json");

                    var requestBody = new
                    {
                        to = $"/topics/" + topic,
                        notification = new
                        {
                            title = notification.text,
                            body = notification.description,
                            image = "url"
                        }
                    };
                    var response = await httpClient.PostAsJsonAsync(url, requestBody);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();

                    }

                }
                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {

                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content
                    var nameContent = new StringContent(notification.text);
                    formDataContent.Add(nameContent, "text");
                    nameContent = new StringContent(notification.description);
                    formDataContent.Add(nameContent, "description");
                    nameContent = new StringContent(notification.id.ToString());
                    formDataContent.Add(nameContent, "Id");
                    nameContent = new StringContent(notification.isRead.ToString());
                    formDataContent.Add(nameContent, "isRead");
                    nameContent = new StringContent(notification.agent_customer_Id.ToString());
                    formDataContent.Add(nameContent, "AgentId");


                    // Convert the file to stream content
                    var fileStreamContent = new StreamContent(notification.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", notification.files.FileName);

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


                    //Convert the file to stream content
                    if (country.files is not null)
                    {
                        var fileStreamContent = new StreamContent(country.files.OpenReadStream());
                        formDataContent.Add(fileStreamContent, "files", Guid.NewGuid() + ".Png");
                    }
                    else
                    {
                        var fileStreamContent = await ConvertFileToStreamContent(country.flagImgUrl);
                        formDataContent.Add(fileStreamContent, "files", Guid.NewGuid() + ".Png");
                    }


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
        public static async Task<Tuple<T, string>> CallApiPutProduct(string service, Product product, string authtoken)
        {
            try
            {

                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {

                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content
                    var nameContent = new StringContent(product.NameAR);
                    formDataContent.Add(nameContent, "NameAR");
                    nameContent = new StringContent(product.Name);
                    formDataContent.Add(nameContent, "Name");
                    nameContent = new StringContent(product.Id.ToString());
                    formDataContent.Add(nameContent, "Id");
                    nameContent = new StringContent(product.Description);
                    formDataContent.Add(nameContent, "Description");
                    nameContent = new StringContent(product.DescriptionAR);
                    formDataContent.Add(nameContent, "DescriptionAR");
                    nameContent = new StringContent(product.Discount.ToString());
                    formDataContent.Add(nameContent, "Discount");
                    nameContent = new StringContent(product.subCategoryId.ToString());
                    formDataContent.Add(nameContent, "subCategoryId");
                    nameContent = new StringContent(product.Price.ToString());
                    formDataContent.Add(nameContent, "Price");
                    nameContent = new StringContent(product.isPercentage.ToString());
                    formDataContent.Add(nameContent, "isPercentage");

                    if (false)
                    {
                        using (HttpResponseMessage r = await httpClient.GetAsync(product.productMainImage))
                        {
                            if (r.IsSuccessStatusCode)
                            {
                                Stream stream = await r.Content.ReadAsStreamAsync();

                                //MultipartFormDataContent content = new MultipartFormDataContent(stream);
                                        formDataContent.Add(new StreamContent(stream) , "files", Guid.NewGuid() + ".Png");
                                     
                                
                            }
                        }
                    }

                    //Convert the file to stream content

                    var fileStreamContent = new StreamContent(product.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", Guid.NewGuid() + ".Png");


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

        public static StreamContent ConvertFileToStreamContentLocal(string filePath)
        {
            // Open the file as a stream
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Create a new stream content with the file stream
                var streamContent = new StreamContent(fileStream);

                // Set the content headers
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                streamContent.Headers.ContentLength = fileStream.Length;

                return streamContent;
            }
        }
        public static async Task<StreamContent> ConvertFileToStreamContent(string filePath)
        {
            try
            {

                // Create a new HttpClient
                using (var httpClient = new HttpClient())
                {
                    // Download the file as a stream
                    var fileStream = await httpClient.GetStreamAsync(filePath);

                    // Create a new stream content with the file stream
                    var streamContent = new StreamContent(fileStream);


                    // Set the content headers
                    var mediaType = GetMediaTypeFromFilePath(filePath);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue(mediaType);
                    var fileInfo = new FileInfo(new Uri(filePath).AbsolutePath);
                    //streamContent.Headers.ContentLength = fileInfo.Length;
                    var response = await httpClient.GetAsync(filePath);
                    if (response.Content.Headers.ContentLength.HasValue)
                        streamContent.Headers.ContentLength = response.Content.Headers.ContentLength.Value;
                    return streamContent;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static string GetMediaTypeFromFilePath(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                // Add more cases for other supported file types
                default:
                    return "application/octet-stream";
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

        public static async Task<Tuple<T, string>> CallApiPostCustomer(string service, Customer customer, string authtoken)
        {
            try
            {

                // Create a new HttpClient instance
                using (var httpClient = new HttpClient())
                {

                    // Create a new multipart form data content
                    var formDataContent = new MultipartFormDataContent();

                    // Serialize the object properties to string content
                    var nameContent = new StringContent(customer.nameAR);
                    formDataContent.Add(nameContent, "NameAR");
                    nameContent = new StringContent(customer.name);
                    formDataContent.Add(nameContent, "Name");
                    nameContent = new StringContent(customer.id.ToString());
                    formDataContent.Add(nameContent, "Id");
                    nameContent = new StringContent(customer.password.ToString());
                    formDataContent.Add(nameContent, "Passowrd");
                    nameContent = new StringContent(customer.confirmPassword.ToString());
                    formDataContent.Add(nameContent, "ConfairmPassowrd");
                    nameContent = new StringContent(customer.genderId.ToString());
                    formDataContent.Add(nameContent, "genderId");
                    nameContent = new StringContent(customer.username.ToString());
                    formDataContent.Add(nameContent, "Username");
                    nameContent = new StringContent(customer.email.ToString());
                    formDataContent.Add(nameContent, "Email");
                    nameContent = new StringContent(customer.phone.ToString());
                    formDataContent.Add(nameContent, "Phone");
                    nameContent = new StringContent(customer.cityId.ToString());
                    formDataContent.Add(nameContent, "cityId");


                    // Convert the file to stream content
                    var fileStreamContent = new StreamContent(customer.files.OpenReadStream());
                    formDataContent.Add(fileStreamContent, "files", customer.files.FileName);

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

    }
}

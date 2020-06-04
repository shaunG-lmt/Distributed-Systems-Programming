using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace DistSysACWClient
{
    #region Task 10 and beyond
    #endregion
    class Program
    {
        private static HttpClient client = new HttpClient();
        private static string clientUsername;
        private static string clientApiKey;
        private static RSACryptoServiceProvider RSAVerify;
        private static bool gotKey = false;
        static void Main()
        {
            // Setting the defaults for the client.
            client.BaseAddress = new Uri("https://localhost:44307/api/");
            
            // Startup user display and input.
            Console.WriteLine("Hello. What would you like to do?");
            string[] userInput = Console.ReadLine().Split(" ");
            Console.WriteLine("...please wait...");

            // Request activation
            HandleRequest(userInput);

            // Addtional requests, user display and input.
            while (true)
            {
                Console.WriteLine("What would you like to do next?");
                userInput = Console.ReadLine().Split(" ");
                Console.Clear();
                Console.WriteLine("...please wait...");
                HandleRequest(userInput);
            }
        }
        static async Task RunAsync(string requestUri, string content, string apiKey, string httpMethod)
        {
            try
            {
                switch (httpMethod)
                {
                    case "get":
                        {
                            if (apiKey == null)
                            {
                                Task<string> taskGet = GetStringAsync(requestUri);
                                if (await Task.WhenAny(taskGet, Task.Delay(20000)) == taskGet)
                                { Console.WriteLine(taskGet.Result); }
                                else
                                { Console.WriteLine("Request Timed Out"); }
                                break;
                            }
                            else
                            {
                                Task<string> taskGet = GetStringAsync(requestUri, apiKey);
                                if (await Task.WhenAny(taskGet, Task.Delay(20000)) == taskGet)
                                { Console.WriteLine(taskGet.Result); }
                                else
                                { Console.WriteLine("Request Timed Out"); }
                                break;
                            }
                        }
                    case "post":
                        {
                            if (apiKey == null)
                            {
                                Task<string> taskPost = PostStringAsync(requestUri, content);
                                if (await Task.WhenAny(taskPost, Task.Delay(20000)) == taskPost)
                                { Console.WriteLine(taskPost.Result); }
                                else
                                { Console.WriteLine("Request Timed Out"); }
                                break;
                            }
                            else
                            {
                                Task<string> taskPost = PostStringAsync(requestUri, content, apiKey);
                                if (await Task.WhenAny(taskPost, Task.Delay(20000)) == taskPost)
                                { Console.WriteLine(taskPost.Result); }
                                else
                                { Console.WriteLine("Request Timed Out"); }
                                break;
                            }
                        }
                    case "del":
                        {
                            Task<string> taskDel = DeleteAsync(requestUri, apiKey);
                            if (await Task.WhenAny(taskDel, Task.Delay(20000)) == taskDel)
                            { Console.WriteLine(taskDel.Result); }
                            else
                            { Console.WriteLine("Request Timed Out"); }
                            break;
                        }
                    case "getKey":
                        {
                            Task<string> taskGet = GetStringAsync(requestUri, apiKey);
                            if (await Task.WhenAny(taskGet, Task.Delay(20000)) == taskGet)
                            {

                                if (taskGet.Result.StartsWith("<"))
                                {
                                    try
                                    {
                                        RSAVerify = new RSACryptoServiceProvider();
                                        CoreExtensions.RSACryptoExtensions.FromXmlStringCore22(RSAVerify, taskGet.Result);
                                        Console.WriteLine("Got Public Key");
                                        gotKey = true;
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e);
                                        Console.WriteLine("Couldnt get the Public Key");
                                    }
                                    
                                }
                                else
                                {
                                    Console.WriteLine("Couldnt get the Public Key"); 
                                }
                            }
                            else
                            { Console.WriteLine("Request Timed Out"); }
                            break;
                        }
                    case "getSign":
                        {
                            Task<string> taskGet = GetStringAsync(requestUri, apiKey);
                            if (await Task.WhenAny(taskGet, Task.Delay(20000)) == taskGet)
                            {
                                // Sent data to verify by.
                                byte[] data = Encoding.ASCII.GetBytes(content);

                                // Received signed data
                                string x = taskGet.Result.Replace("-", "");
                                byte[] StringToByteArray(String hex)
                                {
                                    int NumberChars = hex.Length;
                                    byte[] bytes = new byte[NumberChars / 2];
                                    for (int i = 0; i < NumberChars; i += 2)
                                        bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                                    return bytes;
                                }
                                byte[] signature = StringToByteArray(x);

                                if (RSAVerify.VerifyData(data, "SHA1", signature))
                                {
                                    Console.WriteLine("Message was successfully signed");
                                }
                                else
                                {
                                    Console.WriteLine("Message was not successfully signed");
                                }
                            }
                            else
                            { Console.WriteLine("Request Timed Out"); }
                            break;
                        }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetBaseException().Message);
            }
        }
        static async Task<string> GetStringAsync(string path)
        {
            string responsestring = "";
            HttpResponseMessage response = await client.GetAsync(path);
            responsestring = await response.Content.ReadAsStringAsync();
            return responsestring;
        }
        static async Task<string> GetStringAsync(string path, string apikey)
        {
            // Set ApiKey
            client.DefaultRequestHeaders.Add("Apikey", apikey);

            string responsestring = "";
            HttpResponseMessage response = await client.GetAsync(path);
            responsestring = await response.Content.ReadAsStringAsync();

            // Reset client apiKey for future requests.
            client.DefaultRequestHeaders.Remove("Apikey");

            return responsestring;
        }
        static async Task<string> PostStringAsync(string path, string content)
        {
            var jsonString = JsonConvert.SerializeObject(content);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json"); 
            string responsestring = "";
            HttpResponseMessage response = await client.PostAsync(path, stringContent);
            responsestring = await response.Content.ReadAsStringAsync();
            return responsestring;
        }
        static async Task<string> PostStringAsync(string path, string content, string apikey)
        {
            // Set ApiKey
            client.DefaultRequestHeaders.Add("Apikey", apikey);
            // Build JSON
            string[] requestedUserDetails = content.Split(" ");
            string jsonString = "{ \"username\": \"" + requestedUserDetails[0] + "\", \"role\": \"" + requestedUserDetails[1] + "\" }";
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            // Send Request
            string responsestring = "";
            HttpResponseMessage response = await client.PostAsync(path, stringContent);
            responsestring = await response.Content.ReadAsStringAsync();

            // Reset client apiKey for future requests.
            client.DefaultRequestHeaders.Remove("Apikey");

            return responsestring;
        }
        static async Task<string> DeleteAsync(string path, string apiKey)
        {
            string responsestring = "";
            // Set ApiKey
            client.DefaultRequestHeaders.Add("ApiKey", apiKey);
            //Build JSON

            HttpResponseMessage response = await client.DeleteAsync(path);
            responsestring = await response.Content.ReadAsStringAsync();

            // Reset client creditials
            client.DefaultRequestHeaders.Remove("Apikey");
            clientUsername = null;
            clientApiKey = null;

            if (responsestring == "true")
            {
                return "True";
            }
            else
            {
                return "False";
            }
        }

        static void HandleRequest(string[] request)
        {
            switch (request[0])
            {
                case "TalkBack":
                    { 
                        
                        if (request[1] == "Hello")
                        {
                            if (request.Length > 2)
                            {
                                Console.WriteLine("Invalid TalkBack Hello request...");
                            }
                            else
                            {
                                RunAsync("talkback/hello", null, null, "get").Wait();
                            }
                        }
                        else if (request[1] == "Sort")
                        {
                            TalkBackSort(request);
                        }
                    }
                    break;
                case "User":
                    {
                        switch (request[1])
                        {
                            case "Get":
                                {
                                    RunAsync("user/new?username=" + request[2], null, null, "get").Wait();
                                    break;
                                }
                            case "Post":
                                {
                                    RunAsync("user/new", request[2], null, "post").Wait();
                                    break;
                                }
                            case "Set":
                                {
                                    try
                                    {
                                        clientUsername = request[2];
                                        clientApiKey = request[3];
                                        Console.WriteLine("Stored");
                                    }
                                    catch
                                    {
                                        Console.WriteLine("Invalid format... Please try again.");
                                        Main();
                                    }
                                }
                                break;
                            case "Delete":
                                {
                                    if (clientUsername == null)
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set first");
                                    }
                                    else
                                    {
                                        string requestUri = "user/removeuser?username=" + clientUsername;
                                        RunAsync(requestUri, null, clientApiKey, "del").Wait();
                                    }
                                }
                                break;
                            case "Role":
                                {
                                    if (clientUsername == null)
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set first");
                                    }
                                    else
                                    {
                                        string requestUri = "user/changerole";
                                        string content = request[2] + " " + request[3];
                                        RunAsync(requestUri, content, clientApiKey, "post").Wait();
                                    }
                                    break;
                                }
                        }
                    }
                    break;
                case "Protected":
                    {
                        switch (request[1])
                        {
                            case "Hello":
                                {
                                    if (clientUsername == null)
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set first");
                                    }
                                    else
                                    {
                                        string requestUri = "protected/hello";
                                        RunAsync(requestUri, null, clientApiKey, "get").Wait();
                                    }
                                    break;
                                }
                            case "SHA1":
                                {
                                    if (clientUsername == null)
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set first");
                                    }
                                    else
                                    {
                                        string requestUri = "protected/sha1?message=" + request[2];
                                        RunAsync(requestUri, null, clientApiKey, "get").Wait();
                                    }
                                    break;
                                }
                            case "SHA256":
                                {
                                    if (clientUsername == null)
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set first");
                                    }
                                    else
                                    {
                                        string requestUri = "protected/sha256?message=" + request[2];
                                        RunAsync(requestUri, null, clientApiKey, "get").Wait();
                                    }
                                    break;
                                }
                            case "Get":
                                {
                                    if (clientUsername == null)
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set first");
                                    }
                                    else
                                    {
                                        string requestUri = "protected/getpublickey";
                                        RunAsync(requestUri, null, clientApiKey, "getKey").Wait();
                                    }
                                    break;
                                }
                            case "Sign":
                                {
                                    if (clientUsername == null)
                                    {
                                        Console.WriteLine("You need to do a User Post or User Set first");
                                    }
                                    else if (gotKey == false) 
                                    {
                                        Console.WriteLine("Client doesn't yet have the public key");
                                    }
                                    else
                                    {
                                        string requestUri = "protected/sign?message=" + request[2];
                                        RunAsync(requestUri, request[2], clientApiKey, "getSign").Wait();
                                    }
                                    break;
                                }
                        }
                        break;
                    }
                case "Exit":
                    Environment.Exit(0);
                    break;

                default:
                    // Invalid, handle.
                    break;
            }
        }

        static void TalkBackSort(string[] request)
        {
            // Get numbers from user input.
            char[] ch = { '[',']' };
            string[] numbers = request[2].Split(",");
            numbers[0] = numbers[0].TrimStart(ch);
            numbers[numbers.Length-1] = numbers[numbers.Length-1].TrimEnd(ch);
            
            // Build request URI.
            string requestUri = "talkback/sort?";
            for (int i = 0; i < numbers.Length; i++)
            {
                requestUri += "integers=" + numbers[i] +"&";
            }
            requestUri.TrimEnd(new char[] { '&' });

            // Send request to server with URI.
            RunAsync(requestUri, null, null, "get").Wait();
        }
    }
}

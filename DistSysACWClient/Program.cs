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
        static HttpClient client = new HttpClient();
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
                HandleRequest(userInput);
            }
        }
        static async Task RunAsync(string requestUri, string body, string apiKey, string httpMethod)
        {
            try
            {
                switch (httpMethod)
                {
                    // Get request with no apikey.
                    case "get": 
                        Task<string> taskGet = GetStringAsync(requestUri);
                        if (await Task.WhenAny(taskGet, Task.Delay(20000)) == taskGet)
                        { Console.WriteLine(taskGet.Result); }
                        else
                        { Console.WriteLine("Request Timed Out"); }
                        break;

                    // Post request with content in the body and no apikey.
                    case "post": 
                        Task<string> taskPost = PostStringAsync(requestUri, body);
                        if (await Task.WhenAny(taskPost, Task.Delay(20000)) == taskPost)
                        { Console.WriteLine(taskPost.Result); }
                        else
                        { Console.WriteLine("Request Timed Out"); }
                        break;
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
        static async Task<string> PostStringAsync(string path, string body)
        {
            //const string quote = "\"";
            //body = quote + body + quote;

            var jsonString = JsonConvert.SerializeObject(body);
            var stringContent = new StringContent(jsonString, Encoding.UTF8, "application/json"); 
            
            
            string responsestring = "";
            
            HttpResponseMessage response = await client.PostAsync(path, stringContent);
            responsestring = await response.Content.ReadAsStringAsync();
            return responsestring;
        }

        static void HandleRequest(string[] request)
        {
            switch (request[0])
            {
                case "TalkBack": 
                    if (request[1] == "Hello")
                    {
                        RunAsync("talkback/hello", null, null, "get").Wait();
                    }
                    else if (request[1] == "Sort")
                    {
                        TalkBackSort(request);
                    }
                    break;

                case "User":
                    switch (request[1])
                    {
                        case "Get":
                            RunAsync("user/new?username=" + request[2], null, null, "get").Wait();
                            break;

                        case "Post":
                            RunAsync("user/new", request[2], null, "post").Wait();
                            break;

                        case "Set":
                            break;

                        case "Delete":
                            break;

                        case "Role":
                            break;
                    }
                    break;

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

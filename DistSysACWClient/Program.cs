using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
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
            client.BaseAddress = new Uri("https://localhost:44307/api/");

            Console.WriteLine("Hello. What would you like to do?");
            string[] userInput = Console.ReadLine().Split(" ");
            Console.WriteLine("...please wait...");
            HandleRequest(userInput);

            while (true)
            {
                Console.WriteLine("What would you like to do next?");
                userInput = Console.ReadLine().Split(" ");
                HandleRequest(userInput);
            }
        }
        static async Task RunAsync(string requestUri)
        {
            try
            {
                Task<string> task = GetStringAsync(requestUri);
                if (await Task.WhenAny(task, Task.Delay(20000)) == task)
                {
                    Console.WriteLine(task.Result);
                }
                else
                {
                    Console.WriteLine("Request Timed Out");
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

        static void HandleRequest(string[] request)
        {
            switch (request[0])
            {
                case "TalkBack":
                    if (request[1] == "Hello")
                    {
                        RunAsync("talkback/hello").Wait();
                    }
                    else if (request[1] == "Sort")
                    {
                        TalkBackSort(request);
                    }
                    else
                    {
                        // Invalid, handle.
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
            RunAsync(requestUri).Wait();
        }
    }
}

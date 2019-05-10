using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QB.BAL.Helpers
{
    public class CustomHttpHelper
    {
        public static string MakeHttpRequest(string type , string url , string body , string token)
        {
            //Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer ");
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                try
                {
                    if (string.Equals(type.ToLower(), "get"))
                    {
                        return client.GetStringAsync(url).Result;
                    }
                    else if (string.Equals(type.ToLower(), "post"))
                    {
                        var stringContent = new StringContent("", Encoding.UTF8, "application/json");
                        HttpResponseMessage response =  client.PostAsync(url, stringContent).Result;
                        response.EnsureSuccessStatusCode();
                        return  response.Content.ReadAsStringAsync().Result;
                    }
                    return "";
                }
                catch (HttpRequestException e)
                {
                    return e.ToString();
                }
            }        
        }
        /// The client information used to get the OAuth Access Token from the server.
        static string clientId = "Q0TMGv2KUfZT5vfgcg0kKBiU1hDvzB3Gv6Ir4vNdDRSVru6O26";
        static string clientSecret = "MCUSCRZF8q78uDethITkr875cGEqu8on7kUb33Qa";

       
    }
}

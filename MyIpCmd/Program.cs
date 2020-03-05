using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;

namespace MyIpCmd
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                using (var Client = new HttpClient())
                {
                    var address = "https://api.myip.com/";
                    var Url = new Uri(address);
                    //var request = new HttpRequestMessage(HttpMethod.Get, Url);

                    using ( var response = Client.GetAsync(address).Result)
                    {
                        using (StreamReader sr = new StreamReader(response.Content.ReadAsStreamAsync().Result))
                        {
                            JObject jsonObj = JObject.Parse(sr.ReadToEnd());
                            Console.WriteLine(jsonObj["ip"]);
                        }
                    }
                }
            }
            else if (args.Length == 1)
            {
                string targetaddress = args[0];
                Console.WriteLine($"Arg : {targetaddress}"); 

                using (var Client = new HttpClient())
                {
                    var address = $"http://ip-api.com/json/{targetaddress}";
                    var Url = new Uri(address);

                    using (var response = Client.GetAsync(address).Result)
                    {
                        string infojson = await response.Content.ReadAsStringAsync();
                        JObject infoobj = JObject.Parse(infojson);
                        Console.WriteLine(infoobj.ToString());                       
                    }
                }
            }
            else
            {
                Console.WriteLine("argument lenth is not correct.");
            }


        }
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        ///
        // OPCION 1
        //
        //static void Main(string[] args)
        //{
        //    HttpClient client = new HttpClient();

        //    var msg = client.GetStringAsync("http://localhost:2901/api/values").Result;
        //    Values[] vals = Newtonsoft.Json.JsonConvert.DeserializeObject<Values[]>(msg);
        //    foreach (var item in vals)
        //    {
        //        Console.WriteLine($"[{item.Values1}, {item.Values2}]");
        //    }


        //    var msg2 = client.GetStringAsync("http://localhost:2901/api/values/44").Result;
        //    Values val = Newtonsoft.Json.JsonConvert.DeserializeObject<Values>(msg2);
        //    Console.WriteLine($"[{val.Values1}, {val.Values2}]");


        //    Console.WriteLine("Pulse INTRO para finalizar...");
        //    Console.ReadLine();
        //}

        //
        // OPCION 2
        //
        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();

            var msg = await client.GetStringAsync("http://localhost:2901/api/values");
            Values[] vals = Newtonsoft.Json.JsonConvert.DeserializeObject<Values[]>(msg);
            foreach (var item in vals)
            {
                Console.WriteLine($"[{item.Values1}, {item.Values2}]");
            }

            var msg2 = await client.GetStringAsync("http://localhost:2901/api/values/44");
            Values val = Newtonsoft.Json.JsonConvert.DeserializeObject<Values>(msg2);
            Console.WriteLine($"[{val.Values1}, {val.Values2}]");

            Console.WriteLine("Pulse INTRO para finalizar...");
            Console.ReadLine();
        }
    }
}

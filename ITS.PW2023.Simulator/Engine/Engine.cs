using ITS.PW2023.Simulator.Models;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace ITS.PW2023.Simulator.Engine
{
    public class Engine
    {
        public static void Run(Device[] devices, HttpClient client, string ApiSubKey)
        {
            while (true)
            {
                for (int i = 0; i < devices.Length; i++)
                {
                    ActivityData? data = devices[i].GenerateActivityData();
                    var serializedModel = JsonSerializer.Serialize(data);
                    var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                    content.Headers.Add("Ocp-Apim-Subscription-Key", ApiSubKey);
                    var response = client.PostAsync("/writeData", content);
                }
                Thread.Sleep(10000);
            }
        }
    }
}

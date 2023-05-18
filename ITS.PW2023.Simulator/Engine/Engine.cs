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
            Task[] tasks = new Task[10];
            for (int i = 0; i < devices.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(async () =>
                {
                    while (true)
                    {
                        ActivityData? data = devices[i].GenerateActivityData();
                        var serializedModel = JsonSerializer.Serialize(data);
                        var content = new StringContent(serializedModel, UTF8Encoding.UTF8, "application/json");
                        content.Headers.Add("Ocp-Apim-Subscription-Key", ApiSubKey);
                        var response = await client.PostAsync("/writeData", content);
                        Thread.Sleep(10000); // Wait for 10 seconds before repeating the task
                    }
                }, TaskCreationOptions.LongRunning);
            }

            //si ferma dopo un minuto
            Task.WaitAll(tasks, TimeSpan.FromMinutes(1));
        }
    }
}

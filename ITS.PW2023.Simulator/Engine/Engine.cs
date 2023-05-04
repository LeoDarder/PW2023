using ITS.PW2023.Simulator.Models;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace ITS.PW2023.Simulator.Engine
{
    public class Engine
    {
        public static void Run(Device[] devices, HttpClient client)
        {
            Task[] tasks = new Task[10];
            for (int i = 0; i < devices.Length; i++)
            {
                int index = i; // Store the current index in a local variable
                tasks[i] = Task.Factory.StartNew(async () =>
                {
                    while (true)
                    {
                        ActivityData? data = devices[index].GenerateActivityData();

                        var serializedModel = JsonSerializer.Serialize(data);
                        var content = new StringContent(serializedModel, Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("https://localhost:7030/writeData", content);
                        Thread.Sleep(10000); // Wait for 10 seconds before repeating the task
                    }
                }, TaskCreationOptions.LongRunning);
            }

            //si ferma dopo un minuto
            Task.WaitAll(tasks, TimeSpan.FromMinutes(1));
        }
    }
}

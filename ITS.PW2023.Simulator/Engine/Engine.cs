using ITS.PW2023.Simulator.Models;
using System.Text.Json;

namespace ITS.PW2023.Simulator.Engine
{
    public class Engine
    {
        public static void Run(Device[] devices)
        {
            Task[] tasks = new Task[10];
            for (int i = 0; i < devices.Length; i++)
            {
                int index = i; // Store the current index in a local variable
                tasks[i] = Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        var data = devices[index].GenerateActivityData();
                        Console.WriteLine(JsonSerializer.Serialize(data));
                        Console.Out.Flush(); // Ensure that the output appears immediately in the console window
                        Thread.Sleep(10000); // Wait for 10 seconds before repeating the task
                    }
                }, TaskCreationOptions.LongRunning);
            }

            // Wait for all tasks to complete (with a timeout of 1 hour)
            Task.WaitAll(tasks, TimeSpan.FromMinutes(1));
        }
    }
}

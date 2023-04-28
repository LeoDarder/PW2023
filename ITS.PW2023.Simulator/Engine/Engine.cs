using ITS.PW2023.Simulator.Models;
using System.Text.Json;

namespace ITS.PW2023.Simulator.Engine
{
    public class Engine
    {
        public static void Run(Device[] devices)
        {
            //per ora li genero tutti insieme
            while (true)
            {
                foreach (Device device in devices)
                {
                    device.StartNewActivity();
                    var data = device.GenerateActivityData();
                    Console.WriteLine(JsonSerializer.Serialize(data));
                    //chiama db.uploadData
                    Thread.Sleep(10000);    //metodo temporaneo per aspettare
                }
            }
        }
    }
}

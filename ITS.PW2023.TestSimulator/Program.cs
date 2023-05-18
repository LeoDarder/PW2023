using ITS.PW2023.Simulator.Config;
using ITS.PW2023.TestSimulator;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient();
    })
    .Build();

host.Run();

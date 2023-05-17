using ITS.PW2023.Simulator.Config;
using ITS.PW2023.Simulator.Engine;
using ITS.PW2023.Simulator.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ITS.PW2023.TestSimulator
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly HttpClient _httpClient;
        private readonly Config _config;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
            _config = new Config();
            _configuration = configuration;
            configuration.GetSection(Config.ConfigPosition).Bind(_config);
            _httpClient.BaseAddress = new Uri(configuration.GetSection("Api").GetValue<string>("Endpoint"));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Device[] devices =
            {
                new Device(new Guid("7266b21b-7f83-4204-86f7-d7c2d615edac")),
                new Device(new Guid("bd0bfc50-37cc-4994-9451-f6d99adc0f7d")),
                new Device(new Guid("36cd50f0-fc01-4ddb-930d-011a7afcb417")),
                new Device(new Guid("82e82431-a01a-4c57-8de7-2f4536639fe4")),
                new Device(new Guid("89577c41-2d6a-4c7c-9540-e28d02088b70")),
                new Device(new Guid("773557ba-dfc3-4961-8b72-82a99a6b481b")),
                new Device(new Guid("79d7c725-63d9-4518-ba63-0cf50819e9fd")),
                new Device(new Guid("aa76d690-29c4-4696-999d-997a18e46e75")),
                new Device(new Guid("e3d573cb-19fa-4e6a-85e7-89ef6b50e44c")),
                new Device(new Guid("9af74f50-7bfd-4d9c-b1fc-9a6eea48b6f6"))
            };

            Engine.Run(devices, _httpClient, _configuration.GetSection("Api").GetValue<string>("SubscriptionKey"));
        }
    }
}
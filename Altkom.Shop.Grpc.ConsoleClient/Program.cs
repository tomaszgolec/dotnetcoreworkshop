using Altkom.Shop.GrpcServer;
using Bogus;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace Altkom.Shop.Grpc.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //dotnet add package Grpc.Net.Client
            Console.WriteLine("Hello gRPC Client!");

            //dotnet add package Bogus
            var locations = new Faker<AddLocationRequest>()
                .RuleFor(p => p.Name, f => f.Vehicle.Model())
                .RuleFor(p => p.Latitude, f => (float) f.Address.Latitude())
                .RuleFor(p => p.Longitude, f => (float) f.Address.Longitude())
                .RuleFor(p => p.Speed, f => f.Random.Int(0, 140))
                .RuleFor(p => p.Direction, f => f.Random.Float())
                .GenerateForever();


            const string url = "https://localhost:5001";
            GrpcChannel chanel = GrpcChannel.ForAddress(url);
            //todo do subskrypcji skopiuj sobie z repo
            var client = new TrackingService.TrackingServiceClient(chanel);

            foreach (var request in locations)
            {
                Console.WriteLine($"{request.Name} {request.Latitude} itd");
                var response = await client.AddLocationAsync(request);

                if (response.IsConfirmed)
                {
                    Console.WriteLine($"Confirmed {response.IsConfirmed}");
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            Console.WriteLine("exit");
            Console.ReadKey();

        }
    }
}

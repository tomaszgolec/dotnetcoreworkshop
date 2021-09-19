using Bogus;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altkom.Shop.GrpcServer.Services
{
    public class MyTrackingService : Altkom.Shop.GrpcServer.TrackingService.TrackingServiceBase // ta bazowa klasa jest na podstawie tego proto
    {

        private readonly ILogger<MyTrackingService> logger;

        public MyTrackingService(ILogger<MyTrackingService> logger)
        {
            this.logger = logger;
        }


        public override Task<AddLocationResponse> AddLocation(AddLocationRequest request, ServerCallContext context)
        {
            logger.LogInformation($"{request.Name} {request.Latitude} itd");

            var response = new AddLocationResponse { IsConfirmed = true };
            return Task.FromResult(response);
        }

        public override async Task Subscribe(SubscribeRequest request, IServerStreamWriter<SubscribeResponse> responseStream, ServerCallContext context)
        {

            var  locations  = new Faker<SubscribeResponse>()
              .RuleFor(p => p.Name, f => f.Vehicle.Model())
              .RuleFor(p => p.Latitude, f => (float)f.Address.Latitude())
              .RuleFor(p => p.Longitude, f => (float)f.Address.Longitude())
              .RuleFor(p => p.Speed, f => f.Random.Int(0, 140))
              .RuleFor(p => p.Direction, f => f.Random.Float())
              .GenerateForever();

            locations = locations.Where(location => location.Speed > request.SpeedLimit).Where(location => location.Name == request.Model);

            foreach (var location in locations)
            {
                if (context.CancellationToken.IsCancellationRequested)
                {
                    break;
                }
                await responseStream.WriteAsync(location);

                logger.LogInformation($"{location.Name} {location.Latitude} {location.Longitude} {location.Speed} {location.Direction}");
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

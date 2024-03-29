using Grpc.Core;
using Oponeo.CustomerManagement.GrpcService;

namespace Oponeo.CustomerManagement.GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Witaj " + request.Name
            });
        }
    }
}
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace GrpcServer.Interceptors
{
    public class LogInterceptor : Interceptor
    {
        private readonly ILogger<LogInterceptor> logger;

        public LogInterceptor(ILogger<LogInterceptor> logger)
        {
            this.logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            this.logger.LogWarning("Warn logs.");

            return continuation(request, context);
        }
    }
}
using System;
using Grpc.Core;
using Grpc.Core.Interceptors;

namespace GrpcClient.Interceptors
{
    public class LogInterceptor : Interceptor
    {
        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            Console.WriteLine("Logs");

            return continuation(request, context);
        }
    }
}
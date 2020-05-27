using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Grpc.Net.Compression;
using GrpcClient.Extensions;
using GrpcClient.Interceptors;
using GrpcServer.HumanResource;

namespace GrpcClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // 等待 Server 啟動
            await Task.Delay(5000);

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            // 建立連接到 gRPC 服務的通道
            var channel = GrpcChannel.ForAddress("http://192.168.0.188:5001");



            //var httpClient = new HttpClient(
            //    new HttpClientHandler
            //    {
            //        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            //    });

            //// 建立連接到 gRPC 服務的通道
            //var channel = GrpcChannel.ForAddress("https://192.168.0.188:5001", new GrpcChannelOptions { HttpClient = httpClient });



            //// 建立連接到 gRPC 服務的通道
            //var channel = GrpcChannel.ForAddress("https://localhost:5001");
            
            
            var callInvoker = channel.Intercept(new LogInterceptor());

            // 建立 EmployeeClient
            var client = new Employee.EmployeeClient(callInvoker);

            // 呼叫 GetEmployee()
            var employee = await client.GetEmployeeAsync(new EmployeeRequest { Id = 1 });

            // 輸出 EmployeeModel 的序列化結果
            Console.WriteLine(JsonSerializer.Serialize(employee, new JsonSerializerOptions { WriteIndented = true }));

            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Grpc.Net.Compression;
using GrpcClient.Extensions;
using GrpcServer.HumanResource;

namespace GrpcClient
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // 等待 Server 啟動
            await Task.Delay(5000);

            //var httpClient = new HttpClient(
            //    new HttpClientHandler
            //    {
            //        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            //    });

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            // 建立連接到 https://localhost:5001 的通道
            //var channel = GrpcChannel.ForAddress("https://192.168.1.168:5001", new GrpcChannelOptions { HttpClient = httpClient });
            var channel = GrpcChannel.ForAddress("http://192.168.0.132:5001");

            // 建立 EmployeeClient
            var client = new Employee.EmployeeClient(channel);

            // 呼叫 GetAllEmployees()
            var employeesStream = client.GetAllEmployees(new EmployeeRequest { Id = 0 });

            // 讀取 Employees 串流
            while (await employeesStream.ResponseStream.MoveNext())
            {
                var employee = employeesStream.ResponseStream.Current;

                // 輸出 EmployeeModel 的序列化結果
                Console.WriteLine(JsonSerializer.Serialize(employee, new JsonSerializerOptions { WriteIndented = true }));
            }

            Console.ReadKey();
        }
    }
}
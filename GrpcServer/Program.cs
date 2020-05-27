using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace GrpcServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                        {
                            webBuilder.UseKestrel(
                                serverOptions =>
                                    {
                                        serverOptions.Listen(IPAddress.Parse("192.168.0.188"), 5001);
                                    })
                                .UseStartup<Startup>();

                            //webBuilder.UseKestrel(
                            //    serverOptions =>
                            //        {
                            //            serverOptions.ListenLocalhost(
                            //                5001,
                            //                listenOptions =>
                            //                    {
                            //                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                            //                        listenOptions.UseHttps();
                            //                    });
                            //        })
                            //    .UseStartup<Startup>();

                            //webBuilder.UseKestrel(
                            //    serverOptions =>
                            //        {
                            //            serverOptions.Listen(
                            //                IPAddress.Parse("192.168.0.188"),
                            //                5001,
                            //                listenOptions =>
                            //                    {
                            //                        listenOptions.Protocols = HttpProtocols.Http2;
                            //                        listenOptions.UseHttps("kestrel.pfx", "changeit");
                            //                    });
                            //        })
                            //.UseStartup<Startup>();

                            //webBuilder.UseStartup<Startup>();
                        });
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using CluelessBackend.WebServer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CluelessBackend
{
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;

        public DisposableAction(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action.Invoke();
        }
    }

    public static class Program
    {
        public static List<object> SingletonServices { get; set; }
        public static bool WebsocketServerIsReady { get; set; } = false;
        public static bool IsWebsocketTest { get; private set; } = false;

        public static void Main(string[] args)
        {
            // TODO: Allow args to specify port number?
            // TODO: Load server configuration file
            // TODO: Start logging

            CreateHostBuilder(args).Build().Run();
        }

        public static IDisposable InitializeForWebsocketTests()
        {
            var cts = new CancellationTokenSource();
            IsWebsocketTest = true;
            Task.Run(() => { CreateHostBuilder(default).Build().Run(); }, cts.Token);
            var disposableAction = new DisposableAction(cts.Cancel);
            // Wait for initialization to complete
            while (!WebsocketServerIsReady) { }

            return disposableAction;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<WebServerStartup>(); });
    }
}
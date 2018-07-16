using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Wumpus.Rest.Tests;
using Wumpus.Serialization;
using Wumpus.Server;
using Xunit;

namespace Wumpus.Rest.Tests
{

    public abstract class BaseTest
    {
        private readonly WumpusJsonSerializer _serializer;

        public BaseTest()
        {
            _serializer = new WumpusJsonSerializer();
        }

        protected void RunTest(Func<WumpusRestClient, Task> action)
        {
            const string url = "http://localhost:34560/";

            var client = new WumpusRestClient(url, _serializer);
            var server = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();
            server.Start();
            try
            { 
                var requestTask = action(client);
                var timeoutTask = Task.Delay(3000);
                var task = Task.WhenAny(requestTask, timeoutTask).Result;
                if (task == timeoutTask)
                    throw new TimeoutException();
                requestTask.GetAwaiter().GetResult();
            }
            finally { server.StopAsync().GetAwaiter().GetResult(); }
        }

        protected void RunTest<T>(Func<WumpusRestClient, Task<T>> action, T expectedResponse)
        {
            const string url = "http://localhost:34560/";

            var client = new WumpusRestClient(url, _serializer);
            var server = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();
            server.Start();
            try
            {
                var requestTask = action(client);
                var timeoutTask = Task.Delay(3000);
                var task = Task.WhenAny(requestTask, timeoutTask).Result;
                if (task == timeoutTask)
                    throw new TimeoutException();
                var response = requestTask.GetAwaiter().GetResult();
                Assert.Equal(expectedResponse, response, RecursiveComparer<T>.Instance);
            }
            finally { server.StopAsync().GetAwaiter().GetResult(); }
        }
    }
}
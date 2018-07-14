using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Wumpus.Net.Server;
using Wumpus.Serialization;
using Xunit;

namespace Wumpus.Net.Tests
{
    public class TestData
    {
        public Func<WumpusRestClient, Task> Action { get; }

        public TestData(Func<WumpusRestClient, Task> action)
        {
            Action = action;
        }
    }

    public class TestData<TResponse>
    {
        public Func<WumpusRestClient, Task<TResponse>> Action { get; }
        public TResponse Response { get; }

        public TestData(Func<WumpusRestClient, Task<TResponse>> action, TResponse response)
        {
            Action = action;
            Response = response;
        }
    }

    public abstract class BaseTest
    {
        private readonly WumpusJsonSerializer _serializer;

        public BaseTest()
        {
            _serializer = new WumpusJsonSerializer();
        }

        protected void RunTest(TestData test)
        {
            const string url = "http://localhost:34560/";

            var client = new WumpusRestClient(url, _serializer);
            var server = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();
            server.Start();

            var requestTask = test.Action(client);
            var timeoutTask = Task.Delay(3000);
            var task = Task.WhenAny(requestTask, timeoutTask).Result;
            if (task == timeoutTask)
                throw new TimeoutException();
            if (requestTask.IsFaulted)
                throw requestTask.Exception;
        }

        public static object[] Test(Func<WumpusRestClient, Task> action)
          => new object[] { new TestData(action) };
    }

    public abstract class BaseTest<TResponse>
    {
        private readonly WumpusJsonSerializer _serializer;
        private readonly IEqualityComparer<TResponse> _comparer;

        public BaseTest(IEqualityComparer<TResponse> comparer = null)
        {
            _serializer = new WumpusJsonSerializer();
            _comparer = comparer ?? EqualityComparer<TResponse>.Default;
        }

        protected void RunTest(TestData<TResponse> test)
        {
            const string url = "http://localhost:34560/";

            var client = new WumpusRestClient(url, _serializer);
            var server = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseUrls(url)
                .Build();
            server.Start();

            var requestTask = test.Action(client);
            var timeoutTask = Task.Delay(3000);
            var task = Task.WhenAny(requestTask, timeoutTask).Result;
            if (task == timeoutTask)
                throw new TimeoutException();
            var response = requestTask.GetAwaiter().GetResult();
            Assert.Equal(requestTask.Result, response, _comparer);
        }

        public static object[] Test(Func<WumpusRestClient, Task<TResponse>> action, TResponse response)
          => new object[] { new TestData<TResponse>(action, response) };
    }
}
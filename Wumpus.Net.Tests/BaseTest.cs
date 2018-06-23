using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Voltaic.Serialization.Json;
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
        private readonly JsonSerializer _serializer;
        protected readonly HttpListener _server;
        private readonly WumpusRestClient _client;

        public BaseTest()
        {
            _serializer = new JsonSerializer();

            const string url = "http://localhost:34560/";
            _server = new HttpListener();
            _server.Prefixes.Add(url);
            _client = new WumpusRestClient(url, _serializer);
        }

        protected void RunTest(TestData test)
        {
            _server.Start();
            try
            {
                var listenTask = _server.GetContextAsync().ContinueWith(c =>
                {
                    var serverResponse = c.Result.Response;
                    serverResponse.StatusCode = (int)HttpStatusCode.NoContent;
                    serverResponse.Close();
                });
                var requestTask = test.Action(_client);
                Task.WaitAll(requestTask, listenTask);
            }
            finally { _server.Stop(); }
        }

        public static object[] Test(Func<WumpusRestClient, Task> action)
          => new object[] { new TestData(action) };
    }

    public abstract class BaseTest<TResponse>
    {
        private readonly JsonSerializer _serializer;
        private readonly HttpListener _server;
        private readonly WumpusRestClient _client;
        private readonly IEqualityComparer<TResponse> _comparer;

        public BaseTest(IEqualityComparer<TResponse> comparer = null)
        {
            _serializer = new JsonSerializer();

            const string url = "http://localhost:34560/";
            _server = new HttpListener();
            _server.Prefixes.Add(url);
            _client = new WumpusRestClient(url, _serializer);
            _comparer = comparer ?? EqualityComparer<TResponse>.Default;
        }

        protected void RunTest(TestData<TResponse> test)
        {
            _server.Start();
            try
            {
                var listenTask = _server.GetContextAsync().ContinueWith(c =>
                {
                    var serverResponse = c.Result.Response;
                    serverResponse.StatusCode = (int)HttpStatusCode.OK;
                    serverResponse.Close();
                });
                var requestTask = test.Action(_client);
                Task.WaitAll(requestTask, listenTask);
                Assert.Equal(requestTask.Result, test.Response, _comparer);
            }
            finally { _server.Stop(); }
        }

        public static object[] Test(Func<WumpusRestClient, Task<TResponse>> action, TResponse response)
          => new object[] { new TestData<TResponse>(action, response) };
    }
}
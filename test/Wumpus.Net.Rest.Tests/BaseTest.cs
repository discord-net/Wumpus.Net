using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Wumpus.Serialization;
using Wumpus.Server;

namespace Wumpus.Rest.Tests
{
    public abstract class BaseTest
    {
        private class RecursiveComparer<T> : IEqualityComparer<T>
        {
            public static RecursiveComparer<T> Instance { get; } = new RecursiveComparer<T>();

            public bool Equals(T actual, T expected) => NonTypedEquals(typeof(T), actual, expected);

            private bool NonTypedEquals(Type type, object actual, object expected)
            {
                if (type.IsPrimitive)
                    return actual.Equals(expected);
                else if (type.IsArray)
                {
                    var seq1 = (actual as IEnumerable).Cast<object>().ToArray();
                    var seq2 = (expected as IEnumerable).Cast<object>().ToArray();
                    if (seq1.Length != seq2.Length)
                        return false;
                    for (int i = 0; i < seq1.Length; i++)
                    {
                        if (!NonTypedEquals(type.GetElementType(), seq1[i], seq2[i]))
                            return false;
                    }
                    return true;
                }
                else
                {
                    var fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (var field in fields)
                    {
                        var innerExpectedValue = field.GetValue(expected);
                        var innerActualValue = field.GetValue(actual);
                        if (innerExpectedValue is null && innerActualValue is null)
                            continue; // true
                        if (innerActualValue is null || innerExpectedValue is null)
                            return false;
                        if (!NonTypedEquals(field.FieldType, innerActualValue, innerExpectedValue))
                            return false;
                    }
                    return true;
                }
            }

            public int GetHashCode(T parameterValue) => 0; // Ignore
        }

        private readonly WumpusJsonSerializer _serializer;

        public BaseTest()
        {
            _serializer = new WumpusJsonSerializer();
        }

        protected void RunTest(Func<WumpusRestClient, Task> action)
        {
            CreateServer(out var server, out string url);
            try
            {
                var client = new WumpusRestClient(url, _serializer);
                var requestTask = action(client);
                var timeoutTask = Task.Delay(3000);
                var task = Task.WhenAny(requestTask, timeoutTask).Result;
                if (task == timeoutTask)
                    throw new TimeoutException();
                requestTask.GetAwaiter().GetResult();
            }
            finally { server.StopAsync().GetAwaiter().GetResult(); }
        }

        protected void RunTest<T>(Func<WumpusRestClient, Task<T>> action, Action<T> validateAction)
        {
            CreateServer(out var server, out string url);
            try
            {
                var client = new WumpusRestClient(url, _serializer);
                var requestTask = action(client);
                var timeoutTask = Task.Delay(3000);
                var task = Task.WhenAny(requestTask, timeoutTask).Result;
                if (task == timeoutTask)
                    throw new TimeoutException();
                var response = requestTask.GetAwaiter().GetResult();
                validateAction(response);
            }
            finally { server.StopAsync().GetAwaiter().GetResult(); }
        }

        private void CreateServer(out IWebHost server, out string url)
        {
            int port;
            for (port = 34560; port < ushort.MaxValue; port++)
            {
                try
                {
                    url = $"http://localhost:{port}";
                    server = WebHost.CreateDefaultBuilder()
                        .UseStartup<Startup>()
                        .UseUrls($"http://localhost:{port}")
                        .Build();
                    server.Start();
                    return;
                }
                catch (IOException ex) when (ex.InnerException is AddressInUseException) { continue; }
            }
            throw new InvalidOperationException("Failed to find free port for this test's REST server");
        }
    }
}
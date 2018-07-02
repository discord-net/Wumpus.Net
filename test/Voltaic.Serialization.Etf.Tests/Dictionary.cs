using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class DictionaryTests : BaseTest<Dictionary<string, int>>
    {
        private class Comparer : IEqualityComparer<Dictionary<string, int>>
        {
            public bool Equals(Dictionary<string, int> x, Dictionary<string, int> y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;
                foreach (var pair in x)
                {
                    if (!y.ContainsKey(pair.Key) || y[pair.Key] != pair.Value)
                        return false;
                }
                foreach (var pair in y)
                {
                    if (!x.ContainsKey(pair.Key) || x[pair.Key] != pair.Value)
                        return false;
                }
                return true;
            }
            public int GetHashCode(Dictionary<string, int> obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            throw new NotImplementedException();
        }

        public DictionaryTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Object(BinaryTestData<Dictionary<string, int>> data) => RunTest(data);
    }
}

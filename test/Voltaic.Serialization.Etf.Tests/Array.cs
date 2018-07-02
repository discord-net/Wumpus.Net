using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class ArrayTests : BaseTest<int[]>
    {
        private class Comparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y) => (x == null && y == null) || (x != null && y != null && x.SequenceEqual(y));
            public int GetHashCode(int[] obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            throw new NotImplementedException();
        }

        public ArrayTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Array(BinaryTestData<int[]> data) => RunTest(data);
    }

    public class ListTests : BaseTest<List<int>>
    {
        private class Comparer : IEqualityComparer<List<int>>
        {
            public bool Equals(List<int> x, List<int> y) => (x == null && y == null) || (x != null && y != null && x.SequenceEqual(y));
            public int GetHashCode(List<int> obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            throw new NotImplementedException();
        }

        public ListTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Array(BinaryTestData<List<int>> data) => RunTest(data);
    }
}

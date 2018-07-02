using System.Collections.Generic;
using System.Linq;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
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
            yield return ReadWrite("null", null);

            yield return ReadWrite("[]", new int[0]);
            yield return ReadWrite("[1]", new int[] { 1 });
            yield return ReadWrite("[1,2,3]", new int[] { 1, 2, 3 });
            yield return Read("[1, 2, 3]", new int[] { 1, 2, 3 });
            yield return Read("[1 ,2 ,3]", new int[] { 1, 2, 3 });
            yield return Read("[1 , 2 , 3]", new int[] { 1, 2, 3 });
            yield return Read("[1  ,  2  ,  3]", new int[] { 1, 2, 3 });
            yield return FailRead("[1,]");
            yield return FailRead("[,1]");
            yield return FailRead("[");
            yield return FailRead("[1");
            yield return FailRead("]");
            yield return FailRead("1]");
            yield return FailRead("[1 2 3]");
            yield return FailRead("[1   2   3]");
            yield return FailRead("[1:2:3]");
            yield return FailRead("[1 : 2 : 3]");
            yield return FailRead("[1 :  2  :  3]");
        }

        public ArrayTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Array(TextTestData<int[]> data) => RunTest(data);
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
            yield return ReadWrite("null", null);

            yield return ReadWrite("[]", new List<int>());
            yield return ReadWrite("[1]", new List<int> { 1 });
            yield return ReadWrite("[1,2,3]", new List<int> { 1, 2, 3 });
            yield return Read("[1, 2, 3]", new List<int> { 1, 2, 3 });
            yield return Read("[1 ,2 ,3]", new List<int> { 1, 2, 3 });
            yield return Read("[1 , 2 , 3]", new List<int> { 1, 2, 3 });
            yield return Read("[1  ,  2  ,  3]", new List<int> { 1, 2, 3 });
            yield return FailRead("[1,]");
            yield return FailRead("[,1]");
            yield return FailRead("[");
            yield return FailRead("[1");
            yield return FailRead("]");
            yield return FailRead("1]");
            yield return FailRead("[1 2 3]");
            yield return FailRead("[1   2   3]");
            yield return FailRead("[1:2:3]");
            yield return FailRead("[1 : 2 : 3]");
            yield return FailRead("[1 :  2  :  3]");
        }

        public ListTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Array(TextTestData<List<int>> data) => RunTest(data);
    }
}

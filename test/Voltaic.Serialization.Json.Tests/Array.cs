using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class ArrayTests : BaseTest<int[]>
    {
        private class Comparer : IEqualityComparer<int[]>
        {
            public bool Equals(int[] x, int[] y) => x.SequenceEqual(y);
            public int GetHashCode(int[] obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);

            yield return ReadWrite("[]", new int[0]);
            yield return ReadWrite("[1]", new int[] { 1 });
            yield return ReadWrite("[1,2,3]", new int[] { 1, 2, 3 });
            yield return ReadWrite("[1, 2, 3]", new int[] { 1, 2, 3 });
            yield return ReadWrite("[1 ,2 ,3]", new int[] { 1, 2, 3 });
            yield return ReadWrite("[1 , 2 , 3]", new int[] { 1, 2, 3 });
            yield return ReadWrite("[1  ,  2  ,  3]", new int[] { 1, 2, 3 });
            yield return Fail("[1,]");
            yield return Fail("[,1]");
            yield return Fail("[");
            yield return Fail("[1");
            yield return Fail("]");
            yield return Fail("1]");
            yield return Fail("[1 2 3]");
            yield return Fail("[1   2   3]");
            yield return Fail("[1:2:3]");
            yield return Fail("[1 : 2 : 3]");
            yield return Fail("[1 :  2  :  3]");
        }

        public ArrayTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Test(TestData data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetData))]
        public void TestWhitespace(TestData data) => RunWhitespaceTest(data);
    }
}

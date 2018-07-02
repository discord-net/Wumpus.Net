using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class TestClass1
    {
        [ModelProperty("int")]
        public ulong Int { get; set; }
        [ModelProperty("sub_class")]
        public TestClass2 SubClass { get; set; }
    }
    public class TestClass2
    {
        [ModelProperty("str")]
        public Optional<string> Str { get; set; }
        [ModelProperty("bool"), StandardFormat('G')]
        public bool? Bool { get; set; }
    }

    public class ObjectTests : BaseTest<TestClass1>
    {
        private class Comparer : IEqualityComparer<TestClass1>
        {
            public bool Equals(TestClass1 x, TestClass1 y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;
                if (x.Int != y.Int)
                    return false;
                if (x.SubClass == null && y.SubClass == null)
                    return true;
                if (x.SubClass == null || y.SubClass == null)
                    return false;
                if (x.SubClass.Bool != y.SubClass.Bool)
                    return false;
                return true;
            }
            public int GetHashCode(TestClass1 obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            throw new NotImplementedException();
        }

        public ObjectTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Object(BinaryTestData<TestClass1> data) => RunTest(data);
    }
}

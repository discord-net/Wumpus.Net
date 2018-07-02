using System.Collections.Generic;
using Voltaic.Serialization.Utf8.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class TestClass1
    {
        [ModelProperty("int"), Int53]
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
            yield return ReadWrite("null", null);

            yield return Read("{}",
                new TestClass1 { Int = default, SubClass = default });
            yield return ReadWrite("{\"int\":123,\"sub_class\":{\"str\":\"123\",\"bool\":\"True\"}}",
                new TestClass1 { Int = 123, SubClass = new TestClass2 { Str = "123", Bool = true } });
            yield return ReadWrite("{\"int\":123,\"sub_class\":{\"bool\":\"True\"}}",
                new TestClass1 { Int = 123, SubClass = new TestClass2 { Bool = true } });
        }

        public ObjectTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Object(TextTestData<TestClass1> data) => RunTest(data);
    }
}

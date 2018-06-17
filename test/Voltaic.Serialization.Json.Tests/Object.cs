using System.Collections.Generic;
using Voltaic.Serialization.Tests;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    internal class TestClass1
    {
        [Key("int"), Int53]
        public ulong Int { get; set; }
        [Key("sub_class")]
        public TestClass2 SubClass { get; set; }
    }
    internal class TestClass2
    {
        [Key("str")]
        public Optional<string> Str { get; set; }
        [Key("bool"), StandardFormat('G')]
        public bool? Bool { get; set; }
    }

    public class ObjectTests : BaseTest<object>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite("null", null);

            yield return ReadWrite("{\"int\":123,\"sub_class\":{\"str\":\"123\",\"bool\":True}}",
                new TestClass1 { Int = 123, SubClass = new TestClass2 { Str = "123", Bool = true } });
            yield return ReadWrite("{\"int\":123,\"sub_class\":{\"bool\":True}}",
                new TestClass1 { Int = 123, SubClass = new TestClass2 { Bool = true } });
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Object(TestData<object> data) => RunTest(data);
    }
}

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
            yield return ReadWrite(EtfTokenType.SmallAtom, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil

            yield return ReadWrite(EtfTokenType.Map, new byte[]
            {
                0x00, 0x00, 0x00, 0x02, // 1 element
                0x6D, 0x00, 0x00, 0x00, 0x03, 0x69, 0x6E, 0x74, // int
                0x61, 0x01, // = 1
                0x6D, 0x00, 0x00, 0x00, 0x09, 0x73, 0x75, 0x62, 0x5F, 0x63, 0x6C, 0x61, 0x73, 0x73, // sub_class
                0x73, 0x03, 0x6E, 0x69, 0x6C // = nil
            }, new TestClass1 { Int = 1, SubClass = null });
            yield return ReadWrite(EtfTokenType.Map, new byte[]
            {
                0x00, 0x00, 0x00, 0x02, // 2 elements
                0x6D, 0x00, 0x00, 0x00, 0x03, 0x69, 0x6E, 0x74, // int
                0x61, 0x01, // = 1
                0x6D, 0x00, 0x00, 0x00, 0x09, 0x73, 0x75, 0x62, 0x5F, 0x63, 0x6C, 0x61, 0x73, 0x73, // sub_class
                0x74, 0x00, 0x00, 0x00, 0x01, // 1 element
                0x6D, 0x00, 0x00, 0x00, 0x04, 0x62, 0x6F, 0x6F, 0x6C, // bool
                0x6D, 0x00, 0x00, 0x00, 0x04, 0x54, 0x72, 0x75, 0x65 // = "True"
            }, new TestClass1 { Int = 1, SubClass = new TestClass2 { Bool = true } });
            yield return ReadWrite(EtfTokenType.Map, new byte[]
            {
                0x00, 0x00, 0x00, 0x02, // 2 elements
                0x6D, 0x00, 0x00, 0x00, 0x03, 0x69, 0x6E, 0x74, // int
                0x61, 0x01, // = 1
                0x6D, 0x00, 0x00, 0x00, 0x09, 0x73, 0x75, 0x62, 0x5F, 0x63, 0x6C, 0x61, 0x73, 0x73, // sub_class
                0x74, 0x00, 0x00, 0x00, 0x02, // 2 elements
                0x6D, 0x00, 0x00, 0x00, 0x03, 0x73, 0x74, 0x72, // str
                0x6D, 0x00, 0x00, 0x00, 0x02, 0x68, 0x69, // = "hi"
                0x6D, 0x00, 0x00, 0x00, 0x04, 0x62, 0x6F, 0x6F, 0x6C, // bool
                0x73, 0x03, 0x6E, 0x69, 0x6C // = nil
            }, new TestClass1 { Int = 1, SubClass = new TestClass2 { Str = "hi" } });
        }

        public ObjectTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Object(BinaryTestData<TestClass1> data) => RunTest(data);
    }
}

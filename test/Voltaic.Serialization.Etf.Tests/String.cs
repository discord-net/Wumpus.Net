using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class CharTests : BaseTest<char>
    {
        public static IEnumerable<object[]> GetStringData()
        {
            yield return FailRead(EtfTokenType.String, new byte[] { });
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x00 });
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x01, 0x61 }, 'a');
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x02, 0x61, 0x61 });
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x01, 0x00 }, '\0');
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x03, 0xE2, 0x98, 0x91 }, '☑');
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C });
        }
        public static IEnumerable<object[]> GetBinaryData()
        {
            yield return FailRead(EtfTokenType.Binary, new byte[] { });
            yield return FailRead(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x00 });
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x61 }, 'a');
            yield return FailRead(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x02, 0x61, 0x61 });
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00 }, '\0');
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x03, 0xE2, 0x98, 0x91 }, '☑');
            yield return FailRead(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C });
        }
        public static IEnumerable<object[]> GetAtomData()
        {
            yield return FailRead(EtfTokenType.Atom, new byte[] { });
            yield return FailRead(EtfTokenType.Atom, new byte[] { 0x00, 0x00 });
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x01, 0x61 }, 'a');
            yield return FailRead(EtfTokenType.Atom, new byte[] { 0x00, 0x02, 0x61, 0x61 });
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x01, 0x00 }, '\0');
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0xE2, 0x98, 0x91 }, '☑');
            yield return FailRead(EtfTokenType.Atom, new byte[] { 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C });
        }
        public static IEnumerable<object[]> GetAtomUtf8Data()
        {
            yield return FailRead(EtfTokenType.AtomUtf8, new byte[] { });
            yield return FailRead(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x00 });
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x01, 0x61 }, 'a');
            yield return FailRead(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x02, 0x61, 0x61 });
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x01, 0x00 }, '\0');
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0xE2, 0x98, 0x91 }, '☑');
            yield return FailRead(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C });
        }
        public static IEnumerable<object[]> GetSmallAtomData()
        {
            yield return FailRead(EtfTokenType.SmallAtom, new byte[] { });
            yield return FailRead(EtfTokenType.SmallAtom, new byte[] { 0x00 });
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x01, 0x61 }, 'a');
            yield return FailRead(EtfTokenType.SmallAtom, new byte[] { 0x02, 0x61, 0x61 });
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x01, 0x00 }, '\0');
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x03, 0xE2, 0x98, 0x91 }, '☑');
            yield return FailRead(EtfTokenType.SmallAtom, new byte[] { 0x04, 0xF0, 0x9F, 0x91, 0x8C });
        }
        public static IEnumerable<object[]> GetSmallAtomUtf8Data()
        {
            yield return FailRead(EtfTokenType.SmallAtomUtf8, new byte[] { });
            yield return FailRead(EtfTokenType.SmallAtomUtf8, new byte[] { 0x00 });
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x01, 0x61 }, 'a');
            yield return FailRead(EtfTokenType.SmallAtomUtf8, new byte[] { 0x02, 0x61, 0x61 });
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x01, 0x00 }, '\0');
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0xE2, 0x98, 0x91 }, '☑');
            yield return FailRead(EtfTokenType.SmallAtomUtf8, new byte[] { 0x04, 0xF0, 0x9F, 0x91, 0x8C });
        }

        [Theory]
        [MemberData(nameof(GetStringData))]
        public void String(TestData<char> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetBinaryData))]
        public void Binary(TestData<char> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetAtomData))]
        public void Atom(TestData<char> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetAtomUtf8Data))]
        public void AtomUtf8(TestData<char> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetSmallAtomData))]
        public void SmallAtom(TestData<char> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetSmallAtomUtf8Data))]
        public void SmallAtomUtf8(TestData<char> data) => RunTest(data);
    }

    public class StringTests : BaseTest<string>
    {
        public static IEnumerable<object[]> GetStringData()
        {
            yield return FailRead(EtfTokenType.String, new byte[] { });
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x00 }, "");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x01, 0x61 }, "a");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x02, 0x61, 0x61 }, "aa");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x01, 0x00 }, "\0");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x03, 0x61, 0x00, 0x62 }, "a\0b");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x03, 0xE2, 0x98, 0x91 }, "☑");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x05, 0x61, 0xE2, 0x98, 0x91, 0x62 }, "a☑b");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C }, "👌");
            yield return Read(EtfTokenType.String, new byte[] { 0x00, 0x06, 0x61, 0xF0, 0x9F, 0x91, 0x8C, 0x62 }, "a👌b");
        }
        public static IEnumerable<object[]> GetBinaryData()
        {
            yield return FailRead(EtfTokenType.Binary, new byte[] { });
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x00 }, "");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x61 }, "a");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x02, 0x61, 0x61 }, "aa");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x01, 0x00 }, "\0");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x03, 0x61, 0x00, 0x62 }, "a\0b");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x03, 0xE2, 0x98, 0x91 }, "☑");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x05, 0x61, 0xE2, 0x98, 0x91, 0x62 }, "a☑b");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C }, "👌");
            yield return ReadWrite(EtfTokenType.Binary, new byte[] { 0x00, 0x00, 0x00, 0x06, 0x61, 0xF0, 0x9F, 0x91, 0x8C, 0x62 }, "a👌b");
        }
        public static IEnumerable<object[]> GetAtomData()
        {
            yield return FailRead(EtfTokenType.Atom, new byte[] { });
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x00 }, "");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x01, 0x61 }, "a");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x02, 0x61, 0x61 }, "aa");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x01, 0x00 }, "\0");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0x61, 0x00, 0x62 }, "a\0b");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0xE2, 0x98, 0x91 }, "☑");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x05, 0x61, 0xE2, 0x98, 0x91, 0x62 }, "a☑b");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C }, "👌");
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x06, 0x61, 0xF0, 0x9F, 0x91, 0x8C, 0x62 }, "a👌b");
        }
        public static IEnumerable<object[]> GetAtomUtf8Data()
        {
            yield return FailRead(EtfTokenType.AtomUtf8, new byte[] { });
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x00 }, "");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x01, 0x61 }, "a");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x02, 0x61, 0x61 }, "aa");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x01, 0x00 }, "\0");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0x61, 0x00, 0x62 }, "a\0b");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0xE2, 0x98, 0x91 }, "☑");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x05, 0x61, 0xE2, 0x98, 0x91, 0x62 }, "a☑b");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C }, "👌");
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x06, 0x61, 0xF0, 0x9F, 0x91, 0x8C, 0x62 }, "a👌b");
        }
        public static IEnumerable<object[]> GetSmallAtomData()
        {
            yield return FailRead(EtfTokenType.SmallAtom, new byte[] { });
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x00 }, "");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x01, 0x61 }, "a");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x02, 0x61, 0x61 }, "aa");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x01, 0x00 }, "\0");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x03, 0x61, 0x00, 0x62 }, "a\0b");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x03, 0xE2, 0x98, 0x91 }, "☑");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x05, 0x61, 0xE2, 0x98, 0x91, 0x62 }, "a☑b");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x04, 0xF0, 0x9F, 0x91, 0x8C }, "👌");
            yield return Read(EtfTokenType.SmallAtom, new byte[] { 0x06, 0x61, 0xF0, 0x9F, 0x91, 0x8C, 0x62 }, "a👌b");
        }
        public static IEnumerable<object[]> GetSmallAtomUtf8Data()
        {
            yield return FailRead(EtfTokenType.SmallAtomUtf8, new byte[] { });
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x00 }, "");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x01, 0x61 }, "a");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x02, 0x61, 0x61 }, "aa");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x01, 0x00 }, "\0");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0x61, 0x00, 0x62 }, "a\0b");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0xE2, 0x98, 0x91 }, "☑");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x05, 0x61, 0xE2, 0x98, 0x91, 0x62 }, "a☑b");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x04, 0xF0, 0x9F, 0x91, 0x8C }, "👌");
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x06, 0x61, 0xF0, 0x9F, 0x91, 0x8C, 0x62 }, "a👌b");
        }

        [Theory]
        [MemberData(nameof(GetStringData))]
        public void String(TestData<string> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetBinaryData))]
        public void Binary(TestData<string> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetAtomData))]
        public void Atom(TestData<string> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetAtomUtf8Data))]
        public void AtomUtf8(TestData<string> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetSmallAtomData))]
        public void SmallAtom(TestData<string> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetSmallAtomUtf8Data))]
        public void SmallAtomUtf8(TestData<string> data) => RunTest(data);
    }
}

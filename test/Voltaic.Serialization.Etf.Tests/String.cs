using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class CharTests : BaseTest<char>
    {
        public static IEnumerable<object[]> GetStringData()
        {
            yield return FailRead(EtfTokenType.String, new byte[] { });
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x00 }); // ""
            foreach (var x in  Reads("a", 'a')) yield return x;
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x02, 0x61, 0x61 }); // "aa"
            foreach (var x in Reads("\0", '\0')) yield return x;
            foreach (var x in Reads("☑", '☑')) yield return x;
            yield return FailRead(EtfTokenType.String, new byte[] { 0x00, 0x04, 0xF0, 0x9F, 0x91, 0x8C }); // "👌"
        }

        [Theory]
        [MemberData(nameof(GetStringData))]
        public void String(BinaryTestData<char> data) => RunTest(data);
    }

    public class StringTests : BaseTest<string>
    {
        public static IEnumerable<object[]> GetStringData()
        {
            yield return FailRead(EtfTokenType.SmallAtom, new byte[] { });
            yield return FailRead(EtfTokenType.SmallAtomUtf8, new byte[] { });
            yield return FailRead(EtfTokenType.Atom, new byte[] { });
            yield return FailRead(EtfTokenType.AtomUtf8, new byte[] { });
            yield return FailRead(EtfTokenType.String, new byte[] { });
            yield return FailRead(EtfTokenType.Binary, new byte[] { });
            foreach (var x in Reads("", "")) yield return x;
            foreach (var x in Reads("a", "a")) yield return x;
            foreach (var x in Reads("aa", "aa")) yield return x;
            foreach (var x in Reads("\0", "\0")) yield return x;
            foreach (var x in Reads("a\0b", "a\0b")) yield return x;
            foreach (var x in Reads("☑", "☑")) yield return x;
            foreach (var x in Reads("a☑b", "a☑b")) yield return x;
            foreach (var x in Reads("👌", "👌")) yield return x;
            foreach (var x in Reads("a👌b", "a👌b")) yield return x;
        }

        [Theory]
        [MemberData(nameof(GetStringData))]
        public void String(BinaryTestData<string> data) => RunTest(data);
    }
}

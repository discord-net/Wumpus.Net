using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class BooleanTests : BaseTest<bool>
    {
        public static IEnumerable<object[]> GetSmallAtomData()
        {
            yield return ReadWrite(EtfTokenType.SmallAtom, new byte[] { 0x04, 0x74, 0x72, 0x75, 0x65 }, true);
            yield return ReadWrite(EtfTokenType.SmallAtom, new byte[] { 0x05, 0x66, 0x61, 0x6C, 0x73, 0x65 }, false);
        }
        public static IEnumerable<object[]> GetSmallAtomUtf8Data()
        {
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x04, 0x74, 0x72, 0x75, 0x65 }, true);
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x05, 0x66, 0x61, 0x6C, 0x73, 0x65 }, false);
        }
        public static IEnumerable<object[]> GetAtomData()
        {
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x04, 0x74, 0x72, 0x75, 0x65 }, true);
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x05, 0x66, 0x61, 0x6C, 0x73, 0x65 }, false);
        }
        public static IEnumerable<object[]> GetAtomUtf8Data()
        {
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x04, 0x74, 0x72, 0x75, 0x65 }, true);
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x05, 0x66, 0x61, 0x6C, 0x73, 0x65 }, false);
        }
        public static IEnumerable<object[]> GetGData() => TextToBinary(Utf8.Tests.BooleanTests.GetGData());
        public static IEnumerable<object[]> GetLittleLData() => TextToBinary(Utf8.Tests.BooleanTests.GetLittleLData());

        [Theory]
        [MemberData(nameof(GetSmallAtomData))]
        public void SmallAtom(BinaryTestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetSmallAtomUtf8Data))]
        public void SmallAtomUtf8(BinaryTestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetAtomData))]
        public void Atom(BinaryTestData<bool> data) => RunTest(data);

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(BinaryTestData<bool> data) => RunTest(data, new BooleanEtfConverter('G'));
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(BinaryTestData<bool> data) => RunTest(data, new BooleanEtfConverter('l'));

    }
}

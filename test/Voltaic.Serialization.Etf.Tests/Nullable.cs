using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class NullableTests : BaseTest<int?>
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite(EtfTokenType.SmallAtom, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil

            yield return ReadWrite(EtfTokenType.Integer, new byte[] { 0x7F, 0xFF, 0xFF, 0xFF }, 2147483647);
        }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Nullable(BinaryTestData<int?> data) => RunTest(data);
    }
}

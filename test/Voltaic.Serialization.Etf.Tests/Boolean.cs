using System.Collections.Generic;
using Voltaic.Serialization.Etf;
using Xunit;

namespace Voltaic.Serialization.Json.Tests
{
    public class BooleanTests : BaseTest<bool>
    {
        public static IEnumerable<object[]> GetSmallAtomExtData()
        {
            yield return Read(new byte[] { 131, (byte)EtfTokenType.SmallAtomExt,
                4, (byte)'t', (byte)'r', (byte)'u', (byte)'e' }, 
                true);
            yield return Read(new byte[] { 131, (byte)EtfTokenType.SmallAtomExt,
                5, (byte)'f', (byte)'a', (byte)'l', (byte)'s', (byte)'e' }, 
                false);
        }
        public static IEnumerable<object[]> GetSmallAtomUtf8ExtData()
        {
            yield return Read(new byte[] { 131, (byte)EtfTokenType.SmallAtomUtf8Ext,
                4, (byte)'t', (byte)'r', (byte)'u', (byte)'e' },
                true);
            yield return Read(new byte[] { 131, (byte)EtfTokenType.SmallAtomUtf8Ext,
                5, (byte)'f', (byte)'a', (byte)'l', (byte)'s', (byte)'e' },
                false);
        }
        public static IEnumerable<object[]> GetAtomExtData()
        {
            yield return Read(new byte[] { 131, (byte)EtfTokenType.AtomExt,
                0, 4, (byte)'t', (byte)'r', (byte)'u', (byte)'e' },
                true);
            yield return Read(new byte[] { 131, (byte)EtfTokenType.AtomExt,
                0, 5, (byte)'f', (byte)'a', (byte)'l', (byte)'s', (byte)'e' },
                false);
        }
        public static IEnumerable<object[]> GetAtomUtf8ExtData()
        {
            yield return Read(new byte[] { 131, (byte)EtfTokenType.AtomUtf8Ext,
                0, 4, (byte)'t', (byte)'r', (byte)'u', (byte)'e' },
                true);
            yield return Read(new byte[] { 131, (byte)EtfTokenType.AtomUtf8Ext,
                0, 5, (byte)'f', (byte)'a', (byte)'l', (byte)'s', (byte)'e' },
                false);
        }

        [Theory]
        [MemberData(nameof(GetSmallAtomExtData))]
        public void SmallAtomExt(TestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetSmallAtomUtf8ExtData))]
        public void SmallAtomUtf8Ext(TestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetAtomExtData))]
        public void AtomExt(TestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetAtomUtf8ExtData))]
        public void AtomUtf8Ext(TestData<bool> data) => RunTest(data);

    }
}

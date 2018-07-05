using System;
using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Etf.Tests
{
    public class DictionaryTests : BaseTest<Dictionary<string, int>>
    {
        private class Comparer : IEqualityComparer<Dictionary<string, int>>
        {
            public bool Equals(Dictionary<string, int> x, Dictionary<string, int> y)
            {
                if (x == null && y == null)
                    return true;
                if (x == null || y == null)
                    return false;
                foreach (var pair in x)
                {
                    if (!y.ContainsKey(pair.Key) || y[pair.Key] != pair.Value)
                        return false;
                }
                foreach (var pair in y)
                {
                    if (!x.ContainsKey(pair.Key) || x[pair.Key] != pair.Value)
                        return false;
                }
                return true;
            }
            public int GetHashCode(Dictionary<string, int> obj) => 0; // Ignore
        }

        public static IEnumerable<object[]> GetData()
        {
            yield return ReadWrite(EtfTokenType.SmallAtom, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.SmallAtomUtf8, new byte[] { 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.Atom, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil
            yield return Read(EtfTokenType.AtomUtf8, new byte[] { 0x00, 0x03, 0x6E, 0x69, 0x6C }, null); // nil

            yield return ReadWrite(EtfTokenType.Map, new byte[] { 0x00, 0x00, 0x00, 0x00 }, new Dictionary<string, int>());
            yield return ReadWrite(EtfTokenType.Map, new byte[] 
            {
                0x00, 0x00, 0x00, 0x01, // 1 elements
                0x6D, 0x00, 0x00, 0x00, 0x01, 0x61, // a
                0x61, 0x01 // = 1
            }, new Dictionary<string, int> { ["a"] = 1 });
            yield return ReadWrite(EtfTokenType.Map, new byte[] 
            {
                0x00, 0x00, 0x00, 0x03, // 3 elements
                0x6D, 0x00, 0x00, 0x00, 0x01, 0x61, // a
                0x61, 0x01, // = 1
                0x6D, 0x00, 0x00, 0x00, 0x01, 0x62, // b
                0x61, 0x02, // = 2
                0x6D, 0x00, 0x00, 0x00, 0x01, 0x63, // c
                0x61, 0x03 // = 3
            }, new Dictionary<string, int> { ["a"] = 1, ["b"] = 2, ["c"] = 3 });

            yield return FailRead(EtfTokenType.Map, new byte[]
            {
                0x00, 0x00, 0x00, 0x03, // 3 elements
                0x6D, 0x00, 0x00, 0x00, 0x01, 0x61, // a
                0x61, 0x01, // = 1
                0x6D, 0x00, 0x00, 0x00, 0x01, 0x62, // b
                0x61, 0x01 // = 2
            }); // Incomplete
        }

        public DictionaryTests() : base(new Comparer()) { }

        [Theory]
        [MemberData(nameof(GetData))]
        public void Dictionary(BinaryTestData<Dictionary<string, int>> data) => RunTest(data);
    }
}

using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class BooleanTests : BaseTest<bool>
    {
        public static IEnumerable<object[]> GetGData()
        {
            yield return FailRead("null");
            yield return Read("false", false);
            yield return ReadWrite("False", false);
            yield return Read("true", true);
            yield return ReadWrite("True", true);
        }
        public static IEnumerable<object[]> GetLittleLData()
        {
            yield return FailRead("null");
            yield return ReadWrite("false", false);
            yield return ReadWrite("true", true);
        }

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<bool> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleLData))]
        public void Format_LittleL(TextTestData<bool> data) => RunTest(data, new BooleanUtf8Converter('l'));
    }
}

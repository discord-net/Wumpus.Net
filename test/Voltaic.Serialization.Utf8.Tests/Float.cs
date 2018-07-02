using System.Collections.Generic;
using Xunit;

namespace Voltaic.Serialization.Utf8.Tests
{
    public class SingleTests : BaseTest<float>
    {
        public static IEnumerable<object[]> GetGData()
        {
            yield return FailRead("-3.402824e38"); // Min - 1e32
            yield return Read("-3.402823e38", -3.402823e38f); // Min
            yield return ReadWrite("-3.402823E+38", -3.402823e38f); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("3.402823E+38", 3.402823e38f); // Max
            yield return Read("3.402823e38", 3.402823e38f); // Max
            yield return FailRead("3.402824e38"); // Max + 1e32

            yield return ReadWrite("Infinity", float.PositiveInfinity);
            yield return ReadWrite("-Infinity", float.NegativeInfinity);
            yield return ReadWrite("NaN", float.NaN);
        }
        public static IEnumerable<object[]> GetLittleGData()
        {
            yield return FailRead("-3.402824e38"); // Min - 1e32
            yield return Read("-3.402823e38", -3.402823e38f); // Min
            yield return ReadWrite("-3.402823e+38", -3.402823e38f); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("3.402823e+38", 3.402823e38f); // Max
            yield return Read("3.402823e38", 3.402823e38f); // Max
            yield return FailRead("3.402824e38"); // Max + 1e32

            yield return ReadWrite("Infinity", float.PositiveInfinity);
            yield return ReadWrite("-Infinity", float.NegativeInfinity);
            yield return ReadWrite("NaN", float.NaN);
        }
        public static IEnumerable<object[]> GetFData()
        {
            yield return Write("-3.40", -3.401f);
            yield return ReadWrite("-3.40", -3.40f);
            yield return FailRead("-1.00e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.00", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1.00e");
            yield return ReadWrite("3.40", 3.40f);
            yield return Write("3.40", 3.401f);

            yield return ReadWrite("Infinity", float.PositiveInfinity);
            yield return ReadWrite("-Infinity", float.NegativeInfinity);
            yield return ReadWrite("NaN", float.NaN);
        }
        public static IEnumerable<object[]> GetEData()
        {
            yield return FailRead("-3.402824e38"); // Min - 1e32
            yield return Read("-3.402823e38", -3.402823e38f); // Min
            yield return ReadWrite("-3.402823E+038", -3.402823e38f); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.000000E+000", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("3.402823E+038", 3.402823e38f); // Max
            yield return Read("3.402823e38", 3.402823e38f); // Max
            yield return FailRead("3.402824e38"); // Max + 1e32

            yield return ReadWrite("Infinity", float.PositiveInfinity);
            yield return ReadWrite("-Infinity", float.NegativeInfinity);
            yield return ReadWrite("NaN", float.NaN);
        }
        public static IEnumerable<object[]> GetLittleEData()
        {
            yield return FailRead("-3.402824e38"); // Min - 1e32
            yield return Read("-3.402823e38", -3.402823e38f); // Min
            yield return ReadWrite("-3.402823e+038", -3.402823e38f); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.000000e+000", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("3.402823e+038", 3.402823e38f); // Max
            yield return Read("3.402823e38", 3.402823e38f); // Max
            yield return FailRead("3.402824e38"); // Max + 1e32

            yield return ReadWrite("Infinity", float.PositiveInfinity);
            yield return ReadWrite("-Infinity", float.NegativeInfinity);
            yield return ReadWrite("NaN", float.NaN);
        }

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<float> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(TextTestData<float> data) => RunTest(data, new SingleUtf8Converter('g'));
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(TextTestData<float> data) => RunTest(data, new SingleUtf8Converter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(TextTestData<float> data) => RunTest(data, new SingleUtf8Converter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(TextTestData<float> data) => RunTest(data, new SingleUtf8Converter('e'));
    }

    public class DoubleTests : BaseTest<double>
    {
        public static IEnumerable<object[]> GetGData()
        {
            //yield return Fail("-1.7976931348623158e308"); // Min - 1e292
            yield return Read("-1.79769313486231e308", -1.79769313486231e308d); // Min
            yield return ReadWrite("-1.79769313486231E+308", -1.79769313486231e308d); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("1.79769313486231E+308", 1.79769313486231e308d); // Max
            yield return Read("1.79769313486231e308", 1.79769313486231e308d); // Max
            //yield return Fail("1.7976931348623158e308"); // Max + 1e292

            // Default G formatter does not read/write past a precision of 14
            yield return FailRead("-1.79769313486232e308"); // Min - 1e294
            yield return Write("-1.79769313486232E+308", -1.7976931348623157e308d); // Min
            yield return Write("1.79769313486232E+308", 1.7976931348623157e308d); // Max
            yield return FailRead("1.79769313486232e308"); // Max + 1e294

            yield return ReadWrite("Infinity", double.PositiveInfinity);
            yield return ReadWrite("-Infinity", double.NegativeInfinity);
            yield return ReadWrite("NaN", double.NaN);
        }
        public static IEnumerable<object[]> GetLittleGData()
        {
            //yield return Fail("-1.7976931348623158e308"); // Min - 1e292
            yield return Read("-1.79769313486231e308", -1.79769313486231e308d); // Min
            yield return ReadWrite("-1.79769313486231e+308", -1.79769313486231e308d); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("1.79769313486231e+308", 1.79769313486231e308d); // Max
            yield return Read("1.79769313486231e308", 1.79769313486231e308d); // Max
            //yield return Fail("1.7976931348623158e308"); // Max + 1e292

            // Default G formatter does not read/write past a precision of 14
            yield return FailRead("-1.79769313486232e308"); // Min - 1e294
            yield return Write("-1.79769313486232e+308", -1.7976931348623157e308d); // Min
            yield return Write("1.79769313486232e+308", 1.7976931348623157e308d); // Max
            yield return FailRead("1.79769313486232e308"); // Max + 1e294

            yield return ReadWrite("Infinity", double.PositiveInfinity);
            yield return ReadWrite("-Infinity", double.NegativeInfinity);
            yield return ReadWrite("NaN", double.NaN);
        }
        public static IEnumerable<object[]> GetFData()
        {
            yield return Write("-3.40", -3.401d);
            yield return ReadWrite("-3.40", -3.40d);
            yield return FailRead("-1.00e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.00", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1.00e");
            yield return ReadWrite("3.40", 3.40d);
            yield return Write("3.40", 3.401d);

            yield return ReadWrite("Infinity", double.PositiveInfinity);
            yield return ReadWrite("-Infinity", double.NegativeInfinity);
            yield return ReadWrite("NaN", double.NaN);
        }
        public static IEnumerable<object[]> GetEData()
        {
            yield return FailRead("-1.797694e308"); // Min - 1e302
            yield return Read("-1.797693e308", -1.797693e308d); // Min
            yield return ReadWrite("-1.797693E+308", -1.797693e308d); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.000000E+000", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("1.797693E+308", 1.797693e308d); // Max
            yield return Read("1.797693e308", 1.797693e308d); // Max
            yield return FailRead("1.797694e308"); // Max + 1e302

            yield return ReadWrite("Infinity", double.PositiveInfinity);
            yield return ReadWrite("-Infinity", double.NegativeInfinity);
            yield return ReadWrite("NaN", double.NaN);
        }
        public static IEnumerable<object[]> GetLittleEData()
        {
            yield return FailRead("-1.797694e308"); // Min - 1e302
            yield return Read("-1.797693e308", -1.797693e308d); // Min
            yield return ReadWrite("-1.797693e+308", -1.797693e308d); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.000000e+000", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("1.797693e+308", 1.797693e308d); // Max
            yield return Read("1.797693e308", 1.797693e308d); // Max
            yield return FailRead("1.797694e308"); // Max + 1e302

            yield return ReadWrite("Infinity", double.PositiveInfinity);
            yield return ReadWrite("-Infinity", double.NegativeInfinity);
            yield return ReadWrite("NaN", double.NaN);
        }

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<double> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetLittleGData))]
        public void Format_LittleG(TextTestData<double> data) => RunTest(data, new DoubleUtf8Converter('g'));
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(TextTestData<double> data) => RunTest(data, new DoubleUtf8Converter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(TextTestData<double> data) => RunTest(data, new DoubleUtf8Converter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(TextTestData<double> data) => RunTest(data, new DoubleUtf8Converter('e'));
    }

    public class DecimalTests : BaseTest<decimal>
    {
        public static IEnumerable<object[]> GetGData()
        {
            yield return FailRead("-79228162514264337593543950336"); // Min - 1
            yield return Read("-79228162514264337593543950335", -79228162514264337593543950335m); // Min
            yield return ReadWrite("-79228162514264337593543950335", -79228162514264337593543950335m); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("79228162514264337593543950335", 79228162514264337593543950335m); // Max
            yield return Read("79228162514264337593543950335", 79228162514264337593543950335m); // Max
            yield return FailRead("79228162514264337593543950336"); // Max + 1e32
        }
        public static IEnumerable<object[]> GetFData()
        {
            yield return Write("-3.40", -3.401m);
            yield return ReadWrite("-3.40", -3.40m);
            yield return FailRead("-1.00e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.00", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1.00e");
            yield return ReadWrite("3.40", 3.40m);
            yield return Write("3.40", 3.401m);
        }
        public static IEnumerable<object[]> GetEData()
        {
            yield return FailRead("-7.922817e28"); // Min - 1e22
            yield return Read("-7.922816e28", -7.922816e28m); // Min
            yield return ReadWrite("-7.922816E+028", -7.922816e28m); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.000000E+000", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("7.922816E+028", 7.922816e28m); // Max
            yield return Read("7.922816e28", 7.922816e28m); // Max
            yield return FailRead("7.922817e28"); // Max + 1e22
        }
        public static IEnumerable<object[]> GetLittleEData()
        {
            yield return FailRead("-7.922817e28"); // Min - 1e22
            yield return Read("-7.922816e28", -7.922816e28m); // Min
            yield return ReadWrite("-7.922816e+028", -7.922816e28m); // Min
            yield return FailRead("-1e");
            yield return FailRead("-0.01e");
            yield return ReadWrite("0.000000e+000", 0);
            yield return FailRead("0.01e");
            yield return FailRead("1e");
            yield return ReadWrite("7.922816e+028", 7.922816e28m); // Max
            yield return Read("7.922816e28", 7.922816e28m); // Max
            yield return FailRead("7.922817e28"); // Max + 1e22
        }

        [Theory]
        [MemberData(nameof(GetGData))]
        public void Format_G(TextTestData<decimal> data) => RunTest(data);
        [Theory]
        [MemberData(nameof(GetFData))]
        public void Format_F(TextTestData<decimal> data) => RunTest(data, new DecimalUtf8Converter('F'));
        [Theory]
        [MemberData(nameof(GetEData))]
        public void Format_E(TextTestData<decimal> data) => RunTest(data, new DecimalUtf8Converter('E'));
        [Theory]
        [MemberData(nameof(GetLittleEData))]
        public void Format_LittleE(TextTestData<decimal> data) => RunTest(data, new DecimalUtf8Converter('e'));
    }
}

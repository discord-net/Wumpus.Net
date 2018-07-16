using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wumpus.Rest.Tests
{
    public class RecursiveComparer<T> : IEqualityComparer<T>
    {
        public static RecursiveComparer<T> Instance { get; } = new RecursiveComparer<T>();

        public bool Equals(T actual, T expected) => NonTypedEquals(typeof(T), actual, expected);

        private bool NonTypedEquals(Type type, object actual, object expected)
        {
            if (type.IsPrimitive)
                return actual.Equals(expected);
            else if (type.IsArray)
            {
                var seq1 = (actual as IEnumerable).Cast<object>().ToArray();
                var seq2 = (expected as IEnumerable).Cast<object>().ToArray();
                if (seq1.Length != seq2.Length)
                    return false;
                for (int i = 0; i < seq1.Length; i++)
                {
                    if (!NonTypedEquals(type.GetElementType(), seq1[i], seq2[i]))
                        return false;
                }
                return true;
            }
            else
            {
                var fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var field in fields)
                {
                    var innerExpectedValue = field.GetValue(expected);
                    var innerActualValue = field.GetValue(actual);
                    if (innerExpectedValue is null && innerActualValue is null)
                        continue; // true
                    if (innerActualValue is null || innerExpectedValue is null)
                        return false;
                    if (!NonTypedEquals(field.FieldType, innerActualValue, innerExpectedValue))
                        return false;
                }
                return true;
            }
        }

        public int GetHashCode(T parameterValue) => 0; // Ignore
    }
}

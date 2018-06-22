
using System;

namespace Voltaic.Serialization
{
    public ref struct DeferredPropertyList<TKey, TValue>
    {
        private ReadOnlySpan<TKey> k1, k2, k3, k4, k5, k6, k7, k8;
        private ReadOnlySpan<TValue> v1, v2, v3, v4, v5, v6, v7, v8;

        public int Count { get; private set; }

        public bool Add(ReadOnlySpan<TKey> key, ReadOnlySpan<TValue> value)
        {
            switch (Count++)
            {
                case 0:
                    k1 = key;
                    v1 = value;
                    return true;
                case 1:
                    k2 = key;
                    v2 = value;
                    return true;
                case 2:
                    k3 = key;
                    v3 = value;
                    return true;
                case 3:
                    k4 = key;
                    v4 = value;
                    return true;
                case 4:
                    k5 = key;
                    v5 = value;
                    return true;
                case 5:
                    k6 = key;
                    v6 = value;
                    return true;
                case 6:
                    k7 = key;
                    v7 = value;
                    return true;
                case 7:
                    k8 = key;
                    v8 = value;
                    return true;
                default:
                    return false;
            }
        }
        public ReadOnlySpan<TKey> GetKey(int i)
        {
            switch (i)
            {
                case 0: return k1;
                case 1: return k2;
                case 2: return k3;
                case 3: return k4;
                case 4: return k5;
                case 5: return k6;
                case 6: return k7;
                case 7: return k8;
                default: return ReadOnlySpan<TKey>.Empty;
            }
        }
        public ReadOnlySpan<TValue> GetValue(int i)
        {
            switch (i)
            {
                case 0: return v1;
                case 1: return v2;
                case 2: return v3;
                case 3: return v4;
                case 4: return v5;
                case 5: return v6;
                case 6: return v7;
                case 7: return v8;
                default: return ReadOnlySpan<TValue>.Empty;
            }
        }
    }
}


using System;

namespace Voltaic.Serialization
{
    public ref struct DeferredSpanList<T>
    {
        private ReadOnlySpan<T> s1, s2, s3, s4, s5, s6, s7, s8;

        public int Count { get; private set; }

        public bool Add(ReadOnlySpan<T> span)
        {
            switch (Count++)
            {
                case 0: s1 = span; return true;
                case 1: s2 = span; return true;
                case 2: s3 = span; return true;
                case 3: s4 = span; return true;
                case 4: s5 = span; return true;
                case 5: s6 = span; return true;
                case 6: s7 = span; return true;
                case 7: s8 = span; return true;
                default: return false;
            }
        }
        public ReadOnlySpan<T> this[int i]
        {
            get
            {
                switch (i)
                {
                    case 0: return s1;
                    case 1: return s2;
                    case 2: return s3;
                    case 3: return s4;
                    case 4: return s5;
                    case 5: return s6;
                    case 6: return s7;
                    case 7: return s8;
                    default: return Span<T>.Empty;
                }
            }
        }
    }
}

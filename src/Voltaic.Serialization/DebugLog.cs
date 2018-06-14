using System.Diagnostics;

namespace Voltaic.Serialization
{
    public static class DebugLog
    {
        [Conditional("DEBUG")]
        public static void WriteFailure(string msg)
        {
            Debug.WriteLine(msg);
        }
    }
}

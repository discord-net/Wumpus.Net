using System;
using System.Runtime.InteropServices;

namespace Wumpus
{
    internal class SodiumPrimitives
    {
        // sodium.dll on windows, libsodium.so on linux
        [DllImport("sodium")]
        extern static int crypto_secretbox_macbytes();
        [DllImport("sodium")]
        extern static int crypto_secretbox_keybytes();
        [DllImport("sodium")]
        extern static int crypto_secretbox_noncebytes();

        [DllImport("sodium")]
        extern static unsafe int crypto_secretbox_easy(byte* c, byte* m, int mlen, byte* n, byte* k);
        [DllImport("sodium")]
        extern static unsafe int crypto_secretbox_open_easy(byte* m, byte* c, int clen, byte* n, byte* k);

        [DllImport("sodium")]
        extern static unsafe void randombytes_buf(byte* buf, int size);

        // use the functions to grab the info so it's not hardcoded in the lib
        private static readonly int MACBYTES = crypto_secretbox_macbytes();
        private static readonly int KEYBYTES = crypto_secretbox_keybytes();
        private static readonly int NONCEBYTES = crypto_secretbox_noncebytes();

        public static int NonceSize => NONCEBYTES;

        public static int ComputeMessageLength(int messageLength)
            => messageLength + MACBYTES;

        public static unsafe bool TryEncryptInPlace(Span<byte> ciphertext, ReadOnlySpan<byte> message, ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> secret)
        {
            if (ciphertext.Length < message.Length + MACBYTES)
                return false;
            if (secret.Length < KEYBYTES)
                return false;
            if (nonce.Length < NONCEBYTES)
                return false;

            fixed(byte* c = &ciphertext.GetPinnableReference())
            fixed(byte* m = &message.GetPinnableReference())
            fixed(byte* n = &nonce.GetPinnableReference())
            fixed(byte* k = &secret.GetPinnableReference())
                return crypto_secretbox_easy(c, m, message.Length, n, k) == 0;
        }

        public static unsafe void GenerateRandomBytes(Span<byte> buffer)
        {
            fixed(byte* buf = &buffer.GetPinnableReference())
                randombytes_buf(buf, buffer.Length);
        }
    }
}
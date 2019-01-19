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
        extern static int crypto_secretbox_easy(ref byte c, in byte m, int mlen, in byte n, in byte k);
        [DllImport("sodium")]
        extern static int crypto_secretbox_open_easy(ref byte m, in byte c, int clen, in byte n, in byte k);

        [DllImport("sodium")]
        extern static void randombytes_buf(ref byte buf, int size);

        // use the functions to grab the info so it's not hardcoded in the lib
        private static readonly int MACBYTES = crypto_secretbox_macbytes();
        private static readonly int KEYBYTES = crypto_secretbox_keybytes();
        private static readonly int NONCEBYTES = crypto_secretbox_noncebytes();

        public static int NonceSize => NONCEBYTES;

        public static int ComputeMessageLength(int messageLength)
            => messageLength + MACBYTES;

        public static bool TryEncryptInPlace(Span<byte> ciphertext, ReadOnlySpan<byte> message, ReadOnlySpan<byte> nonce, ReadOnlySpan<byte> secret)
        {
            if (ciphertext.Length < message.Length + MACBYTES)
                return false;
            if (secret.Length < KEYBYTES)
                return false;
            if (nonce.Length < NONCEBYTES)
                return false;

            return crypto_secretbox_easy(
                ref ciphertext.GetPinnableReference(),
                message.GetPinnableReference(), message.Length,
                nonce.GetPinnableReference(),
                secret.GetPinnableReference()) == 0;
        }

        public static void GenerateRandomBytes(Span<byte> buffer)
        {
            randombytes_buf(ref buffer.GetPinnableReference(), buffer.Length);
        }
    }
}
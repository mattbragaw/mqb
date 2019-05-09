using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Mqb
{
    public static class Unique
    {
        private const string RNG_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        private const int RNG_MAX_SIZE = 11;

        public static string String() => StringRngMask();
        
        private static string StringRngMask()
        {
            char[] chars = new char[RNG_CHARS.Length];
            chars = RNG_CHARS.ToCharArray();
            int size = RNG_MAX_SIZE;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = RNG_MAX_SIZE;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }
    }
}

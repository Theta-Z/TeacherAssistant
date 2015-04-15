using System;
using System.Security.Cryptography;

namespace TeacherAssistant.Helpers
{
    public class MD5Helper
    {
        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var buffer = new byte[input.Length * sizeof(char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, buffer, 0, buffer.Length);

            var hashed = md5.ComputeHash(buffer);
            var output = new char[hashed.Length / sizeof(char)];
            Buffer.BlockCopy(hashed, 0, output, 0, hashed.Length);

            return new string(output);
        }
    }
}
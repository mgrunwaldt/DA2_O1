using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    public static class TokenHelper
    {
        public static string CreateToken()

        {
            string input = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            Random r = new Random();
            for (int i = 0; i < 32; i++)
            {
                ch = input[r.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
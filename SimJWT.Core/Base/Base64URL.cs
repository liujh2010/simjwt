using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base
{
    public class Base64URL : IBase64URL
    {
        private readonly char[] padding = { '=' };

        public string Encode(string s)
        {
            return Convert
                .ToBase64String(Encoding.UTF8.GetBytes(s))
                .TrimEnd(padding)
                .Replace('+', '-')
                .Replace('/', '_');
        }

        public string Decode(string s)
        {
            var str = s.Replace('_', '/').Replace('-', '+');
            switch (str.Length % 4)
            {
                case 2:
                    str += "==";
                    break;
                case 3:
                    str += "=";
                    break;
            }
            return Encoding.UTF8
                .GetString(Convert.FromBase64String(str));
        }

    }
}

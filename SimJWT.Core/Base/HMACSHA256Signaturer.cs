using SimJWT.Core.Common;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SimJWT.Core.Base
{
    public class HMACSHA256Signaturer : ISignaturer
    {
        private string _secret = "85&S)@q+y&%8DmqDSMmw9>?x;8;f<EQTES^lW0jt]O=<jzS5$y";

        public HMACSHA256Signaturer(string key) => _secret = key;

        public string GetDigest(string cleanText) => GetDigest(cleanText, _secret);

        public string GetDigest(string cleanText, string secret)
        {
            secret = secret ?? _secret;
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var textBytes = Encoding.UTF8.GetBytes(cleanText);
            using (var crypter = new HMACSHA256(keyBytes))
            {
                var digest = crypter.ComputeHash(textBytes);
                var builder = new StringBuilder();
                for (int i = 0; i < digest.Length; i++)
                {
                    builder.Append(digest[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }
    }
}

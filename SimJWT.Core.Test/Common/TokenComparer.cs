using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit.Sdk;

namespace SimJWT.Core.Test.Common
{
    public class TokenComparer<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            var header = typeof(T).GetProperty("Header");
            var base64Header = typeof(T).GetProperty("Base64Header");
            var payload = typeof(T).GetProperty("Payload");
            var base64Payload = typeof(T).GetProperty("Base64Payload");
            var signature = typeof(T).GetProperty("Signature");
            var base64Signature = typeof(T).GetProperty("Base64Signature");
            var jwt = typeof(T).GetProperty("Jwt");
            var authorizedToken = typeof(T).GetProperty("AuthorizedToken");

            EqualObjects(header, x, y);
            EqualObjects(payload, x, y);

            EqualObject(base64Header, x, y);
            EqualObject(base64Payload, x, y);
            EqualObject(signature, x, y);
            EqualObject(base64Signature, x, y);
            EqualObject(jwt, x, y);
            EqualObject(authorizedToken, x, y);

            return true;
        }

        public int GetHashCode(T obj)
        {
            return Tuple.Create(obj).GetHashCode();
        }

        private bool EqualObject<U>(PropertyInfo prop, U x, U y)
        {
            var expected = prop.GetValue(x);
            var actual = prop.GetValue(y);
            if (expected == null && actual == null) return true;
            if (!expected.Equals(actual))
            {
                throw new EqualException($"A value of \"{expected}\" for property \"{prop.Name}\"",
                    $"A value of \"{actual}\" for property \"{prop.Name}\"");
            }
            return true;
        }

        private bool EqualObjects(PropertyInfo prop, T x, T y)
        {
            var xVal = prop.GetValue(x);
            var yVal = prop.GetValue(y);
            var props = xVal.GetType().GetProperties();
            foreach (var p in props)
            {
                EqualObject(p, xVal, yVal);
            }
            return true;
        }
    }
}

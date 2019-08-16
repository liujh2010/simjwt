using SimJWT.Core.Base;
using SimJWT.Core.JWT;
using Xunit;
using SimJWT.Core.Test.Common;
using SimJWT.Core.JWT.Structure;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;
using System;

namespace SimJWT.Core.Test.JWT
{
    public class TokenTest
    {
        private string key = "123456";
        private Token<Header, Payload> testToken;
        private JwtFactory factory;

        private Header testHeader = new Header() { Alg = "HS256", Typ = "JWT" };
        private Payload testPayload = new Payload()
        {
            Iss = "Richard",
            Exp = "2019.8.9 12:00:00",
            Sub = "login",
            Nbf = "2019.8.8 12:00:00",
            Iat = "2019.8.8 12:00:00",
            Jti = "001"
        };

        private class TokenComparer<T> : IEqualityComparer<T>
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

        public TokenTest()
        {
            factory = new JwtFactory(new Base64URL(), new TestJsonSerialization(), new HMACSHA256Signaturer(key));
            testToken = factory.GetJwtObject(testHeader, testPayload);
        }

        [Fact]
        public void TestTokenConstructByData()
        {
            Assert.NotNull(testToken);

            var actualHeader = testToken.Base64Header;
            var expectedHeader = Base64UrlEncoder.Encode(JsonConvert.SerializeObject(testHeader));
            Assert.Equal(expectedHeader, actualHeader);

            var actualPayload = testToken.Base64Payload;
            var expectedPayload = Base64UrlEncoder.Encode(JsonConvert.SerializeObject(testPayload));
            Assert.Equal(expectedPayload, actualPayload);

            Assert.NotNull(testToken.Jwt);

            var actualSignature = testToken.Base64Signature;
            var expectedSignature = Base64UrlEncoder.Encode(new HMACSHA256Signaturer(key).GetDigest($"{actualHeader}.{actualPayload}"));
            Assert.Equal(expectedSignature, actualSignature);

            var actualJwt = testToken.Jwt;
            var expectedJwt = $"{expectedHeader}.{expectedPayload}.{expectedSignature}";
            Assert.Equal(expectedJwt, actualJwt);

            Assert.True(testToken.AuthorizedToken);
        }

        [Fact]
        public void TestTokenConstructByJwt()
        {
            var actualJwt = factory.GetJwtObject<Header, Payload>(testToken.Jwt);

            Assert.Equal(testToken, actualJwt, new TokenComparer<object>());
        }
    }
}

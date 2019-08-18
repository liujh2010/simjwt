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

            Assert.Equal(testToken, actualJwt, new TokenComparer<Token<Header,Payload>>());
        }
    }
}

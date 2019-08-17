using Newtonsoft.Json;
using SimJWT.Core.Base.JSON;
using SimJWT.Core.Common;
using SimJWT.Core.JWT;
using SimJWT.Core.JWT.Structure;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimJWT.Core.Test.JsonComponents
{
    public class JsonSeriallzationTest
    {
        private TestClass<Header,Payload> _testClass;
        private TestClass<TestHeader, TestPayload> _testExtendsClass;

        private class TestClass<T,U>
        {
            private string _private = "private1";
            public string publicField = "publicField1";
            public string PublicProp1 { get; set; } = "public prop1";
            public string PublicProp2 { get; set; } = "public prop2";

            private T PrivateHeader { get; set; }
            public U PublicPayload { get; set; }

            public TestClass(T h, U p)
            {
                PrivateHeader = h;
                PublicPayload = p;
            }
        }

        private class TestHeader : Header
        {
            public string extendsHeader = "123";
        }

        private class TestPayload : Payload
        {
            public string extendsPayload { get; set; } = "456";
        }

        public JsonSeriallzationTest()
        {
            // initialization test class
            Header testHeader = new Header() { Alg = "HS256", Typ = "JWT" };
            Payload testPayload = new Payload()
            {
                Iss = "Richard",
                Exp = "2019.8.9 12:00:00",
                Sub = "login",
                Nbf = "2019.8.8 12:00:00",
                Iat = "2019.8.8 12:00:00",
                Jti = "001"
            };

            _testClass = new TestClass<Header, Payload>(testHeader, testPayload);

            // initialization test extends class
            TestHeader testHeader1 = new TestHeader() { Alg = "HS256", Typ = "JWT", extendsHeader = "1234" };
            TestPayload testPayload1 = new TestPayload()
            {
                Iss = "Richard",
                Exp = "2019.8.9 12:00:00",
                Sub = "login",
                Nbf = "2019.8.8 12:00:00",
                Iat = "2019.8.8 12:00:00",
                Jti = "001",
                extendsPayload = "5678"
            };

            _testExtendsClass = new TestClass<TestHeader, TestPayload>(testHeader1, testPayload1);
        }

        [Fact]
        public void TestJsonSerizllze()
        {
            var actual = new DefaultJsonSeriallzation().SerializeObject(_testClass);
            var expected = JsonConvert.SerializeObject(_testClass);

            var actualObj = JsonConvert.DeserializeObject<TestClass<Header,Payload>>(actual);
            var expectedObj = JsonConvert.DeserializeObject<TestClass<Header,Payload>>(expected);

            var results = new List<bool>();

            results.Add(expectedObj.publicField.Equals(actualObj.publicField));
            results.Add(expectedObj.PublicProp1.Equals(actualObj.PublicProp1));
            results.Add(expectedObj.PublicProp2.Equals(actualObj.PublicProp2));
            //results.Add(expectedObj.PublicPayload.Aud.Equals(actualObj.PublicPayload.Aud));
            results.Add(expectedObj.PublicPayload.Exp.Equals(actualObj.PublicPayload.Exp));
            results.Add(expectedObj.PublicPayload.Iat.Equals(actualObj.PublicPayload.Iat));
            results.Add(expectedObj.PublicPayload.Iss.Equals(actualObj.PublicPayload.Iss));
            results.Add(expectedObj.PublicPayload.Jti.Equals(actualObj.PublicPayload.Jti));
            results.Add(expectedObj.PublicPayload.Nbf.Equals(actualObj.PublicPayload.Nbf));
            results.Add(expectedObj.PublicPayload.Sub.Equals(actualObj.PublicPayload.Sub));

            foreach (var item in results)
            {
                Assert.True(item);
            }
        }

        [Fact]
        public void TestJsonSerizllzeByExtendsClass()
        {
            var actual = new DefaultJsonSeriallzation().SerializeObject(_testExtendsClass);
            var expected = JsonConvert.SerializeObject(_testExtendsClass);

            var actualObj = JsonConvert.DeserializeObject<TestClass<TestHeader, TestPayload>>(actual);
            var expectedObj = JsonConvert.DeserializeObject<TestClass<TestHeader, TestPayload>>(expected);

            var results = new List<bool>();

            results.Add(expectedObj.publicField.Equals(actualObj.publicField));
            results.Add(expectedObj.PublicProp1.Equals(actualObj.PublicProp1));
            results.Add(expectedObj.PublicProp2.Equals(actualObj.PublicProp2));
            //results.Add(expectedObj.PublicPayload.Aud.Equals(actualObj.PublicPayload.Aud));
            results.Add(expectedObj.PublicPayload.Exp.Equals(actualObj.PublicPayload.Exp));
            results.Add(expectedObj.PublicPayload.Iat.Equals(actualObj.PublicPayload.Iat));
            results.Add(expectedObj.PublicPayload.Iss.Equals(actualObj.PublicPayload.Iss));
            results.Add(expectedObj.PublicPayload.Jti.Equals(actualObj.PublicPayload.Jti));
            results.Add(expectedObj.PublicPayload.Nbf.Equals(actualObj.PublicPayload.Nbf));
            results.Add(expectedObj.PublicPayload.Sub.Equals(actualObj.PublicPayload.Sub));
            results.Add(expectedObj.PublicPayload.extendsPayload.Equals(actualObj.PublicPayload.extendsPayload));

            foreach (var item in results)
            {
                Assert.True(item);
            }
        }
    }
}

using Xunit;
using Microsoft.IdentityModel.Tokens;
using SimJWT.Core.Base;

namespace SimJWT.Core.Test.BasicComponents
{
    public class Base64URLTest
    {
        string uncodedTestString;
        string codedTestString = null;
    
        public Base64URLTest()
        {
            uncodedTestString = "Microsoft.IdentityModel.Tokens";
            codedTestString = Base64UrlEncoder.Encode(uncodedTestString);
        }

        [Fact]
        public void TestEncode()
        {
            var expected = codedTestString;
            var actual = new Base64URL().Encode(uncodedTestString);
            Assert.Equal(expected, actual);   
        }

        [Fact]
        public void TestDecode()
        {
            var expected = uncodedTestString;
            var res = Base64UrlEncoder.Decode(codedTestString);
            Assert.Equal(uncodedTestString, res);
            var actual = new Base64URL().Decode(codedTestString);
            Assert.Equal(expected, actual);
        }
    }
}

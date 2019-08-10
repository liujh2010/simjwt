using SimJWT.Core.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SimJWT.Core.Test.BasicComponents
{
    public class HMACSHA256SignaturerTest
    {
        private string text = "using System.Collections.Generic";
        private string secret = "123456";

        [Fact]
        public void TestGetDigest()
        {
            var signaturer = new HMACSHA256Signaturer(secret);
            var hash = signaturer.GetDigest(text);

            Assert.NotNull(hash);
        }
    }
}

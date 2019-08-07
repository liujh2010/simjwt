using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Common
{
    public interface ISignaturer
    {
        string GetDigest(string cleanText, string secret);
    }
}

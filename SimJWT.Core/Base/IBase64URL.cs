using System;

namespace SimJWT.Core.Base
{
    public interface IBase64URL
    {
        string Encode(string s);
        string Decode(string s);
    }
}

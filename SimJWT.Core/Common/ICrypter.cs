using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Common
{
    public interface ICrypter
    {
        string Encrypt(object o);
        string Encrypt(string s);
        string Decrypt(string s);
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.JWT.Structure
{
    public class Signature
    {
        public string SignatureData { get; }

        public Signature(string clearText)
        {
            SignatureData = clearText;
        }
    }
}

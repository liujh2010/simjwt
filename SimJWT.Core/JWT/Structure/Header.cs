using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.JWT.Structure
{
    public class Header : ClearTextBase
    {
        public string Alg { get; set; }
        public string Typ { get; set; }
    }
}

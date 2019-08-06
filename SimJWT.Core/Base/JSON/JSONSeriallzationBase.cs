using System;
using System.Collections.Generic;
using System.Text;
using SimJWT.Core.JWT;

namespace SimJWT.Core.Base.JSON
{
    public abstract class JSONSeriallzationBase : IJSONSerialization
    {
        private EnumCaseOption _caseOption;
        private EnumFieldOption _fieldOption;

        public JSONSeriallzationBase()
        {
            _caseOption = EnumCaseOption.ToLowerCase;
            _fieldOption = EnumFieldOption.IgIgnoreNull;
        }

        public abstract string SerializeToString(object o);

        public abstract string SerializeObject(object o, EnumCaseOption caseOption, EnumFieldOption fieldOption);
        public abstract object DeserializeToObject(string s);
        public abstract T DeserializeToObject<T>(string s) where T : class;
    }
}

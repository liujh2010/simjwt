using System;
using System.Collections.Generic;
using System.Text;
using SimJWT.Core.Common;
using SimJWT.Core.JWT;

namespace SimJWT.Core.Base.JSON
{
    public abstract class JSONSeriallzationBase : IJSONSerialization
    {
        private SerializationOption _option;

        public JSONSeriallzationBase()
        {
            _option = SerializationOption.Default;
        }

        public abstract string SerializeObject(object o);
        public abstract string SerializeObject(object o, SerializationOption option);
        public abstract object DeserializeObject(string s);
        public abstract T DeserializeObject<T>(string s) where T : class;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base.JSON
{
    public class DefaultJsonSeriallzation : JSONSeriallzationBase
    {
        public override string SerializeObject(object o)
        {
            return new Seriallzation().Serialize(o);
        }

        public override string SerializeObject(object o, SerializationOption option)
        {
            throw new NotImplementedException();
        }

        public override object DeserializeObject(string s)
        {
            throw new NotImplementedException();
        }

        public override T DeserializeObject<T>(string s)
        {
            throw new NotImplementedException();
        }
    }
}

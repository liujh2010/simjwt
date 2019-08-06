using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.JWT
{
    public interface IJSONSerialization
    {
        string SerializeToString(object o);
        object DeserializeToObject(string s);
        T DeserializeToObject<T>(string s) where T : class;
    }
}

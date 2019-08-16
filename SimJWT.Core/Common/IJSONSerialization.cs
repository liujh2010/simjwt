using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Common
{
    public interface IJSONSerialization
    {
        string SerializeObject(object o);
        object DeserializeObject(string s);
        T DeserializeObject<T>(string s) where T : class;
    }
}

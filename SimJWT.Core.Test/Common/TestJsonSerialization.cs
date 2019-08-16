using Newtonsoft.Json;
using SimJWT.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Test.Common
{
    public class TestJsonSerialization : IJSONSerialization
    {
        public object DeserializeObject(string s)
        {
            return JsonConvert.DeserializeObject(s);
        }

        public T DeserializeObject<T>(string s) where T : class
        {
            return JsonConvert.DeserializeObject<T>(s);
        }

        public string SerializeObject(object o)
        {
            return JsonConvert.SerializeObject(o);
        }
    }
}

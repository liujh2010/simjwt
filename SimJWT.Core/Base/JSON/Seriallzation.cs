using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base.JSON
{
    public class Seriallzation
    {
        public string Serialize(object o)
        {
            var reflection = new ReflectionProperty();
            var jsonProps = reflection.GetJsonProperties(o);
            var jsonWriter = new JsonWriter(jsonProps);
            return jsonWriter.WriteJsonString();
        }
    }
}

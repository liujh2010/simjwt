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
            var option = SerializationOption.LeaveNull | SerializationOption.ToLowerCamelCase;
            var jsonWriter = new JsonWriter(jsonProps, option);
            return jsonWriter.WriteJsonString();
        }
    }
}

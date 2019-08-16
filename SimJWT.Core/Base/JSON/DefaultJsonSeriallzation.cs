using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base.JSON
{
    class DefaultJsonSeriallzation : JSONSeriallzationBase
    {
        public override string SerializeObject(object o)
        {   
            // more than big Wrepper serialize to cash this object
            var stringBuilder = new StringBuilder();
            return SerializeObject(o, stringBuilder)
                .ToString();
        }

        private StringBuilder SerializeObject(object o,StringBuilder sb)
        {
            StartWrite(sb);

            var props = o.GetType().GetProperties();

            foreach (var prop in props)
            {
                if (prop.Name == "Header") SerializeObject(prop.GetValue(o),sb);  // return serialize object strting
                if (prop.Name == "Payload") SerializeObject(prop.GetValue(o),sb);

                // add this serialize object string to stringBuild
                AppendOneObject(sb, prop.Name, Convert.ToString(prop.GetValue(o)));

            }

            EndWriteObject(sb);
            return sb;
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

        private void StartWrite(StringBuilder builder) => builder.Append('{');

        private void EndWrite(StringBuilder builder) => builder
            .Remove(builder.Length - 2, builder.Length - 1);

        private void EndWriteObject(StringBuilder builder)
        {
            builder.Remove(builder.Length - 2, builder.Length - 1);
            builder.Append("},");
        }

        private void AppendOneObject (StringBuilder builder, string name,string value)
        {
            builder.Append("\"");
            builder.Append(name);
            builder.Append("\":\"");
            builder.Append(value);
            builder.Append("\",");
        }

    }
}

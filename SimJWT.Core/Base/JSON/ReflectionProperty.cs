using SimJWT.Core.JWT.Structure;

namespace SimJWT.Core.Base.JSON
{
    public class ReflectionProperty
    {
        public JsonProperties GetJsonProperties(object o)
        {
            var props = o.GetType().GetProperties();
            var fields = o.GetType().GetFields();
            var jsonProps = new JsonProperties();

            foreach (var field in fields)
            {
                var value = field.GetValue(o);
                jsonProps.AddStringProperty(field.Name, value?.ToString());
            }

            foreach (var prop in props)
            {
                var value = prop.GetValue(o);
                if (value is Header || value is Payload)
                {
                    var res = PutStringProperties(prop.GetValue(o));
                    jsonProps.AddObjectProperty(prop.Name, res);
                    continue;
                }

                jsonProps.AddStringProperty(prop.Name, value?.ToString());
            }

            return jsonProps;
        }

        private JsonProperties PutStringProperties(object o)
        {
            var jsonProps = new JsonProperties();
            var props = o.GetType().GetProperties();
            var fields = o.GetType().GetFields();

            foreach (var prop in props)
            {
                var value = prop.GetValue(o);
                jsonProps.AddStringProperty(prop.Name, value?.ToString());
            }

            foreach (var field in fields)
            {
                var value = field.GetValue(o);
                jsonProps.AddStringProperty(field.Name, value?.ToString());
            }

            return jsonProps;
        }
    }
}

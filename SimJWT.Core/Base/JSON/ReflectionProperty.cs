namespace SimJWT.Core.Base.JSON
{
    public class ReflectionProperty
    {
        public JsonProperties GetJsonProperties(object o)
        {
            var props = o.GetType().GetProperties();
            var fields = o.GetType().GetFields();
            var jsonProps = new JsonProperties();

            foreach (var prop in props)
            {
                if (prop.Name == "Header" || prop.Name == "Payload")
                {
                    var res = PutStringProperties(prop.GetValue(o));
                    jsonProps.AddObjectProperty(prop.Name, res);
                    continue;
                }

                jsonProps.AddStringProperty(prop.Name, prop.GetValue(o).ToString());
            }

            foreach (var field in fields)
            {
                jsonProps.AddStringProperty(field.Name, field.GetValue(o).ToString());
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
                jsonProps.AddStringProperty(prop.Name, prop.GetValue(o).ToString());
            }

            foreach (var field in fields)
            {
                jsonProps.AddStringProperty(field.Name, field.GetValue(o).ToString());
            }

            return jsonProps;
        }
    }
}

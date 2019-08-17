using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base.JSON
{
    public class JsonWriter
    {
        private StringBuilder _stringBuilder;
        private JsonProperties _jsonProperties;

        private bool isDefault;
        private bool isLeaveNull;
        private bool isToLowerCamelCase;

        public JsonWriter(JsonProperties jsonProperties, SerializationOption option)
        {
            _stringBuilder = new StringBuilder();
            _jsonProperties = jsonProperties;

            isDefault = SerializationOption.Default == (option & SerializationOption.Default);
            isLeaveNull = SerializationOption.LeaveNull == (option & SerializationOption.LeaveNull);
            isToLowerCamelCase = SerializationOption.ToLowerCamelCase == (option & SerializationOption.ToLowerCamelCase);
        }

        private void StartWriteObject()
        {
            _stringBuilder.Append('{');
        }

        private void EndWriteObject()
        {
            _stringBuilder.Remove(_stringBuilder.Length - 1, 1);
            _stringBuilder.Append('}');
        }

        private void AddOneProperty(string name, string value)
        {
            if (name == null || name == "") return;

            if (value != null)
            {
                if (isDefault)
                {
                    _stringBuilder.AppendFormat("\"{0}\":\"{1}\",", name, value);
                    return;
                }
                if (isToLowerCamelCase)
                {
                    _stringBuilder.AppendFormat("\"{0}\":\"{1}\",", name, ToLowerCamelCase(value));
                    return;
                }
            }
            else
            {
                if (isLeaveNull)
                    _stringBuilder.AppendFormat("\"{0}\":null,", name);
                else
                    return;
            }
        }

        private void AddOneProperty(string name, JsonProperties jsonProperties)
        {
            if (name == null || name == "") return;
            var enumerator = jsonProperties.GetStringPropertiesEnumerator();
            if (enumerator.MoveNext() == false) return;

            _stringBuilder.AppendFormat("\"{0}\":", name);
            StartWriteObject();

            do
            {
                var current = enumerator.Current;
                AddOneProperty(current.Key, current.Value);
            } while (enumerator.MoveNext());

            EndWriteObject();
            _stringBuilder.Append(',');
        }

        public string WriteJsonString()
        {
            StartWriteObject();
            var stringEnumerator = _jsonProperties.GetStringPropertiesEnumerator();
            var objcetEnumerator = _jsonProperties.GetObjectPropertiesEnumerator();

            if (stringEnumerator.MoveNext() == false) return "";

            do
            {
                AddOneProperty(stringEnumerator.Current.Key, stringEnumerator.Current.Value);
            } while (stringEnumerator.MoveNext());

            if (objcetEnumerator.MoveNext() == false) return _stringBuilder.ToString();

            do
            {
                AddOneProperty(objcetEnumerator.Current.Key, objcetEnumerator.Current.Value);
            } while (objcetEnumerator.MoveNext());

            EndWriteObject();

            return _stringBuilder.ToString();
        }

        private string ToLowerCamelCase(string s)
        {
            unsafe
            {
                fixed (char* sf = s) sf[0] = Char.ToLower(sf[0]);
            }
            return s;
        }
    }
}

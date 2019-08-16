using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base.JSON
{
    public class JsonProperties
    {
        private Dictionary<string, string> _stringProperties;
        private Dictionary<string, JsonProperties> _objectProperties;

        public int StringPropertiesCount { get { return _stringProperties.Count; } }
        public int ObjectPropertiesCount { get { return _objectProperties.Count; } }

        public void AddStringProperty(string key, string value)
        {
            if (value == null || value == "") return;
            _stringProperties.Add(key, value);
        }

        public void AddObjectProperty(string key, JsonProperties jp)
        {
            if (jp == null || jp.StringPropertiesCount == 0 || jp.ObjectPropertiesCount == 0) return;
            _objectProperties.Add(key, jp);
        }

        public IEnumerator<KeyValuePair<string, string>> GetStringPropertiesEnumerator()
        {
            return _stringProperties.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<string,JsonProperties>> GetObjectPropertiesEnumerator()
        {
            return _objectProperties.GetEnumerator();
        }
    }
}

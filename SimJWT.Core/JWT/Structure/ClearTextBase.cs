using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.JWT.Structure
{
    public class ClearTextBase
    {
        protected Dictionary<string, string> _customData;

        public (string, string) CustomData
        {
            set
            {
                string key, val;
                (key, val) = value;
                _customData.Add(key, val);
            }
        }

        public ClearTextBase()
        {
            _customData = new Dictionary<string, string>();
        }

        public void Add(string key, string value)
        {
            _customData.Add(key, value);
        }
    }
}

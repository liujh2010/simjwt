using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base.JSON
{
    public enum SerializationOption
    {
        Default = 0,
        LeaveNull = 1,      //保留空字段
        ToLowerCamelCase = 2,    //转换小驼峰写法
    }
}

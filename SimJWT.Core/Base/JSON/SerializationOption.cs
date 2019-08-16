using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Base.JSON
{
    public enum SerializationOption
    {
        Default = 0,
        LeaveNull = 1,      //保留空字段
        ToLowerCase = 2,    //忽略大小写
    }
}

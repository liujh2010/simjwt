using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.JWT.Structure
{
    public class Payload
    {
        // issuer 签发人
        public string Iss { get; set; }

        // expiration time 过期时间
        public string Exp { get; set; }

        // subject 主题
        public string Sub { get; set; }

        // audience 受众
        public string Aud { get; set; }

        // Not Before 生效时间
        public string Nbf { get; set; }

        // Issued At 签发时间
        public string Iat { get; set; }

        // JWT ID 编号
        public string Jti { get; set; }
    }
}

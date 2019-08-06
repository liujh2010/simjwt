using SimJWT.Core.Common;
using SimJWT.Core.JWT;
using SimJWT.Core.JWT.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Authenticator
{
    public class JwtAuthenticationServiceFactory
    {
        public JwtAuthenticationService<THeader, TPayload, TCrypter, TJsonSerialization>
            GetService<THeader, TPayload, TCrypter, TJsonSerialization>
            (TCrypter c, TJsonSerialization j, ServiceOption option)
            where THeader : Header
            where TPayload : Payload
            where TCrypter : ICrypter
            where TJsonSerialization : IJSONSerialization
        {
            return new JwtAuthenticationService<THeader, TPayload, TCrypter, TJsonSerialization>(c, j);
        }
    }
}

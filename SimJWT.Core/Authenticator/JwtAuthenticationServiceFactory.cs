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
        public JwtAuthenticationService<THeader, TPayload, TSignaturer, TJsonSerialization>
            GetService<THeader, TPayload, TSignaturer, TJsonSerialization>
            (TSignaturer s, TJsonSerialization j, ServiceOption option)
            where THeader : Header
            where TPayload : Payload
            where TSignaturer : ISignaturer
            where TJsonSerialization : IJSONSerialization
        {
            return new JwtAuthenticationService<THeader, TPayload, TSignaturer, TJsonSerialization>(s, j);
        }
    }
}

using SimJWT.Core.Base;
using SimJWT.Core.JWT.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.JWT
{
    public class JwtFactory
    {
        private IBase64URL _base64;
        private IJSONSerialization _jSONSerialization;
        private ICrypter _crypter;

        public JwtFactory(IBase64URL b, IJSONSerialization s, ICrypter c)
        {
            _base64 = b;
            _jSONSerialization = s;
            _crypter = c;
        }

        public Token<THeader, TPayload> GetJwtObject<THeader, TPayload>(THeader header, TPayload payload)
            where THeader : Header
            where TPayload : Payload
        {
            return new Token<THeader, TPayload>(header, payload, _base64, _jSONSerialization, _crypter);
        }

        public Token<THeader, TPayload> GetJwtObject<THeader, TPayload>(string jwt)
            where THeader : Header
            where TPayload : Payload
        {
            return new Token<THeader, TPayload>(jwt, _base64, _jSONSerialization, _crypter);
        }
    }
}

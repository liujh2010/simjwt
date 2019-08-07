using SimJWT.Core.Base;
using SimJWT.Core.Common;
using SimJWT.Core.JWT;
using SimJWT.Core.JWT.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimJWT.Core.Authenticator
{
    public class JwtAuthenticationService<THeader, TPayload, TCrypter, TJsonSerialization>
        where THeader : Header
        where TPayload : Payload
        //where TCrypter : ICrypter, new()
        //where TJsonSerialization : IJSONSerialization, new()
        where TCrypter : ISignaturer
        where TJsonSerialization : IJSONSerialization
    {
        private Base64URL _base64;
        private TCrypter _crypter;
        private TJsonSerialization _jsonSerialization;
        private JwtFactory _jwtFactory;

        //public JwtAuthenticationService()
        //{
        //    _base64 = new Base64URL();
        //    _crypter = new TCrypter();
        //    _jsonSerialization = new TJsonSerialization();
        //    _jwtFactory = new JwtFactory(_base64, _jsonSerialization, _crypter);
        //}

        public JwtAuthenticationService(TCrypter c, TJsonSerialization j)
        {
            _base64 = new Base64URL();
            _jwtFactory = new JwtFactory(_base64, _jsonSerialization, _crypter);
            _crypter = c;
            _jsonSerialization = j;
        }

        public string IssueToken(THeader h, TPayload p) => _jwtFactory
            .GetJwtObject(h, p)
            .Jwt;

        public bool AuthenticationToken(string jwt) => GetTokenObject(jwt)
            .IsAuthorizedToken(_crypter);

        public Token<THeader, TPayload> GetTokenObject(string jwt) => _jwtFactory
            .GetJwtObject<THeader, TPayload>(jwt);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using SimJWT.Core.Base;
using SimJWT.Core.Common;
using SimJWT.Core.JWT.Structure;

namespace SimJWT.Core.JWT
{
    public class Token<THeader, TPayload>
        where THeader : Header
        where TPayload : Payload
    {
        public THeader Header { get; }
        public string Base64Header { get; }
        public TPayload Payload { get; }
        public string Base64Payload { get; }
        public string Signature { get; }
        public string Jwt { get; }

        public Token(THeader h, TPayload p, IBase64URL base64, IJSONSerialization serialization, ISignaturer crypter)
        {
            Header = h;
            Base64Header = base64.Encode(serialization.SerializeToString(h));

            Payload = p;
            Base64Payload = base64.Encode(serialization.SerializeToString(p));

            var encodedClearText = $"{Header}.{Payload}";

            Signature = encodedClearText;

            Jwt = $"{encodedClearText}.{Signature}";
        }

        public Token(string jwt, IBase64URL base64, IJSONSerialization serialization, ISignaturer crypter)
        {
            var arr = jwt.Split('.');

            Base64Header = arr[0];
            Header = serialization.DeserializeToObject<THeader>(base64.Decode(Base64Header));

            Base64Payload = arr[1];
            Payload = serialization.DeserializeToObject<TPayload>(base64.Decode(Base64Payload));

            Signature = arr[3];
        }

        public bool IsAuthorizedToken(ISignaturer crypter)
        {
            var encodedClearText = $"{Base64Header}.{Base64Payload}";

            if (encodedClearText == Signature)
            {
                return true;
            }
            return false;
        }

    }
}

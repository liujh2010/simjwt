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
        public string Base64Signature { get; }
        public string Jwt { get; }
        public bool AuthorizedToken { private set; get; }

        public Token(THeader h, TPayload p, IBase64URL coder, IJSONSerialization serialization, ISignaturer signaturer)
        {
            Header = h;
            Base64Header = coder.Encode(serialization.SerializeObject(h));

            Payload = p;
            Base64Payload = coder.Encode(serialization.SerializeObject(p));

            var encodedClearText = $"{Base64Header}.{Base64Payload}";
            Signature = signaturer.GetDigest(encodedClearText);
            Base64Signature = coder.Encode(Signature);

            Jwt = $"{Base64Header}.{Base64Payload}.{Base64Signature}";

            AuthorizedToken = true;
        }

        public Token(string jwt, IBase64URL coder, IJSONSerialization serialization, ISignaturer signaturer)
        {
            Jwt = jwt;

            var arr = jwt.Split('.');

            Base64Header = arr[0];
            Header = serialization.DeserializeObject<THeader>(coder.Decode(Base64Header));

            Base64Payload = arr[1];
            Payload = serialization.DeserializeObject<TPayload>(coder.Decode(Base64Payload));

            Base64Signature = arr[2];
            Signature = coder.Decode(Base64Signature);

            AuthorizedToken = IsAuthorizedToken(signaturer);
        }

        private bool IsAuthorizedToken(ISignaturer signaturer)
        {
            var encodedClearText = $"{Base64Header}.{Base64Payload}";
            return signaturer
                .GetDigest(encodedClearText)
                .Equals(Signature);
        }
    }
}

using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.core
{
    public class Token
    {
        public static string Secret = "NetJWT";

        //new Dictionary<string, object>{{ "name", "MrBug" },{ "exp",secondsSinceEpoch+100 }};
        public static string BuildToken(IDictionary payload, int exp=604800)
        {
            IDateTimeProvider provider = new UtcDateTimeProvider();
            var now = provider.GetNow();
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc); // or use JwtValidator.UnixEpoch
            var secondsSinceEpoch = Math.Round((now - unixEpoch).TotalSeconds);

            payload.Add("exp", secondsSinceEpoch + exp);

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload,Secret);
        }

        public static string DecryptToken(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                return decoder.Decode(token, Secret, verify: true);//token为之前生成的字符串
            }
            catch (TokenExpiredException)
            {
                return "Token has expired";
            }
            catch (SignatureVerificationException)
            {
                return "Token has invalid signature";
            }
        }

    }
}

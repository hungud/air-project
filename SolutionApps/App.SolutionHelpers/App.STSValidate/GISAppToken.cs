using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IdentityModel.Claims;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace App.STSValidate
{
    public class GISAppToken : SecurityToken
    {
        public static DateTime SwtBaseTime = new DateTime(1970, 1, 1, 0, 0, 0, 0); // per SWT psec

        NameValueCollection _properties;
        string _serilaizedToken;

        /// <summary>
                /// Initializes a new instance of the <see cref="SimpleWebToken"/> class.
                /// This is internal contructor is only called from the <see cref="SimpleWebTokenHandler"/> when reading a token received from the wire.
                /// </summary>
                /// <param name="properties">The collection represents all the key value pairs in the token.</param>
                /// <param name="serializedToken">The serialized form of the token.</param>
        internal GISAppToken(NameValueCollection properties, string serializedToken)
: this(properties)
        {
            _serilaizedToken = serializedToken;
        }

        /// <summary>
                /// Initializes a new instance of the <see cref="SimpleWebToken"/> class.
                /// </summary>
                /// <param name="properties">The collection represents all the key value pairs in the token.</param>
        public GISAppToken(NameValueCollection properties)
        {
            if (properties == null)
            {
                throw new ArgumentNullException("properties");
            }

            _properties = properties;
        }

        /// <summary>
                /// Gets the Id of the token.
                /// </summary>
                /// <value>The Id of the token.</value>
        public override string Id
        {
            get
            {
                return _properties[GisAppTokenConstants.Id];
            }
        }

        /// <summary>
                /// Gets the keys associated with this token.
                /// </summary>
                /// <value>The keys associated with this token.</value>
        public override ReadOnlyCollection<SecurityKey> SecurityKeys
        {
            get
            {
                return new ReadOnlyCollection<SecurityKey>(new List<SecurityKey>());
            }
        }

        /// <summary>
                /// Gets the time from when the token is valid.
                /// </summary>
                /// <value>The time from when the token is valid.</value>
        public override DateTime ValidFrom
        {
            get
            {
                string validFrom = _properties[GisAppTokenConstants.ValidFrom];
                return GetTimeAsDateTime(String.IsNullOrEmpty(validFrom) ? "0" : validFrom);
            }
        }

        /// <summary>
                /// Gets the time when the token expires.
                /// </summary>
                /// <value>The time upto which the token is valid.</value>
        public override DateTime ValidTo
        {
            get
            {
                string expiryTime = _properties[GisAppTokenConstants.ExpiresOn];
                return GetTimeAsDateTime(String.IsNullOrEmpty(expiryTime) ? "0" : expiryTime);
            }
        }

        /// <summary>
                /// Gets the Audience for the token.
                /// </summary>
                /// <value>The audience of the token.</value>
        public string Audience
        {
            get
            {
                return _properties[GisAppTokenConstants.Audience];
            }
        }

        /// <summary>
                /// Gets the Issuer for the token.
                /// </summary>
                /// <value>The issuer for the token.</value>
        public string Issuer
        {
            get
            {
                return _properties[GisAppTokenConstants.Issuer];
            }
        }

        /// <summary>
                /// Gets the signature for the token.
                /// </summary>
                /// <value>The signature for the token.</value>
        public string Signature
        {
            get
            {
                return _properties[GisAppTokenConstants.Signature];
            }
        }

        /// <summary>
                /// Gets the serialized form of the token if the token was created from its serialized form by the token handler.
                /// </summary>
                /// <value>The serialized form of the token.</value>
        public string SerializedToken
        {
            get
            {
                return _serilaizedToken;
            }
        }

        /// <summary>
                /// Creates a copy of all key value pairs of the token.
                /// </summary>
                /// <returns>A copy of all the key value pairs in the token.</returns>
        public NameValueCollection GetAllProperties()
        {
            return new NameValueCollection(_properties);
        }

        /// <summary>
                /// Convert the time in seconds to a <see cref="DateTime"/> object based on the base time
                /// defined by the Simple Web Token.
                /// </summary>
                /// <param name="expiryTime">The time in seconds.</param>
                /// <returns>The time as a <see cref="DateTime"/> object.</returns>
        protected virtual DateTime GetTimeAsDateTime(string expiryTime)
        {
            long totalSeconds = 0;
            if (!long.TryParse(expiryTime, out totalSeconds))
            {
                throw new SecurityTokenException("Invalid expiry time. Expected the time to be in seconds passed from 1 January 1970.");
            }

            long maxSeconds = (long)(DateTime.MaxValue - SwtBaseTime).TotalSeconds - 1;
            if (totalSeconds > maxSeconds)
            {
                totalSeconds = maxSeconds;
            }

            return SwtBaseTime.AddSeconds(totalSeconds);
        }
    }
}

////public class GISAppToken : SecurityToken
////{
////    string _id;
////    Uri _audienceUri;
////    List<Claim> _claims;
////    string _issuer;
////    DateTime _expiresOn;
////    string _signature;
////    DateTime _validFrom;
////    InMemorySymmetricSecurityKey _signingKey;
////    string _unsignedString;

////    public GISAppToken(Uri audienceUri, string issuer, DateTime expiresOn, List<Claim> claims, string signature, string unsignedString)
////    {
////        _audienceUri = audienceUri;
////        _issuer = issuer;
////        _expiresOn = expiresOn;
////        _claims = claims;
////        _signature = signature;
////        _unsignedString = unsignedString;
////    }

////    public GISAppToken(Uri audienceUri, string issuer, DateTime expiresOn, List<Claim> claims, InMemorySymmetricSecurityKey signingKey)
////    {
////        _audienceUri = audienceUri;
////        _issuer = issuer;
////        _expiresOn = expiresOn;
////        _claims = claims;
////        _signingKey = signingKey;
////    }

////    public override string Id
////    {
////        get { return _id; }
////    }

////    public override ReadOnlyCollection<SecurityKey> SecurityKeys
////    {
////        get
////        {
////            if (_signingKey != null)
////            {
////                return new ReadOnlyCollection<SecurityKey>(new List<System.IdentityModel.Tokens.SecurityKey> { _signingKey });
////            }
////            else
////            {
////                return new ReadOnlyCollection<SecurityKey>(new List<System.IdentityModel.Tokens.SecurityKey>());
////            }
////        }
////    }

////    public override DateTime ValidFrom
////    {
////        get { return _validFrom; }
////    }

////    public override DateTime ValidTo
////    {
////        get { return _expiresOn; }
////    }

////    public Uri AudienceUri
////    {
////        get { return _audienceUri; }
////    }

////    public string Issuer
////    {
////        get { return _issuer; }
////    }

////    public string Signature
////    {
////        get { return _signature; }
////    }

////    public List<Claim> Claims
////    {
////        get { return _claims; }
////    }

////    /// <summary>
////    /// Verifies the signature of the token.
////    /// </summary>
////    /// <param name="key">The key used for signing.</param>
////    /// <returns>true if the signatures match, false otherwise.</returns>
////    public bool VerifySignature(byte[] key)
////    {
////        if (key == null)
////        {
////            throw new ArgumentNullException("key");
////        }

////        if (_signature == null || _unsignedString == null)
////        {
////            throw new InvalidOperationException("Token has never been signed");
////        }

////        string verifySignature;

////        using (HMACSHA256 signatureAlgorithm = new HMACSHA256(key))
////        {
////            verifySignature = Convert.ToBase64String(signatureAlgorithm.ComputeHash(Encoding.ASCII.GetBytes(_unsignedString)));
////        }

////        return (ObfuscatingComparer.IsEqual(verifySignature, _signature));
////    }
////}






//public class GISAppToken : SecurityToken
//{
//    private static readonly TimeSpan lifeTime = new TimeSpan(0, 2, 0);
//    private static readonly DateTime epochStart = new DateTime
//                          (1970, 01, 01, 0, 0, 0, 0, DateTimeKind.Utc);
//    private NameValueCollection keyValuePairs;
//    private byte[] keyBytes = null;

//    public GISAppToken(string key)
//    {
//        TimeSpan ts = DateTime.UtcNow - epochStart + lifeTime;
//        this.ExpiresOn = Convert.ToUInt64(ts.TotalSeconds);
//        this.keyValuePairs = new NameValueCollection();

//        var securityKey = new InMemorySymmetricSecurityKey
//                                     (Convert.FromBase64String(key));
//        keyBytes = securityKey.GetSymmetricKey();
//    }

//    public string Issuer { get; set; }
//    public string Audience { get; set; }
//    public byte[] Signature { get; set; }
//    public ulong ExpiresOn { get; private set; }
//    public IList<Claim> Claims
//    {
//        get
//        {
//            return this.keyValuePairs.AllKeys
//                .SelectMany(key =>
//                    this.keyValuePairs[key].Split(',')
//                        .Select(value => new Claim(ClaimTypes.Role, key, value)))
//                           .ToList();
//        }
//    }

//    public void AddClaim(string name, string value)
//    {
//        this.keyValuePairs.Add(name, value);
//    }
//    public override string ToString()
//    {
//        StringBuilder content = new StringBuilder();

//        content.Append("Issuer=").Append(this.Issuer);

//        foreach (string key in this.keyValuePairs.AllKeys)
//        {
//            content.Append('&').Append(key)
//                .Append('=').Append(this.keyValuePairs[key]);
//        }

//        content.Append("&ExpiresOn=").Append(this.ExpiresOn);

//        if (!string.IsNullOrWhiteSpace(this.Audience))
//        {
//            content.Append("&Audience=").Append(this.Audience);
//        }

//        using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
//        {
//            byte[] signatureBytes = hmac.ComputeHash(
//                Encoding.ASCII.GetBytes(content.ToString()));

//            string signature = HttpUtility.UrlEncode(
//               Convert.ToBase64String(signatureBytes));

//            content.Append("&HMACSHA256=").Append(signature);
//        }

//        return content.ToString();
//    }

//    public static GISAppToken Parse(string token, string secretKey)
//    {
//        var items = HttpUtility.ParseQueryString(token);
//        var swt = new GISAppToken(secretKey);

//        foreach (string key in items.AllKeys)
//        {
//            string item = items[key];
//            switch (key)
//            {
//                case "Issuer": swt.Issuer = item; break;
//                case "Audience": swt.Audience = item; break;
//                case "ExpiresOn": swt.ExpiresOn = ulong.Parse(item); break;
//                case "HMACSHA256": swt.Signature =
//                                      Convert.FromBase64String(item); break;
//                default: swt.AddClaim(key, items[key]); break;
//            }
//        }

//        string rawToken = swt.ToString(); // Computes HMAC inside ToString()
//        string computedSignature = HttpUtility.ParseQueryString(rawToken)
//                                                           ["HMACSHA256"];

//        if (!computedSignature.Equals(Convert.ToBase64String(swt.Signature),
//                                                    StringComparison.Ordinal))
//            throw new SecurityTokenValidationException("Signature is invalid");

//        TimeSpan ts = DateTime.UtcNow - epochStart;

//        if (swt.ExpiresOn < Convert.ToUInt64(ts.TotalSeconds))
//            throw new SecurityTokenException("Token has expired");

//        return swt;
//    }
//}

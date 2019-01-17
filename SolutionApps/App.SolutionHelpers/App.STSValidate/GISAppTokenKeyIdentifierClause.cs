using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.STSValidate
{
    public class GISAppTokenKeyIdentifierClause  : SecurityKeyIdentifierClause
    {
        const string localId="GISAppToken";
        private string _audience;

        public GISAppTokenKeyIdentifierClause(string audience)
            :base (localId)
        {
            if (audience == null)
            {
                throw new ArgumentNullException("audience");
            }
            _audience = audience;
        }
 
        public string Audience
        {
            get
            {
                return _audience;
            }
        }
 
        public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
        {
            if (keyIdentifierClause is GISAppTokenKeyIdentifierClause)
            {
                return true;
            }
 
            return false;
        }
    }
}
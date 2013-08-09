using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace Coinbase.Net.Authentication
{
    public class ApiKeyAuthenticator : AuthenticatorBase
    {
        public override string FieldName
        {
            get { return "api_key"; }
        }
    }
}

using System;
using System.Security;

namespace Coinbase.NET.Extensions
{
    internal static class SecureStringHelper
    {
        internal static SecureString ToSecureString(this string value)
        {
			if(value == null) throw new ArgumentNullException("value");

		    var secureString = new SecureString();
            foreach (var c in value)
                secureString.AppendChar(c);
            
            return secureString;
		}
    }
}

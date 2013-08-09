using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbase.Net.Extensions
{
    internal static class StringExtensions
    {
        public static string AppendFormatIfNotNull(this string url, string currencyFormat, string currency)
        {
            return System.String.IsNullOrWhiteSpace(currency) ? url : url + System.String.Format(currencyFormat);
        }
    }
}

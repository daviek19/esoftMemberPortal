using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsoftPortalMvc.Services.Common
{
    public class PasswordComplexity
    {
        static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        static bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }

        public static bool IsValidPassword(string password)
        {
            bool anyLetter = password.Any(c => IsLetter(c));
            bool anyNumeric = password.Any(c => IsDigit(c));
            bool anySymbol = password.Any(c => IsSymbol(c));

            return (anyLetter && anyNumeric && anySymbol);
        }
    }
}
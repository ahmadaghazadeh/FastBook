using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace FastBookCreator.Shared
{
    public class Utility
    {
        public static string GetCurrentCultureTwoLetter()
        {
            return Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName.ToLower();
        }
        public static string RegexrResource = "getResource[(](\\d+)[)]";
        public static string RegexrNumber = "\\d+";
    }
}
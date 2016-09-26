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
    }
}
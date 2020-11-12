

using System;
using System.Text.RegularExpressions;

namespace AcademiaFuncaBolivia.Commons
{
    public static class Contants
    {
        public static string LOGIN_URL = "https://academia.funcabolivia.com/wp-login.php";
        public static string INDEX_URL = "https://academia.funcabolivia.com/";
        public static string DOMAIN_URL = "academia.funcabolivia.com";
        public static int EXPIRED_TIME = 5; //minutes


        public static string ReplaceWholeWord(this string original, string wordToFind, string replacement, RegexOptions regexOptions = RegexOptions.None)
        {
            string pattern = String.Format(@"\b{0}\b", wordToFind);
            string ret = Regex.Replace(original, pattern, replacement, regexOptions);
            return ret;
        }
    }

}
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace _24_WEB2_ASP.NET_MVC.Helpers
{
    public class StringUtils
    {
        public static string Md5(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.Default.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            string res = BitConverter.ToString(hash).Replace("-", "");

            return res;
        }

        /// <summary>
        /// Link: http://asp.net.vn/Modules/ASPNETVN.PORTAL.Modules.CMS/Pages/Print.aspx?itemid=967
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string convertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }  
    }
}
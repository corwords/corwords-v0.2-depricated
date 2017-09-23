using System.ComponentModel.DataAnnotations;

namespace Corwords.Web.Extensions
{
    public class PasswordAttribute : RegularExpressionAttribute
    {
        public PasswordAttribute() : base(GetRegex()) { }

        private static string GetRegex()
        {
            //MinLength(6), 
            return @""; // @"^[\w-]+(\.[\w-]+)*@([a-z0-9-]+(\.[a-z0-9-]+)*?\.[a-z]{2,6}|(\d{1,3}\.){3}\d{1,3})(:\d{4})?$";
        }
    }
}

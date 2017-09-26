using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Corwords.Web.Extensions
{
    public class PasswordAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            // If value is null or empty, return true as required should be used in the view model
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return true;

            var password = value.ToString();

            // Minimum length of 6
            if (password.Length < 6)
                return false;

            // For now, let's allow special characters and spaces
            //// Special Characters - Not Allowed
            //// Spaces - Not Allowed
            //if (!(password.All(c => char.IsLetter(c) || char.IsDigit(c))))
            //    return false;

            // Numeric Character - At least one character
            if (!password.Any(c => char.IsDigit(c)))
                return false;

            // At least one Letter
            if (!password.Any(c => char.IsLetter(c)))
                return false;

            // Repetitive Characters - Allowed only two repetitive characters
            var repeatCount = 0;
            var lastChar = '\0';
            foreach (var c in password)
            {
                if (c == lastChar)
                    repeatCount++;
                else
                    repeatCount = 0;
                if (repeatCount == 2)
                    return false;
                lastChar = c;
            }

            return true;
        }
    }
}
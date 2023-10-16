using System.Text.RegularExpressions;

namespace PropertySolutionCustomerPortal.Domain.Helper
{
    public static class ValidationHelper
    {
        public static void CheckIsNull(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException("No details received.");
        }

        public static void CheckException(bool value, string message)
        {
            if (value)
                throw new ArgumentException(message);
        }

        public static void ValidateEmail(string email)
        {
            Regex regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.IsMatch(email))
                throw new ArgumentException("Invalid email address.");
        }

        public static void ValidatePasswordFormat(string password)
        {
            bool hasLowercase = password.Any(char.IsLower);
            bool hasUppercase = password.Any(char.IsUpper);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

            if (!hasLowercase)
                throw new ArgumentException("Password must contain at least one lowercase letter.");

            if (!hasUppercase)
                throw new ArgumentException("Password must contain at least one uppercase letter.");

            if (!hasDigit)
                throw new ArgumentException("Password must contain at least one digit.");

            if (!hasSpecialChar)
                throw new ArgumentException("Password must contain at least one special character.");
        }

        public static void ValidateEnum<TEnum>(TEnum value, string fieldName) where TEnum : Enum
        {
            if (!Enum.IsDefined(typeof(TEnum), value))
                throw new ArgumentException($"{fieldName} is not a valid value.");
        }
    }
}

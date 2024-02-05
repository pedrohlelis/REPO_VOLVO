using System.Text.RegularExpressions;

namespace TRABALHO_VOLVO
{
    public class ValidationHelper
    {
        public static void ValidateNameFormat(string? value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        public static void ValidateNumericFormat(string? value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.All(char.IsDigit))
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        public static void ValidateEmailFormat(string? email,string errorMessage)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex re = new Regex(pattern);
            if(string.IsNullOrWhiteSpace(email) || !re.IsMatch(email))
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        
    }
}

using System.Text.RegularExpressions;

namespace FoodServiceAPI.Shared.Validator
{
    /// <summary>
    /// Utility class for validating CPF (Brazilian individual taxpayer registry) and CNPJ (Brazilian company taxpayer registry) numbers.
    /// </summary>
    public static class CpfCnpjValidator
    {
        /// <summary>
        /// Validates a CPF number.
        /// </summary>
        /// <param name="cpf">The CPF number to validate.</param>
        /// <returns>True if the CPF number is valid, otherwise false.</returns>
        public static bool IsValidCpf(string cpf)
        {
            // Remove non-numeric characters
            cpf = Regex.Replace(cpf, @"\D", "");

            // Check if the number of digits is 11
            if (cpf.Length != 11)
                return false;

            // Check if all digits are the same
            if (new string(cpf[0], cpf.Length) == cpf)
                return false;

            // Calculate the first check digit
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (10 - i) * (cpf[i] - '0');

            int digit1 = 11 - sum % 11;
            if (digit1 >= 10)
                digit1 = 0;

            // Calculate the second check digit
            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (11 - i) * (cpf[i] - '0');

            int digit2 = 11 - sum % 11;
            if (digit2 >= 10)
                digit2 = 0;

            // Check if the calculated digits match the provided digits
            return cpf[9] - '0' == digit1 && cpf[10] - '0' == digit2;
        }

        /// <summary>
        /// Validates a CNPJ number.
        /// </summary>
        /// <param name="cnpj">The CNPJ number to validate.</param>
        /// <returns>True if the CNPJ number is valid, otherwise false.</returns>
        public static bool IsValidCnpj(string cnpj)
        {
            // Remove non-numeric characters
            cnpj = Regex.Replace(cnpj, @"\D", "");

            // Check if the number of digits is 14
            if (cnpj.Length != 14)
                return false;

            // Check if all digits are the same
            if (new string(cnpj[0], cnpj.Length) == cnpj)
                return false;

            // Calculate the first check digit
            int sum = 0;
            int multiplier = 2;
            for (int i = 11; i >= 0; i--)
            {
                sum += (cnpj[i] - '0') * multiplier;
                multiplier = multiplier == 9 ? 2 : multiplier + 1;
            }
            int digit1 = sum % 11;
            digit1 = digit1 < 2 ? 0 : 11 - digit1;

            // Calculate the second check digit
            sum = 0;
            multiplier = 2;
            for (int i = 12; i >= 0; i--)
            {
                sum += (cnpj[i] - '0') * multiplier;
                multiplier = multiplier == 9 ? 2 : multiplier + 1;
            }
            int digit2 = sum % 11;
            digit2 = digit2 < 2 ? 0 : 11 - digit2;

            // Check if the calculated digits match the provided digits
            return cnpj[12] - '0' == digit1 && cnpj[13] - '0' == digit2;
        }
    }
}

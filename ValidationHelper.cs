using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace TRABALHO_VOLVO
{
    public class ValidationHelper
    {
        public static void CheckIntPK(string value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.All(c => char.IsDigit(c)))
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

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

        public static void ValidateEmailFormat(string? email, string errorMessage)
        {
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex re = new Regex(pattern);
            if (string.IsNullOrWhiteSpace(email) || !re.IsMatch(email))
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        public static void ValidateAlphaNumericFormat(string? value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value) || (!value.Any(char.IsDigit) && !value.Any(char.IsLetter)))
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        public static void ValidateAlphaFormat(string? value, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(value) || !value.All(char.IsLetter))
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        public static void ValidateDateOnly(string? value, string errorMessage)
        {
            if (DateOnly.TryParse(value, out DateOnly dateOnlyValue))
            {
                return;
            }
            else
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        public static bool IsValidDouble(string value, string errorMessage)
        {
            if (double.TryParse(value, out double result))
            {
                return true;
            }
            else
            {
                throw new FormatoInvalidoException(errorMessage);
            }
        }

        public static void ValidatePostVenda(VendaCaminhao vendaCaminhao, TrabalhoVolvoContext _context)
        {
            var conc = _context.Concessionarias.FirstOrDefault(c => c.CodConc == vendaCaminhao.FkConcessionariasCodConc);
            if (!_context.Funcionarios.Any(c => (c.CodFuncionario == vendaCaminhao.FkFuncionariosCodFuncionario) && c.FkConcessionariasCodConc == conc.CodConc))
            {
                throw new FKNotFoundException("Nenhum Funcionario registrado nessa concessionaria possui esse codigo.");
            }
        }
        public static void FilterExceptionsPostVendaCaminhao(string message)
        {
            if (message == "The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Caminhoes_Clientes\". The conflict occurred in database \"TrabalhoVolvo\", table \"dbo.Clientes\", column 'CodCliente'.")
            {
                throw new FKNotFoundException("Nenhum cliente registrado possui esse codigo.");
            }
            else if (message == "The INSERT statement conflicted with the FOREIGN KEY constraint \"Fk_VendaCaminhoes_Concessionarias_CodConc\". The conflict occurred in database \"TrabalhoVolvo\", table \"dbo.Concessionarias\", column 'CodConc'.")
            {
                throw new FKNotFoundException("Nenhuma concessionaria registrada possui esse codigo.");
            }
            else if (message == "The INSERT statement conflicted with the FOREIGN KEY constraint \"Fk_VendaCaminhoes_Funcionarios_CodFuncionario\". The conflict occurred in database \"TrabalhoVolvo\", table \"dbo.Funcionarios\", column 'CodFuncionario'.")
            {
                throw new FKNotFoundException("Nenhum Funcionario registrado possui esse codigo.");
            }
            else if (message == "Object reference not set to an instance of an object.")
            {
                throw new FKNotFoundException("Nenhum caminhao do estoque registrado possui esse codigo.");
            }
        }
    }
}

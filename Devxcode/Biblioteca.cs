using System;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Devxcode
{
    public static class Biblioteca
    {

        #region Válidação de Número de Documentos de Pessoa Física e Pessoa Juridica
        
        /// <summary>
        /// Verifica se valor informado é número de PIS
        /// </summary>
        /// <param name="PIS">Pis a ser verificado</param>
        /// <exception cref="ArgumentNullException">Exceção lançada quando argumento <paramref name="PIS"/> estiver vázio ou nulo.</exception>
        /// <exception cref="ArgumentException">Exceção lançada quando parametro não for número de PIS</exception>
        /// <exception cref="Exception"></exception>
        /// <returns>[True] PIS válido || [False] PIS inválido</returns>
        public static bool IsPis(this string PIS)
        {
            try
            {
                if (string.IsNullOrEmpty(PIS))
                    throw new ArgumentNullException($"Parâmetro {nameof(PIS)} não pode ser nulo ou vazio.");

                PIS = PIS.Trim().Replace("-", "").Replace(".", "").PadLeft(11, '0');

                if (!PIS.IsNumeric())
                    throw new ArgumentException($"Parâmetro {nameof(PIS)} não é númerico");

                if (PIS.Trim().Length != 11)
                    throw new ArgumentException($"O valor do parâmetro {nameof(PIS)} inválido.");

                int[] multiplicador = new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                int soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(PIS[i].ToString()) * multiplicador[i];


                int resto = (soma % 11) < 2 ? 0 : 11 - (soma % 11);

                return PIS.EndsWith(resto.ToString());
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException($"Operação abortada. Motivo: {e.Message}");
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw new Exception($"Operação abortada. Motivo: {e.Message}");
            }
        }

        /// <summary>
        /// Validação de CNPJ
        /// </summary>
        /// <param name="CNPJ"></param>
        /// <returns>[True] CNPJ válido || [False] CNPJ inválido</returns>
        public static bool IsCNPJ(this string CNPJ)
        {
            try
            {
                CNPJ = CNPJ.Trim().Replace(".", "").Replace("-", "").Replace("/", "");

                if (string.IsNullOrEmpty(CNPJ))
                    throw new ArgumentNullException($"Parâmetro {nameof(CNPJ)} não pode ser vázio ou nulo.");

                if (!CNPJ.IsNumeric())
                    throw new ArgumentException($"Parâmetro {nameof(CNPJ)} não é númerico");
                
                if (CNPJ.Length != 14)
                    throw new ArgumentException($"Parâmetro {nameof(CNPJ)} não possue a quantidade de digitos de um CNPJ.");

                if (CNPJ.IsInvalidNumberSeq(CNPJ.Length))
                    throw new ArgumentException($"Parãmetro {nameof(CNPJ)} possui todos os caracteres iguais.");

                var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                var tempCnpj = CNPJ.Substring(0, 12);

                var soma = 0;

                for (var i = 0; i < 12; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];


                var resto = (soma % 11);

                resto = resto < 2 ? 0 : 11 - resto;

                var digito = resto.ToString();

                tempCnpj += digito;

                soma = 0;

                for (var i = 0; i < 13; i++)
                    soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);

                resto = resto < 2 ? 0 : 11 - resto;

                digito += resto.ToString();

                return CNPJ.EndsWith(digito);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw new Exception($"Operação abortada. Ocorreu um erro não tratado. Motivo: {e.Message}");
            }
        }

        /// <summary>
        /// Validação de CPF
        /// </summary>
        /// <param name="CPF">Número de CPF</param>
        /// <returns>[True] CPF válido || [False] CPF inválido</returns>
        public static bool IsCPF(this string CPF)
        {
            try
            {

                CPF = CPF.Trim().Replace(".", "").Replace("-", "");

                if (string.IsNullOrEmpty(CPF))
                    throw new ArgumentNullException($"Parâmetro {nameof(CPF)} não pode ser vázio ou nulo.");

                if (!CPF.IsNumeric())
                    throw new ArgumentException($"Parâmetro {nameof(CPF)} não é númerico");

                if (CPF.Length != 11)
                    throw new ArgumentException($"Parâmetro {nameof(CPF)} não possue a quantidade de digitos de um CPF.");

                if (CPF.IsInvalidNumberSeq(CPF.Length))
                    throw new ArgumentException($"Parãmetro {nameof(CPF)} possui todos os caracteres iguais.");

                var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                var tempCpf = CPF.Substring(0, 9);

                var soma = 0;

                for (var i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

                var resto = soma % 11;

                resto = resto < 2 ? 0 : 11 - resto;

                var digito = resto.ToString();

                tempCpf += digito;

                soma = 0;

                for (var i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

                resto = soma % 11;

                resto = resto < 2 ? 0 : 11 - resto;

                digito += resto.ToString();

                return CPF.EndsWith(digito);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Exception e)
            {
                throw new Exception($"Operação abortada. Ocorreu um erro não trato. Motivo: {e.Message}");
            }
        }

        #endregion
        /// <summary>
        /// Verifica se o parâmetro <paramref name="value"/> é númerico
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string value) => value.Where(x => char.IsNumber(x)).Count() > 0;

        /// <summary>
        /// Remove todas as acentuação da string informada via parâmetrização
        /// </summary>
        /// <param name="texto">Valor a ser removido as acentuações</param>
        /// <returns>Texto sem acentuação</returns>
        public static string RemoveAccents(this string texto)
        {
            var conteudo = texto.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();

            for (var i = 0; i < conteudo.Length; i++)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(conteudo[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                    builder.Append(conteudo[i]);
            }
            return builder.ToString();
        }

        /// <summary>
        /// Valida se todos os caracteres são sequencias de 0 a 9
        /// </summary>
        /// <param name="value">valor a ser comparado.</param>
        /// <param name="length">tamanho do valor</param>
        /// <returns>[True] Todos os caracteres são iguais || [False] Os caracteres não são iguais</returns>
        public static bool IsInvalidNumberSeq(this string value, int length)
        {
            try
            {
                if(!value.IsNumeric())
                    throw new Exception($"Parâmetro {nameof(value)} não é númerico!");

                for (var i = 0; i < 10; i++)
                {
                    var ValueRepeat = "";
                    foreach (var enumerable in Enumerable.Repeat(i, length))
                        ValueRepeat += enumerable.ToString();
                    
                    if (value == ValueRepeat)
                        throw new Exception($"Todos os caracteres informados no parâmetro {nameof(value)} são iguais a escala. Escala: 0 à 9 !");
                }

                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }

}

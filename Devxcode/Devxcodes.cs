namespace Devxcode
{
    public partial class Devxcodes
    {
        public static string MessageMandatoryDefault(string value)
        {
            return $"Parâmetro {nameof(value)} de preenchimento obrigatório.";
        }

        public static string ErroDefault(string MessagemError)
        {
            return $"Operação abortada. Motivo: {MessagemError}";
        }
    }
}

namespace MultRHAPI.Validators
{
    public class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf = cpf.Substring(0, 9);
            int soma = multiplicador1.Select((m, i) => m * (tempCpf[i] - '0')).Sum();
            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;
            tempCpf += digito1;
            soma = multiplicador2.Select((m, i) => m * (tempCpf[i] - '0')).Sum();
            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;
            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}

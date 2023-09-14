﻿using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace WebZi.Plataform.CrossCutting.Documents
{
    public abstract class DocumentHelper
    {
        public static bool IsCPF(string cpf)
        {
            //915.331.274-09 (CPF VÁLIDO)

            cpf = cpf.Trim();

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }
            else if (!Regex.IsMatch(cpf, @"^\d+$"))
            {
                return false;
            }
            else if (cpf.Length != 11)
            {
                return false;
            }
            else if (cpf.Distinct().Count().Equals(11))
            {
                return false;
            }

            if (Regex.IsMatch(cpf, @"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)"))
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCPF;
                string digito;

                int soma;
                int resto;

                tempCPF = cpf.Substring(0, 9);

                soma = 0;

                for (int i = 0; i < 9; i++)
                {
                    soma += int.Parse(tempCPF[i].ToString()) * multiplicador1[i];
                }

                resto = soma % 11;

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = resto.ToString();

                tempCPF += digito;

                soma = 0;

                for (int i = 0; i < 10; i++)
                {
                    soma += int.Parse(tempCPF[i].ToString()) * multiplicador2[i];
                }

                resto = soma % 11;

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito += resto.ToString();

                return cpf.EndsWith(digito);
            }
            else
            {
                return false;
            }
        }

        public static bool IsCNPJ(string cnpj)
        {
            //11.519.458/0001-78 (CNPJ VÁLIDO)

            cnpj = cnpj.Trim();
            
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return false;
            }
            else if (!Regex.IsMatch(cnpj, @"^\d+$"))
            {
                return false;
            }
            else if (cnpj.Length != 14)
            {
                return false;
            }
            else if (cnpj.Distinct().Count().Equals(14))
            {
                return false;
            }

            if (Regex.IsMatch(cnpj, @"(^(\d{2}.\d{3}.\d{3}/\d{4}-\d{2})|(\d{14})$)"))
            {
                int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                int soma;
                int resto;
                string digito;
                string tempCNPJ;

                cnpj = cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);

                tempCNPJ = cnpj.Substring(0, 12);

                soma = 0;

                for (int i = 0; i < 12; i++)
                {
                    soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador1[i];
                }

                resto = (soma % 11);

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito = resto.ToString();

                tempCNPJ += digito;

                soma = 0;

                for (int i = 0; i < 13; i++)
                {
                    soma += int.Parse(tempCNPJ[i].ToString()) * multiplicador2[i];
                }

                resto = (soma % 11);

                if (resto < 2)
                {
                    resto = 0;
                }
                else
                {
                    resto = 11 - resto;
                }

                digito += resto.ToString();

                return cnpj.EndsWith(digito);
            }
            else
            {
                return false;
            }
        }

        public static bool IsCNH(string cnh)
        {
            cnh = cnh.Trim();

            if (string.IsNullOrWhiteSpace(cnh))
            {
                return false;
            }
            else if (!Regex.IsMatch(cnh, @"^\d+$"))
            {
                return false;
            }
            else if (cnh.Length != 9 && cnh.Length != 11)
            {
                return false;
            }
            else if (cnh.Distinct().Count().Equals(11))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
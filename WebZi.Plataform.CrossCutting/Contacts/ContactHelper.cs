using WebZi.Plataform.CrossCutting.Number;

namespace WebZi.Plataform.CrossCutting.Contacts
{
    public abstract class ContactHelper
    {
        public static bool IsTelephone(string telephone)
        {
            telephone = telephone.Replace("-", "").Trim();

            if (string.IsNullOrWhiteSpace(telephone))
            {
                return false;
            }
            else if (!NumberHelper.IsNumber(telephone))
            {
                return false;
            }
            else if (telephone.Length != 8)
            {
                return false;
            }

            return true;
        }

        public static bool IsCellphone(string cellphone)
        {
            cellphone = cellphone.Replace("-", "").Trim();

            if (string.IsNullOrWhiteSpace(cellphone))
            {
                return false;
            }
            else if (!NumberHelper.IsNumber(cellphone))
            {
                return false;
            }
            else if (cellphone.Length != 9)
            {
                return false;
            }

            return true;
        }

        public static bool IsTelephoneOrCellphone(string phone)
        {
            phone = phone.Replace("-", "").Trim();

            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }
            else if (!NumberHelper.IsNumber(phone))
            {
                return false;
            }
            else if (phone.Length < 8 || phone.Length > 9)
            {
                return false;
            }

            return true;
        }

        public static bool IsDDD(string ddd)
        {
            ddd = ddd.Trim();

            if (string.IsNullOrWhiteSpace(ddd))
            {
                return false;
            }
            else if (!NumberHelper.IsNumber(ddd))
            {
                return false;
            }

            return Convert.ToInt16(ddd) switch
            {
                // Centro-Oeste
                // Distrito Federal
                61 or 62 or 64 or 65 or 66 or 67 => true,
                
                // Nordeste
                // Alagoas
                82 => true,
                
                // Bahia
                71 or 73 or 74 or 75 or 77 => true,
                
                // Ceará
                85 or 88 => true,
                
                // Maranhão
                98 or 99 => true,
                
                // Paraíba
                83 => true,
                
                // Pernambuco
                81 or 87 => true,
                
                // Piauí
                86 or 89 => true,
                
                // Rio Grande do Norte
                84 => true,
                
                // Sergipe
                79 => true,
                
                // Norte
                // Acre
                68 => true,
                
                // Amapá
                96 => true,
                
                // Amazonas
                92 or 97 => true,
                
                // – Pará
                91 or 93 or 94 => true,
                
                // – Rondônia
                69 => true,
                
                // – Roraima
                95 => true,
                
                // – Tocantins
                63 => true,
                
                // Sudeste
                // – Espírito Santo
                27 or 28 => true,
                
                // – Minas Gerais
                31 or 32 or 33 or 34 or 35 or 37 or 38 => true,
                
                // – Rio de Janeiro
                21 or 22 or 24 => true,
                
                // – São Paulo
                11 or 12 or 13 or 14 or 15 or 16 or 17 or 18 or 19 => true,
                
                // Sul
                // – Paraná
                41 or 42 or 43 or 44 or 45 or 46 => true,
                
                // – Rio Grande do Sul
                51 or 53 or 54 or 55 => true,
                
                // – Santa Catarina
                47 or 48 or 49 => true,
                _ => false,
            };
        }
    }
}
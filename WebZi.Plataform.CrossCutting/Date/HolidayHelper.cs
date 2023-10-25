namespace WebZi.Plataform.CrossCutting.Date
{
    public static class HolidayHelper
    {
        #region GET DOMINGO DE PÁSCOA
        /// <summary>
        /// Retorna o domingo de páscoa de um determinado ano
        /// </summary>
        public static DateTime GetDomingoDePascoa(int ano)
        {
            int a = ano % 19;

            int b = ano / 100;

            int c = ano % 100;

            int d = b / 4;

            int e = b % 4;

            int f = (b + 8) / 25;

            int g = (b - f + 1) / 3;

            int h = (19 * a + b - d - g + 15) % 30;

            int i = c / 4;

            int k = c % 4;

            int L = (32 + 2 * e + 2 * i - h - k) % 7;

            int m = (a + 11 * h + 22 * L) / 451;

            int mes = (h + L - 7 * m + 114) / 31;

            int dia = ((h + L - 7 * m + 114) % 31) + 1;


            return new DateTime(ano, mes, dia);
        }
        #endregion GET DOMINGO DE PÁSCOA


        #region GET SEXTA-FEIRA DA PAIXÃO
        /// <summary>
        /// Retorna o domingo de carnaval de um determinado ano
        /// </summary>
        public static DateTime GetSextaFeiraPaixao(int ano)
        {
            return GetDomingoDePascoa(ano).AddDays(-2);
        }
        #endregion GET SEXTA-FEIRA DA PAIXÃO


        #region GET CORPUS CHRISTI
        /// <summary>
        /// Retorna Corpus Christi
        /// </summary>
        public static DateTime GetCorpusChristi(int ano)
        {
            return GetDomingoDePascoa(ano).AddDays(50);
        }
        #endregion GET CORPUS CHRISTI


        #region GET DOMINGO DE CARNAVAL
        /// <summary>
        /// Retorna o domingo de carnaval de um determinado ano
        /// </summary>
        public static DateTime GetDomingoDeCarnaval(int ano)
        {
            return GetDomingoDePascoa(ano).AddDays(-49);
        }
        #endregion GET DOMINGO DE CARNAVAL


        #region GET SEGUNDA-FEIRA DE CARNAVAL
        /// <summary>
        /// Retorna a segunda-feira de carnaval de um determinado ano
        /// </summary>
        public static DateTime GetSegundaFeiraCarnaval(int ano)
        {
            return GetDomingoDeCarnaval(ano).AddDays(1);
        }
        #endregion GET SEGUNDA-FEIRA DE CARNAVAL


        #region GET TERÇA-FEIRA DE CARNAVAL
        /// <summary>
        /// Retorna a terça-feira de carnaval de um determinado ano
        /// </summary>
        public static DateTime GetTercaFeiraCarnaval(int ano)
        {
            return GetDomingoDeCarnaval(ano).AddDays(2);
        }
        #endregion GET TERÇA-FEIRA DE CARNAVAL


        #region GET QUARTA-FEIRA DE CINZAS
        /// <summary>
        /// Retorna a Quarta-Feira de Cinzas de um determinado ano
        /// </summary>
        public static DateTime GetQuartaFeiraDeCinzas(int ano)
        {
            return GetDomingoDeCarnaval(ano).AddDays(4);
        }
        #endregion GET QUARTA-FEIRA DE CINZAS


        #region IS ANO NOVO
        /// <summary>
        /// Verifica se o dia é Ano Novo
        /// </summary>
        public static bool IsAnoNovo(DateTime inputDate)
        {
            if (inputDate.Month == 1 && inputDate.Day == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS ANO NOVO


        #region IS CARNAVAL
        /// <summary>
        /// Verifica se o dia é terça-feira de Carnaval
        /// </summary>
        public static bool IsCarnaval(DateTime inputDate)
        {
            if (inputDate.Date == GetTercaFeiraCarnaval(inputDate.Year).Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS CARNAVAL


        #region IS SEXTA-FEIRA DA PAIXÃO
        /// <summary>
        /// Retorna o domingo de carnaval de um determinado ano
        /// </summary>
        public static bool IsSextaFeiraPaixao(DateTime inputDate)
        {
            if (inputDate.Date == GetSextaFeiraPaixao(inputDate.Year))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS SEXTA-FEIRA DA PAIXÃO


        #region IS TIRADENTES
        /// <summary>
        /// Verifica se o dia é Dia e Tiradentes
        /// </summary>
        public static bool IsTiradentes(DateTime inputDate)
        {
            if (inputDate.Month == 4 && inputDate.Day == 21)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS TIRADENTES


        #region IS DIA DO TRABALHO
        /// <summary>
        /// Verifica se o dia é Dia do Trabalho
        /// </summary>
        public static bool IsDiaDoTrabalho(DateTime inputDate)
        {
            if (inputDate.Month == 5 && inputDate.Day == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS DIA DO TRABALHO


        #region IS CORPUS CHRISTI
        /// <summary>
        /// Verifica se o dia é Corpus Christi
        /// </summary>
        public static bool IsCorpusChristi(DateTime inputDate)
        {
            if (inputDate.Date == GetCorpusChristi(inputDate.Year).Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS CORPUS CHRISTI


        #region IS DIA DA INDEPENDÊNCIA NO BRASIL
        /// <summary>
        /// Verifica se o dia é Dia da Independência no Brasil
        /// </summary>
        public static bool IsDiaDaIndependencia(DateTime inputDate)
        {
            if (inputDate.Month == 9 && inputDate.Day == 7)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS DIA DA INDEPENDÊNCIA NO BRASIL


        #region IS NOSSA SENHORA APARECIDA
        /// <summary>
        /// Verifica se o dia é Nossa Senhora Aparecida
        /// </summary>
        public static bool IsNossaSenhoraAparecida(DateTime inputDate)
        {
            if (inputDate.Month == 10 && inputDate.Day == 12)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS NOSSA SENHORA APARECIDA


        #region IS FINADOS
        /// <summary>
        /// Verifica se o dia é Finados
        /// </summary>
        public static bool IsFinados(DateTime inputDate)
        {
            if (inputDate.Month == 11 && inputDate.Day == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS FINADOS


        #region IS PROCLAMAÇÃO DA REPÚBLICA
        /// <summary>
        /// Verifica se o dia é Dia da Proclamação Da República
        /// </summary>
        public static bool IsProclamacaoDaRepublica(DateTime inputDate)
        {
            if (inputDate.Month == 11 && inputDate.Day == 15)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS PROCLAMAÇÃO DA REPÚBLICA


        #region IS NATAL
        /// <summary>
        /// Verifica se o dia é Natal
        /// </summary>
        public static bool IsNatal(DateTime inputDate)
        {
            if (inputDate.Month == 12 && inputDate.Day == 25)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS NATAL


        #region IS HOLIDAY
        /// <summary>
        /// Verifica se o dia é Feriado
        /// </summary>
        public static bool IsHoliday(DateTime inputDate)
        {
            if (IsAnoNovo(inputDate) || IsSextaFeiraPaixao(inputDate) || IsTiradentes(inputDate) || IsDiaDoTrabalho(inputDate) || IsNossaSenhoraAparecida(inputDate) || IsFinados(inputDate) || IsDiaDaIndependencia(inputDate) || IsProclamacaoDaRepublica(inputDate) || IsNatal(inputDate))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion IS HOLIDAY
    }
}
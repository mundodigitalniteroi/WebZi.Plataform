using System.Globalization;
using WebZi.Plataform.CrossCutting.Sistema;

namespace WebZi.Plataform.CrossCutting.Date
{
    public static class DateTimeHelper
    {
        public enum DateTimeFormat
        {
            #region DATE
            [EnumHelper.StringValue("ddMMyyyy")] Date,

            [EnumHelper.StringValue("MMddyyyy")] EnglishDate,

            [EnumHelper.StringValue("dd/MM/yyyy")] DateFormatted,

            [EnumHelper.StringValue("MM/dd/yyyy")] EnglishDateFormatted,
            #endregion DATE

            #region DATETIME
            [EnumHelper.StringValue("ddMMyyyy HHmmss")] DateTime,

            [EnumHelper.StringValue("dd/MM/yyyy HH:mm:ss")] DateTimeFormatted,

            [EnumHelper.StringValue("MMddyyyy HHmmss")] EnglishDateTime,

            [EnumHelper.StringValue("MM/dd/yyyy HH:mm:ss")] EnglishDateTimeFormatted,
            #endregion DATETIME

            #region EXTEND
            [EnumHelper.StringValue("yyyyMMdd")] ExtendYearToDay,

            [EnumHelper.StringValue("yyyy-MM-dd")] ExtendYearToDayFormatted,

            [EnumHelper.StringValue("yyyyMMdd HH:mm")] ExtendYearToDayNotFormattedAndHourToMinuteFormatted,

            [EnumHelper.StringValue("yyyy-MM-dd HH:mm")] ExtendYearToDayFormattedAndHourToMinuteFormatted,

            [EnumHelper.StringValue("yyyyMMdd HH:mm:ss")] ExtendYearToDayNotFormattedAndHourToSecondFormatted,

            [EnumHelper.StringValue("yyyy-MM-dd HH:mm:ss")] ExtendYearToDayFormattedAndHourToSecondFormatted,

            [EnumHelper.StringValue("yyyyMM")] ExtendYearToMonth,

            [EnumHelper.StringValue("yyyy-MM")] ExtendYearToMonthFormatted,

            [EnumHelper.StringValue("yyyyMMdd HHmm")] ExtendYearToMinute,

            [EnumHelper.StringValue("yyyy-MM-dd HH:mm")] ExtendYearToMinuteFormatted,

            [EnumHelper.StringValue("yyyyMMdd HHmmss")] ExtendYearToSecond,

            [EnumHelper.StringValue("yyyy-MM-dd HH:mm:ss")] ExtendYearToSecondFormatted,
            #endregion EXTEND

            #region SMALLDATETIME
            [EnumHelper.StringValue("ddMMyyyy HHmm")] SmallDateTime,

            [EnumHelper.StringValue("dd/MM/yyyy HH:mm")] SmallDateTimeFormatted,

            [EnumHelper.StringValue("MMddyyyy HHmm")] EnglishSmallDateTime,

            [EnumHelper.StringValue("MM/dd/yyyy HH:mm")] EnglishSmallDateTimeFormatted,
            #endregion SMALLDATETIME

            #region SMALLTIME
            [EnumHelper.StringValue("HHmm")] SmallTime,

            [EnumHelper.StringValue("HH:mm")] SmallTimeFormatted,
            #endregion SMALLTIME

            #region TIME
            [EnumHelper.StringValue("HHmmss")] Time,

            [EnumHelper.StringValue("HH:mm:ss")] TimeFormatted,
            #endregion TIME

            #region UNIQUE
            [EnumHelper.StringValue("dd")] Day,

            [EnumHelper.StringValue("MM")] Month,

            [EnumHelper.StringValue("yyyy")] Year,

            [EnumHelper.StringValue("HH")] Hour,

            [EnumHelper.StringValue("mm")] Minute,

            [EnumHelper.StringValue("ss")] Second
            #endregion UNIQUE
        }

        public static DateTime AddDays(DateTime inputDateTime, int days)
        {
            return inputDateTime.AddDays(days);
        }

        public static DateTime AddMonths(DateTime inputDateTime, int months)
        {
            return inputDateTime.AddMonths(months);
        }

        public static DateTime AddYears(DateTime inputDateTime, int years)
        {
            return inputDateTime.AddYears(years);
        }

        public static long ConvertDateTimeToJulianDate(DateTime Date)
        {
            int Month = Date.Month;
            int Day = Date.Day;
            int Year = Date.Year;

            if (Month < 3)
            {
                Month += 12;
                Year--;
            }

            return Day + (153 * Month - 457) / 5 + 365 * Year + (Year / 4) - (Year / 100) + (Year / 400) + 1721119;
        }

        public static string FormatDateTime(DateTime inputDateTime, string outputDateTimeFormat = "dd/MM/yyyy")
        {
            return string.Format("{0:" + NormalizeDateTimeFormat(outputDateTimeFormat) + "}", inputDateTime);
        }

        public static string FormatDateTime(DateTime inputDateTime, DateTimeFormat outputDateTimeFormat = DateTimeFormat.Date)
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(outputDateTimeFormat) + "}", inputDateTime);
        }

        public static string FormatDateTime(string inputDateTime, string outputDateTimeFormat = "dd/MM/yyyy")
        {
            return string.Format("{0:" + NormalizeDateTimeFormat(outputDateTimeFormat) + "}", DateTime.Parse(inputDateTime));
        }

        public static string FormatDateTime(string inputDateTime, string inputDateTimeFormat = "dd/MM/yyyy", string outputDateTimeFormat = "dd/MM/yyyy")
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            return string.Format("{0:" + NormalizeDateTimeFormat(outputDateTimeFormat) + "}", DateTime.ParseExact(inputDateTime, NormalizeDateTimeFormat(inputDateTimeFormat), CultureInfo.GetCultureInfo("pt-BR")));
        }

        public static string FormatDateTime(string inputDateTime, DateTimeFormat inputDateTimeFormat, DateTimeFormat outputDateTimeFormat)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            return string.Format("{0:" + EnumHelper.GetStringValue(outputDateTimeFormat) + "}", DateTime.ParseExact(inputDateTime, EnumHelper.GetStringValue(inputDateTimeFormat), CultureInfo.GetCultureInfo("pt-BR")));
        }

        public static string FormatDateTime(DateTimeFormat outputDateTimeFormat = DateTimeFormat.DateTime)
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(outputDateTimeFormat) + "}", DateTime.Now);
        }

        public static string FormatDateTime(string outputDateTimeFormat = "dd/MM/yyyy")
        {
            return string.Format("{0:" + NormalizeDateTimeFormat(outputDateTimeFormat) + "}", DateTime.Now);
        }

        public static int GetAge(DateTime dataNascimento)
        {
            DateTime dataAtual = DateTime.Now;

            try
            {
                if (dataNascimento.Date > dataAtual.Date)
                {
                    throw new Exception("A Data de Nascimento não pode ser maior do que hoje");
                }

                int GetAge = dataAtual.Year - dataNascimento.Year;

                if (dataAtual.Month < dataNascimento.Month || (dataAtual.Month == dataNascimento.Month && dataAtual.Day < dataNascimento.Day))
                {
                    --GetAge;
                }

                return GetAge;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna a data de início do horário de verão de um determinado ano
        /// </summary>
        public static DateTime GetBrazilFirstDaylightSavingDay(int anoInicioHorarioVerao)
        {
            // Terceiro domingo de outubro
            DateTime primeiroDeOutubro = new(anoInicioHorarioVerao, 10, 1);

            DateTime primeiroDomingoDeOutubro = primeiroDeOutubro.AddDays((7 - (int)primeiroDeOutubro.DayOfWeek) % 7);

            DateTime terceiroDomingoDeOutubro = primeiroDomingoDeOutubro.AddDays(14);

            return terceiroDomingoDeOutubro;
        }

        /// <summary>
        /// Retorna a data de término do horário de verão de um determinado ano
        /// </summary>
        public static DateTime GetBrazilLastDaylightSavingDay(int anoInicioHorarioVerao)
        {
            DateTime primeiroDeFevereiro = new(anoInicioHorarioVerao + 1, 2, 1);

            DateTime primeiroDomingoDeFevereiro = primeiroDeFevereiro.AddDays((7 - (int)primeiroDeFevereiro.DayOfWeek) % 7);

            DateTime terceiroDomingoDeFevereiro = primeiroDomingoDeFevereiro.AddDays(14);

            if (terceiroDomingoDeFevereiro != HolidayHelper.GetDomingoDeCarnaval(anoInicioHorarioVerao))
            {
                return terceiroDomingoDeFevereiro;
            }
            else
            {
                return terceiroDomingoDeFevereiro.AddDays(7);
            }
        }

        public static string ConvertExtendDateToDate(string inputDate)
        {
            if (inputDate.Length != 10)
            {
                throw new Exception("Data extendida inválida");
            }

            if (!inputDate.Substring(4, 1).Equals("-") && !inputDate.Substring(7, 1).Equals("-"))
            {
                throw new Exception("Data extendida inválida");
            }

            try
            {
                return inputDate.Substring(8, 2) + "/" + inputDate.Substring(5, 2) + "/" + inputDate.Substring(0, 4);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DateTime GetDateTime(int dia, int mes, int ano, int hora = 0, int minuto = 0, int segundo = 0)
        {
            try
            {
                return new DateTime(ano, mes, dia, hora, minuto, segundo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DateTime GetDateTime(string inputDateTime, DateTimeFormat inputDateTimeFormat = DateTimeFormat.DateTimeFormatted)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            return DateTime.ParseExact(inputDateTime, EnumHelper.GetStringValue(inputDateTimeFormat), CultureInfo.GetCultureInfo("pt-BR"));
        }

        public static DateTime GetDateTime(string inputDateTime, string inputDateTimeFormat = "dd/MM/yyyy")
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            return DateTime.ParseExact(inputDateTime, NormalizeDateTimeFormat(inputDateTimeFormat), CultureInfo.GetCultureInfo("pt-BR"));
        }

        public static DateTime GetDateTimeFromInternet()
        {
            // Initialize the list of NIST time servers
            // http://tf.nist.gov/tf-cgi/servers.cgi
            string[] servers = new string[]
            {
                "time.nist.gov",
                "nist1-ny.ustiming.org",
                "nist1-nj.ustiming.org",
                "nist1-pa.ustiming.org",
                "time-a.nist.gov",
                "time-b.nist.gov",
                "nist1.aol-va.symmetricom.com",
                "nist1.columbiacountyga.gov",
                "nist1-chi.ustiming.org",
                "nist.expertsmi.com",
                "nist.netservicesgroup.com"
            };

            // Try 5 servers in random order to spread the load
            //Random rnd = new Random();

            //foreach (string server in servers.OrderBy(s => rnd.NextDouble()).Take(5))
            foreach (string server in servers)
            {
                try
                {
                    // Connect to the server (at port 13) and get the response
                    string serverResponse = string.Empty;

                    using (var reader = new StreamReader(new System.Net.Sockets.TcpClient(server, 13).GetStream()))
                    {
                        serverResponse = reader.ReadToEnd();
                    }

                    // If a response was received
                    if (!string.IsNullOrWhiteSpace(serverResponse))
                    {
                        // Split the response string ("55596 11-02-14 13:54:11 00 0 0 478.1 UTC(NIST) *")
                        string[] tokens = serverResponse.Split(' ');

                        // Check the number of tokens
                        if (tokens.Length >= 6)
                        {
                            // Check the health status
                            string health = tokens[5];

                            if (health == "0")
                            {
                                // Get date and time parts from the server response
                                string[] dateParts = tokens[1].Split('-');
                                string[] timeParts = tokens[2].Split(':');

                                // Create a DateTime instance
                                DateTime utcDateTime = new(Convert.ToInt32(dateParts[0]) + 2000, Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[2]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]), Convert.ToInt32(timeParts[2]));

                                // Convert received (UTC) DateTime value to the local timezone
                                DateTime result = utcDateTime.ToLocalTime();

                                // Response successfully received; exit the loop
                                return result;
                            }
                        }
                    }
                }
                catch
                {
                    // Ignore exception and try the next server
                }
            }

            throw new Exception("Não foi possível selecionar a Data e Hora da Internet, nenhum servidor está disponível");
        }

        public static string GetDayOfWeek(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    return "Domingo";

                case DayOfWeek.Monday:
                    return "Segunda-feira";

                case DayOfWeek.Tuesday:

                    return "Terça-feira";

                case DayOfWeek.Wednesday:
                    return "Quarta-feira";

                case DayOfWeek.Thursday:
                    return "Quinta-feira";

                case DayOfWeek.Friday:
                    return "Sexta-feira";

                case DayOfWeek.Saturday:
                    return "Sábado";
            }

            return string.Empty;
        }

        public static string GetDayOfWeek(int DayOfWeek)
        {
            switch (DayOfWeek)
            {
                case 1:
                    return "Domingo";

                case 2:
                    return "Segunda-feira";

                case 3:
                    return "Terça-feira";

                case 4:
                    return "Quarta-feira";

                case 5:
                    return "Quinta-feira";

                case 6:
                    return "Sexta-feira";

                case 7:
                    return "Sábado";
            }

            return string.Empty;
        }

        public static int GetDaysBetweenTwoDates(DateTime inputStartDateTime, DateTime inputEndDateTime)
        {
            return (int)(inputEndDateTime - inputStartDateTime).TotalDays;
        }

        public static string GetExtendYearToDay()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToDay) + "}", DateTime.Now);
        }

        public static string GetExtendYearToDayFormatted()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToDayFormatted) + "}", DateTime.Now);
        }

        public static string GetExtendYearToMinute()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToMinute) + "}", DateTime.Now);
        }

        public static string GetExtendYearToMinuteFormatted()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToMinuteFormatted) + "}", DateTime.Now);
        }

        public static string GetExtendYearToMonth()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToMonth) + "}", DateTime.Now);
        }

        public static string GetExtendYearToMonthFormatted()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToMonthFormatted) + "}", DateTime.Now);
        }

        public static string GetExtendYearToSecond()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToSecond) + "}", DateTime.Now);
        }

        public static string GetExtendYearToSecondFormatted()
        {
            return string.Format("{0:" + EnumHelper.GetStringValue(DateTimeFormat.ExtendYearToSecondFormatted) + "}", DateTime.Now);
        }

        public static int GetLastDayOfMonth()
        {
            return DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        }

        public static int GetLastDayOfMonth(this DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        }

        public static int GetMonthsBetweenTwoDates(DateTime inputStartDateTime, DateTime inputEndDateTime)
        {
            return DateTimeSpan.CompareDates(inputStartDateTime, inputEndDateTime).Months;
        }

        public static int GetYearsBetweenTwoDates(DateTime inputStartDateTime, DateTime inputEndDateTime)
        {
            return DateTimeSpan.CompareDates(inputStartDateTime, inputEndDateTime).Years;
        }

        public readonly struct DateTimeSpan
        {
            public int Years { get; }
            public int Months { get; }
            public int Days { get; }
            public int Hours { get; }
            public int Minutes { get; }
            public int Seconds { get; }
            public int Milliseconds { get; }

            public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
            {
                Years = years;
                Months = months;
                Days = days;
                Hours = hours;
                Minutes = minutes;
                Seconds = seconds;
                Milliseconds = milliseconds;
            }

            enum Phase { Years, Months, Days, Done }

            public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
            {
                if (date2 < date1)
                {
                    var sub = date1;
                    date1 = date2;
                    date2 = sub;
                }

                DateTime current = date1;
                int years = 0;
                int months = 0;
                int days = 0;

                Phase phase = Phase.Years;
                DateTimeSpan span = new();
                int officialDay = current.Day;

                while (phase != Phase.Done)
                {
                    switch (phase)
                    {
                        case Phase.Years:
                            if (current.AddYears(years + 1) > date2)
                            {
                                phase = Phase.Months;
                                current = current.AddYears(years);
                            }
                            else
                            {
                                years++;
                            }
                            break;
                        case Phase.Months:
                            if (current.AddMonths(months + 1) > date2)
                            {
                                phase = Phase.Days;
                                current = current.AddMonths(months);
                                if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                    current = current.AddDays(officialDay - current.Day);
                            }
                            else
                            {
                                months++;
                            }
                            break;
                        case Phase.Days:
                            if (current.AddDays(days + 1) > date2)
                            {
                                current = current.AddDays(days);
                                var timespan = date2 - current;
                                span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                                phase = Phase.Done;
                            }
                            else
                            {
                                days++;
                            }
                            break;
                    }
                }

                return span;
            }
        }

        public static string GetMonthOfYear(int mes)
        {
            try
            {
                switch (mes)
                {
                    case 1: return "Janeiro";

                    case 2: return "Fevereiro";

                    case 3: return "Março";

                    case 4: return "Abril";

                    case 5: return "Maio";

                    case 6: return "Junho";

                    case 7: return "Julho";

                    case 8: return "Agosto";

                    case 9: return "Setembro";

                    case 10: return "Outubro";

                    case 11: return "Novembro";

                    case 12: return "Dezembro";
                }

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string GetMonthOfYear(DateTime date)
        {
            return GetMonthOfYear(date.Month);
        }

        public static string GetTime()
        {
            return string.Format("{0:HHmmss}", DateTime.Now);
        }

        public static string GetTime(DateTime inputDateTime)
        {
            return string.Format("{0:HHmmss}", inputDateTime);
        }

        public static string GetTimeFormatted()
        {
            return string.Format("{0:HH:mm:ss}", DateTime.Now);
        }

        public static string GetTimeFormatted(DateTime inputDateTime)
        {
            return string.Format("{0:HH:mm:ss}", inputDateTime);
        }

        public static string GetSmallTime()
        {
            return string.Format("{0:HHmm}", DateTime.Now);
        }

        public static string GetSmallTime(DateTime inputDateTime)
        {
            return string.Format("{0:HHmm}", inputDateTime);
        }

        public static string GetSmallTimeFormatted()
        {
            return string.Format("{0:HH:mm}", DateTime.Now);
        }

        public static string GetSmallTimeFormatted(DateTime inputDateTime)
        {
            return string.Format("{0:HH:mm}", inputDateTime);
        }

        public static string GetTimeZoneName()
        {
            return TimeZone.CurrentTimeZone.StandardName;
        }

        public static string GetYearMonth()
        {
            return string.Format("{0:yyyyMM}", DateTime.Now);
        }

        public static string GetYearMonth(DateTime inputDateTime)
        {
            return string.Format("{0:yyyyMM}", inputDateTime);
        }

        public static string GetYear()
        {
            return string.Format("{0:yyyy}", DateTime.Now);
        }

        public static string GetYear(DateTime inputDateTime)
        {
            return string.Format("{0:yyyy}", inputDateTime);
        }

        public static bool IsBiggerThenNow(DateTime inputDateTime)
        {
            if (inputDateTime.Date > DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsBiggerThenNow(string inputDateTime)
        {
            var dateTime = new DateTime();

            if (dateTime.Date > DateTime.Now.Date)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsDate(string inputDateTime, string inputDateTimeFormat = "dd/MM/yyyy")
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

            DateTime parsedDate;

            inputDateTimeFormat = NormalizeDateTimeFormat(inputDateTimeFormat);

            try
            {
                return DateTime.TryParseExact(inputDateTime, inputDateTimeFormat, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out parsedDate);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsNull(DateTime inputDateTime)
        {
            if (inputDateTime == DateTime.MinValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int LastDayOfTheMonth(DateTime inputDateTime)
        {
            DateTime lastDayOfThisMonth = new DateTime(inputDateTime.Year, inputDateTime.Month, 1).AddMonths(1).AddDays(-1);

            return lastDayOfThisMonth.Day;
        }

        public static int LastDayOfTheMonth(int year, int month)
        {
            DateTime date = new(year, month, 1);

            DateTime lastDayOfThisMonth = new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);

            return lastDayOfThisMonth.Day;
        }

        public static DateTime LastDayOfTheMonthAsDateTime(int year, int month)
        {
            return new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
        }

        public static string NormalizeDateTimeFormat(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }
            else if (input.Length == 2 && input == "MM")
            {
                return input;
            }

            return input.ToLower().Replace('a', 'y')
                .Replace('h', 'H')
                .Replace("ddmm", "ddMM")
                .Replace("mmdd", "MMdd")
                .Replace("/mm", "/MM")
                .Replace("mm/", "MM/")
                .Replace("-mm", "-MM")
                .Replace("mm-", "MM-")
                .Replace(".mm", ".MM")
                .Replace("mm.", "MM.")
                .Replace("yymm", "yyMM")
                .Replace("mmyy", "MMyy");
        }

        public static string PeriodAddDays(string period, int days)
        {
            int dia = 1;
            int mes = Convert.ToInt16(period.Substring(0, 4));
            int ano = Convert.ToInt16(period.Substring(5, 2));

            DateTime date = new(ano, mes, dia);

            date.AddDays(days);

            return string.Format("{0:yyyyMM}", date);
        }

        public static string PeriodAddMonths(string period, int months)
        {
            int dia = 1;
            int mes = Convert.ToInt16(period.Substring(0, 4));
            int ano = Convert.ToInt16(period.Substring(5, 2));

            DateTime date = new(ano, mes, dia);

            date.AddMonths(months);

            return string.Format("{0:yyyyMM}", date);
        }

        public static string PeriodAddYears(string period, int months)
        {
            int dia = 1;
            int mes = Convert.ToInt16(period.Substring(0, 4));
            int ano = Convert.ToInt16(period.Substring(5, 2));

            DateTime date = new(ano, mes, dia);

            date.AddYears(months);

            return string.Format("{0:yyyyMM}", date);
        }

        public static DateTime SetTime(this DateTime dateTime, int hour)
        {
            if (hour == 24)
            {
                hour = 0;
            }

            if (hour > 23)
            {
                hour = 23;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, dateTime.Minute, dateTime.Second);
        }

        public static DateTime SetTime(this DateTime dateTime, int hour, int minute)
        {
            if (hour == 24)
            {
                hour = 0;
            }

            if (hour > 23)
            {
                hour = 23;
            }

            if (minute > 59)
            {
                minute = 59;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, dateTime.Second);
        }

        public static DateTime SetTime(this DateTime dateTime, int hour, int minute, int second)
        {
            if (hour == 24)
            {
                hour = 0;
            }

            if (hour > 23)
            {
                hour = 23;
            }

            if (minute > 59)
            {
                minute = 59;
            }

            if (second > 59)
            {
                second = 59;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, second);
        }

        public static DateTime SetYear(this DateTime dateTime, int year)
        {
            if (year < 1)
            {
                year = 1;
            }

            return new DateTime(year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static DateTime SetMonth(this DateTime dateTime, int month)
        {
            if (month < 1)
            {
                month = 1;
            }
            else if (month > 12)
            {
                month = 12;
            }

            return new DateTime(dateTime.Year, month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static DateTime SetDay(this DateTime dateTime, int day)
        {
            int aux = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            if (day < 1)
            {
                day = 1;
            }
            else if (day > aux)
            {
                day = aux;
            }

            return new DateTime(dateTime.Year, dateTime.Month, day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static DateTime SetHour(this DateTime dateTime, int hour)
        {
            if (hour < 0)
            {
                hour = 0;
            }
            else if (hour > 23)
            {
                hour = 23;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, dateTime.Minute, dateTime.Second);
        }

        public static DateTime SetMinute(this DateTime dateTime, int minute)
        {
            if (minute < 0)
            {
                minute = 0;
            }
            else if (minute > 59)
            {
                minute = 59;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, minute, dateTime.Second);
        }

        public static DateTime SetSecond(this DateTime dateTime, int second)
        {
            if (second < 0)
            {
                second = 0;
            }
            else if (second > 59)
            {
                second = 59;
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, second);
        }
    }
}
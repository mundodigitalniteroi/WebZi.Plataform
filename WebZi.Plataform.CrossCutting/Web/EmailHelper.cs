using System.Net.Mail;

namespace WebZi.Plataform.CrossCutting.Web
{
    public static class EmailHelper
    {
        public static bool IsEmail(this string email)
        {
            email = email.Trim();

            if (email.EndsWith("."))
            {
                return false;
            }

            try
            {
                return new MailAddress(email).Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
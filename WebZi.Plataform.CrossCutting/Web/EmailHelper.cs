using System.Net.Mail;

namespace WebZi.Plataform.CrossCutting.Web
{
    public abstract class EmailHelper
    {
        public static bool IsEmail(string email)
        {
            string trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }

            try
            {
                return new MailAddress(email).Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }
    }
}
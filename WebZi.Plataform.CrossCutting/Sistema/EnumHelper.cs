using System.Reflection;

namespace WebZi.Plataform.CrossCutting.Sistema
{
    public abstract class EnumHelper
    {
        public class StringValue : Attribute
        {
            private string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }

            //Example:

            //public enum AuthenticationMethod
            //{
            //    [EnumClass.StringValue("FORMS")] FORMS = 1,
            //    [EnumClass.StringValue("WINDOWS")] WINDOWSAUTHENTICATION = 2,
            //    [EnumClass.StringValue("SSO")] SINGLESIGNON = 3
            //}
        }

        public static string GetStringValue(Enum value)
        {
            string output = null;

            Type type = value.GetType();

            FieldInfo fieldInfo = type.GetField(value.ToString());

            StringValue[] stringValue = fieldInfo.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (stringValue.Length > 0)
            {
                output = stringValue[0].Value;
            }

            return output;
        }
    }
}
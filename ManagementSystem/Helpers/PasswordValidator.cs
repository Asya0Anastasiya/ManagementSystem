using System.Text.RegularExpressions;
using System.Text;

namespace ManagementSystem.Helpers
{
    public class PasswordValidator
    {
        public static string CheckPasswordStrength(string password)
        {
            // массив строк
            StringBuilder sb = new();
            if (password.Length < 8)
                sb.Append("Password length should be more than 8" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                sb.Append("Password should be Alphanumeric" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[!,@,#,$,%,^,&,*,(,),_,=,+,{,}]"))
                sb.Append("Password should contain special chars" + Environment.NewLine);
            return sb.ToString();
        }
    }
}

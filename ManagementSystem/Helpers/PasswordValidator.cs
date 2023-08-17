using System.Text.RegularExpressions;

namespace ManagementSystem.Helpers
{
    public class PasswordValidator
    {
        public static List<string> CheckPasswordStrength(string password)
        {
            List<string> result = new();
            if (password.Length < 8)
                result.Add("Password length should be more than 8" + Environment.NewLine);
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
                result.Add("Password should be Alphanumeric" + Environment.NewLine);
            if (!Regex.IsMatch(password, "[!,@,#,$,%,^,&,*,(,),_,=,+,{,}]"))
                result.Add("Password should contain special chars" + Environment.NewLine);
            return result;
        }
    }
}

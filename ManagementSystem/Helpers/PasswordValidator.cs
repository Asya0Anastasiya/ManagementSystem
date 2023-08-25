using System.Text.RegularExpressions;
using UserServiceAPI.Exceptions;

namespace UserServiceAPI.Helpers
{
    public class PasswordValidator
    {
        public static void CheckPasswordStrength(string password)
        {
            if (password.Length < 8)
                throw new InternalException("Password should contains of 8 chars at least");
            if (!(Regex.IsMatch(password, "[a-z]")
                && Regex.IsMatch(password, "[A-Z]")
                && Regex.IsMatch(password, "[0-9]")
                && Regex.IsMatch(password, "[!,@,#,$,%,^,&,*,(,),_,=,+,{,}]")))
                throw new InternalException("Password should be Alphanumeric with special chars");
        }
    }
}

using FluentValidation;
using System.Text.RegularExpressions;

namespace UserService.Helpers
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> PasswordValidator<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MinimumLength(8)
                .MaximumLength(25)
                .Must(val => Regex.IsMatch(val, "[a-z, A-Z, 0-9, !,@,#,$,%,^,&,*,(,),_,=,+,{,}]"))
                .WithMessage("Password must contains of 8 chars at least and be Alphanumeric with special chars");
        }
    }
}

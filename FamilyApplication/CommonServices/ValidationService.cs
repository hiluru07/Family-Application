using FamilyApplication.DTOs;
using FluentValidation;

namespace FamilyApplication.CommonServices
{
    public class PasswordValidation : AbstractValidator<RegisterDTO>
    {
        public PasswordValidation() 
        {
            RuleFor(x => x.Password)
                  .NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password)
                .Matches(@"[A-Z]").WithMessage("Password atleast one caps")
                .Matches(@"[a-z]").WithMessage("Password atleast one small")
                .Matches(@"[0-9]").WithMessage("Password atleast one number")
                .Matches(@"[!@$&*]").WithMessage("Password atleast one special char");

            RuleFor(x => x.Cpassword)
                .Equal(x=>x.Password)
                .WithMessage("must both password match");
        }
    }
}

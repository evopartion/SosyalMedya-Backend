using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserOperationValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().NotNull();
            RuleFor(x => x.OperationClaimId).NotEmpty().NotNull();
        }
    }
}

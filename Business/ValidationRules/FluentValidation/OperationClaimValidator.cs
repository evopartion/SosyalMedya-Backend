using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Lütfen yetki adı giriniz");
            RuleFor(x => x.Name).NotNull().WithMessage("Lütfen yetki adı giriniz");
        }
    }
}

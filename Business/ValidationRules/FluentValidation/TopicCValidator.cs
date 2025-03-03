using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class TopicCValidator : AbstractValidator<Topic>
    {
        public TopicCValidator()
        {
            RuleFor(x => x.TopicTitle).NotEmpty();
            RuleFor(x => x.TopicTitle).NotNull();
        }
    }
}

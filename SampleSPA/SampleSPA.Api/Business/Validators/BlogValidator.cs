using FluentValidation;
using SampleSPA.Api.Contracts;
using SampleSPA.Api.Resources;

namespace SampleSPA.Api.Business.Validators
{
    public class BlogValidator : AbstractValidator<BlogRequest>
    {
        public BlogValidator()
        {
            RuleFor(x => x.Url).NotEmpty().WithMessage(WebStrings.BlogRequired);
        }
    }
}

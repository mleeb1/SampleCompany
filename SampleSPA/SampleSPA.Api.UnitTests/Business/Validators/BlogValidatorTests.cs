using FluentValidation.TestHelper;
using SampleSPA.Api.Business.Validators;
using SampleSPA.Api.Resources;
using Xunit;

namespace SampleSPA.Api.UnitTests.Business.Validators
{
    public class BlogValidatorTests
    {
        private readonly BlogValidator _validator;

        public BlogValidatorTests()
        {
            _validator = new BlogValidator();
        }

        [Fact]
        public void Validate_UrlNull_HasError()
        {
            var result = _validator.ShouldHaveValidationErrorFor(b => b.Url, null as string);
            result.WithErrorMessage(WebStrings.BlogRequired);
        }

        [Fact]
        public void Validate_WithValidUrl_HasNoError()
        {
            _validator.ShouldNotHaveValidationErrorFor(b => b.Url, "http://www.company/.com");
        }
    }
}

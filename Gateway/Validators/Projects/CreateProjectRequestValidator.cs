using FluentValidation;
using Requests;

namespace Validators
{
    class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequestDto>
    {
        public CreateProjectRequestValidator()
        {
            RuleFor(x => x.Name).ValidateProjectName();
            RuleFor(x => x.Description).ValidateDescription();
        }
    }
}

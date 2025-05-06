using FluentValidation;
using Requests;

namespace Validators
{
    class UpdateProjectRequestValidator : AbstractValidator<UpdateProjectRequestDto>
    {
        public UpdateProjectRequestValidator()
        {
            RuleFor(x => x.Name).ValidateProjectName();
            RuleFor(x => x.Description).ValidateDescription();
        }
    }
}

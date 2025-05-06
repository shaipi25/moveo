using FluentValidation;
using Requests;

namespace Validators
{
    class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequestDto>
    {
        public CreateTaskRequestValidator()
        {
            RuleFor(x => x.Name).ValidateTaskName();
            RuleFor(x => x.Description).ValidateDescription();
        }
    }
}

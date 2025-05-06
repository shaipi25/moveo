using FluentValidation;
using Requests;

namespace Validators
{
    class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequestDto>
    {
        public UpdateTaskRequestValidator()
        {
            RuleFor(x => x.Name).ValidateTaskName();
            RuleFor(x => x.Description).ValidateDescription();
        }
    }
}

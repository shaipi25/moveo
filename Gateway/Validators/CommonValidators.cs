using FluentValidation;

namespace Validators
{
    static class CommonValidators
    {
        const string InvalidCharsRegeularExpression = "^[a-zA-Z0-9-_]+$";

        public static IRuleBuilderOptions<T, string> ValidateProjectName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Project name is required.")
                .MaximumLength(100).WithMessage("Project name can't exceed 100 characters.")
                .Matches(InvalidCharsRegeularExpression).WithMessage("Project name contains invalid characters.");
        }
        
        public static IRuleBuilderOptions<T, string> ValidateTaskName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Task name is required.")
                .MaximumLength(100).WithMessage("Task name can't exceed 100 characters.")
                .Matches(InvalidCharsRegeularExpression).WithMessage("Task name contains invalid characters.");
        }

        public static IRuleBuilderOptions<T, string> ValidateDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(500).WithMessage("Description can't exceed 500 characters.");
        }
    }
}

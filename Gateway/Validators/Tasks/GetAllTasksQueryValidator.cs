using FluentValidation;
using Gateway.Model.Queries;

namespace Validators
{
    class GetAllTasksQueryValidator : AbstractValidator<GetAllTasksQuery>
    {
        public GetAllTasksQueryValidator()
        {
            RuleFor(x => !x.PageNumber.HasValue || x.PageNumber.Value >= 1);
            RuleFor(x => !x.PageSize.HasValue || x.PageSize.Value >= 1);
        }
    }
}


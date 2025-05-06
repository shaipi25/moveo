using FluentValidation;
using Gateway.Model.Queries;

namespace Validators
{
    class GetAllProjectsQueryValidator : AbstractValidator<GetAllProjectsQuery>
    {
        public GetAllProjectsQueryValidator()
        {
            RuleFor(x => !x.PageNumber.HasValue ||  x.PageNumber.Value >= 1);
            RuleFor(x => !x.PageSize.HasValue ||  x.PageSize.Value >= 1);
        }
    }
}

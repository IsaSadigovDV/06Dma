using Api006.App.Dtos;
using FluentValidation;

namespace Api006.App.Validations.Category
{
    public class CategoryPostDtoValidation:AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(c => c.Name)
                .NotNull().WithMessage("Item can not be null")
                .NotEmpty().WithMessage("Item can not be empty")
                .MaximumLength(30)
                .MinimumLength(3);
        }
    }
}

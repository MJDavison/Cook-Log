using FluentValidation;

namespace Cook_Log.Application.MealItems.Commands.UpdateMealItem
{
    public class UpdateMealItemCommandValidator : AbstractValidator<UpdateMealItemCommand>
    {
        public UpdateMealItemCommandValidator(){
            RuleFor(v=>v.Title)
            .MaximumLength(200)
            .NotEmpty();
        }
    }
}

using System;
using FluentValidation;

namespace Cook_Log.Application.MealItems.Commands.CreateMealItem
{
    public class CreateMealItemCommandValidator : AbstractValidator<CreateMealItemCommand>
    {
        public CreateMealItemCommandValidator(){
            RuleFor(v=>v.Title)
            .MaximumLength(200)
            .NotEmpty();
        }
    }
}

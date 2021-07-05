using System;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Cook_Log.Application.MealCategories.Commands.CreateMealCategory
{
    public class CreateMealCategoryCommandValidator : AbstractValidator<CreateMealCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateMealCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v=>v.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title already exists");
        }

        private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return await _context.MealCategories.AllAsync(l=>l.Title != title);
        }
    }
}

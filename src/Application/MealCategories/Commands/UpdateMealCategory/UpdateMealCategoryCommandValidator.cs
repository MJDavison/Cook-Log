using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Cook_Log.Application.MealCategories.Commands.UpdateMealCategory
{
    public class UpdateMealCategoryCommandValidator : AbstractValidator<UpdateMealCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMealCategoryCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v=>v.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters")
            .MustAsync(BeUniqueTitle).WithMessage("The specified title is taken");
        }

        private async Task<bool> BeUniqueTitle(UpdateMealCategoryCommand model,string title, CancellationToken cancellationToken)
        {
            return await _context.MealCategories
            .Where(l=>l.Id != model.Id)
            .AllAsync(l=>l.Title != title);
        }
    }
}

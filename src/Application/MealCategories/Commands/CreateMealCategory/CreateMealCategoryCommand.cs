using System;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Interfaces;
using Cook_Log.Domain.Entities;
using MediatR;

namespace Cook_Log.Application.MealCategories.Commands.CreateMealCategory
{
    public partial class CreateMealCategoryCommand : IRequest<int>
    {
        public string Title{ get; set; }    
    }

    public class CreateMealCategoryCommandHandler : IRequestHandler<CreateMealCategoryCommand, int>{
        private readonly IApplicationDbContext _context;

        public CreateMealCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMealCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new MealCategory();

            entity.Title = request.Title;

            _context.MealCategories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
            
        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.Common.Interfaces;
using Cook_Log.Domain.Entities;
using MediatR;

namespace Cook_Log.Application.MealCategories.Commands.UpdateMealCategory
{
    public class UpdateMealCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }

    public class UpdateMealCategoryCommandHandler : IRequestHandler<UpdateMealCategoryCommand>{
        private readonly IApplicationDbContext _context;

        public UpdateMealCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMealCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MealCategories.FindAsync(request.Id);

            if(entity == null){
                throw new NotFoundException(nameof(MealCategory), request.Id);
            }

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;        
        }
    }
}

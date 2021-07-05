using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.Common.Interfaces;
using Cook_Log.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MealCategories.Commands.DeleteMealCategory
{
    public class DeleteMealCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteMealCategoryCommandHandler : IRequestHandler<DeleteMealCategoryCommand>{
        private readonly IApplicationDbContext _context;

        public DeleteMealCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMealCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MealCategories
            .Where(l=>l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

            if(entity == null){
                throw new NotFoundException(nameof(MealCategory), request.Id);
            }

            _context.MealCategories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

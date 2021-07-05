using System;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.Common.Interfaces;
using Cook_Log.Domain.Entities;
using MediatR;

namespace Cook_Log.Application.MealItems.Commands.UpdateMealItemDetail
{
    public class UpdateMealItemDetailCommand : IRequest
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }        
    }

    public class UpdateMealItemDetailCommandHandler : IRequestHandler<UpdateMealItemDetailCommand>
    {

        private readonly IApplicationDbContext _context;

        public UpdateMealItemDetailCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMealItemDetailCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.MealItems.FindAsync(request.Id);

            if(entity == null){
                throw new NotFoundException(nameof(MealItem), request.Id);
            }

            entity.CategoryId = request.CategoryId;
            
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
    
}

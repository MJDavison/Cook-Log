using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Cook_Log.Domain.Entities;

namespace Cook_Log.Application.MealItems.Commands.DeleteMealItem
{
    public class DeleteMealItemCommand : IRequest
    {
        public int Id {get; set;}
    }

    public class DeleteMealItemCommandHandler : IRequestHandler<DeleteMealItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteMealItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteMealItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            if(entity == null){
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }         
            _context.TodoItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

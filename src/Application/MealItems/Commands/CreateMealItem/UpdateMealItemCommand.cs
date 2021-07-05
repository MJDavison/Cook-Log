using System;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.Common.Interfaces;
using Cook_Log.Domain.Entities;
using MediatR;

namespace Application.MealItems.Commands.CreateMealItem
{
    public partial class UpdateMealItemCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Done { get; set; }
    }

    public class UpdateMealItemCommandHandler : IRequestHandler<UpdateMealItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateMealItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateMealItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(MealItem), request.Id);
            }

            entity.Title = request.Title;
            entity.Done = request.Done;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

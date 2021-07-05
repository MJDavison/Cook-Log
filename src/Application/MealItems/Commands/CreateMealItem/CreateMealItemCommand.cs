using System;
using System.Threading;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Interfaces;
using Cook_Log.Domain.Entities;

using MediatR;

namespace Cook_Log.Application.MealItems.Commands.CreateMealItem
{
    public class CreateMealItemCommand: IRequest<int>
    {
        public int CategoryId{get; set;}
        public string Title { get; set; }
    }

    public class CreateMealItemCommandHandler : IRequestHandler<CreateMealItemCommand, int>{
        private readonly IApplicationDbContext _context;

        public CreateMealItemCommandHandler(IApplicationDbContext context){
            _context = context;
        }

        public async Task<int> Handle(CreateMealItemCommand request, CancellationToken cancellationToken){
            var entity = new MealItem{
                CategoryId = request.CategoryId,
                Title = request.Title,                
            };

            _context.MealItems.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}

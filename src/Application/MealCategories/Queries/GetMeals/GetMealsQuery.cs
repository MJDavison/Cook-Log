using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cook_Log.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cook_Log.Application.MealCategories.Queries.GetMeals
{
    public class GetMealsQuery : IRequest<MealsVm>
    {
    }

    public class GetMealsQueryHandler : IRequestHandler<GetMealsQuery, MealsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetMealsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MealsVm> Handle(GetMealsQuery request, CancellationToken cancellationToken)
        {
            return new MealsVm{
                Categories = await _context.MealCategories
                .ProjectTo<MealCategoryDto>(_mapper.ConfigurationProvider)
                .OrderBy(t=>t.Title)
                .ToListAsync(cancellationToken)
            };
        }
    }
}

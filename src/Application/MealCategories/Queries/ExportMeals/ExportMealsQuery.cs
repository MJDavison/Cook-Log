using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cook_Log.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cook_Log.Application.MealCategories.Queries.ExportMeals
{
    public class ExportMealsQuery : IRequest<ExportMealsVm>
    {
        public int CategoryId { get; set; }
    }

    public class ExportMealsQueryHandler : IRequestHandler<ExportMealsQuery, ExportMealsVm>{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder _fileBuilder;

        public ExportMealsQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportMealsVm> Handle(ExportMealsQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportMealsVm();

            var records = await _context.MealItems
                .Where(t=>t.CategoryId == request.CategoryId)
                .ProjectTo<MealItemFileRecord>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);              

            vm.Content = _fileBuilder.BuildMealItemsFile(records);
            vm.ContentType = "text/csv";
            vm.FileName = "MealItems.csv";

            return await Task.FromResult(vm);

        }
    }
}

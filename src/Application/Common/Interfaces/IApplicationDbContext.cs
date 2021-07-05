using Cook_Log.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cook_Log.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<MealCategory> MealCategories {get; set;}

        DbSet<MealItem> MealItems {get; set;}

        DbSet<IngredientItem> IngredientLists {get; set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

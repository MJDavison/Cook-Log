using System;
using Cook_Log.Application.Common.Mappings;
using Cook_Log.Domain.Entities;

namespace Cook_Log.Application.MealCategories.Queries.ExportMeals
{
    public class MealItemFileRecord : IMapFrom<MealItem>
    {
        public string Title { get; set; }        
    }
}

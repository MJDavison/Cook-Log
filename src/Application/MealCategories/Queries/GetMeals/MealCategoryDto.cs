using System;
using System.Collections.Generic;
using Cook_Log.Application.Common.Mappings;
using Cook_Log.Domain.Entities;

namespace Cook_Log.Application.MealCategories.Queries.GetMeals
{
    public class MealCategoryDto : IMapFrom<MealCategory>
    {
        public MealCategoryDto()
        {            
            Items = new List<MealItemDto>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public IList<MealItemDto> Items { get; set; }
    }
}

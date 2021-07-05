using System;
using System.Collections.Generic;

namespace Cook_Log.Application.MealCategories.Queries.GetMeals
{
    public class MealsVm
    {
        public IList<MealCategoryDto> Categories { get; set; }
    }
}

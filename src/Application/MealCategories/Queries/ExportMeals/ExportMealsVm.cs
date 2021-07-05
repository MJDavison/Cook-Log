using System;

namespace Cook_Log.Application.MealCategories.Queries.ExportMeals
{
    public class ExportMealsVm
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
    }
}

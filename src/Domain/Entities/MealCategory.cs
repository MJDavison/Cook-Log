using System;
using System.Collections.Generic;

namespace Cook_Log.Domain.Entities
{
    public class MealCategory
    {
        public MealCategory(){
            Items = new List<MealItem>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Colour { get; set; }
        public IList<MealItem> Items { get; set; }
    }
}

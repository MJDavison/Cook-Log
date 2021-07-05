using System;
using System.Collections.Generic;
using Domain.Common;

namespace Cook_Log.Domain.Entities
{
    public class MealItem : MyAuditableEntity
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public MealCategory Category { get; set; }
        public IList<IngredientItem> Ingredients { get; set; }

    }
}

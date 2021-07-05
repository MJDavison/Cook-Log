using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Cook_Log.Domain.Entities
{
    public class IngredientItem
    {
        public int Id { get; set; }
        public int MealId { get; set; }
        public string Title { get; set; }
        public IList<InstructionItem> Instructions { get; set; }
    }
}

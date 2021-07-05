using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Cook_Log.Application.Common.Mappings;
using Cook_Log.Domain.Entities;

namespace Cook_Log.Application.MealCategories.Queries.GetMeals
{
    public class MealItemDto : IMapFrom<MealItem>
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public IList<IngredientItem> Ingrediants { get; set; }

        public void Mapping(Profile profile){
            profile.CreateMap<MealItem, MealItemDto>();    
        }
    }
}

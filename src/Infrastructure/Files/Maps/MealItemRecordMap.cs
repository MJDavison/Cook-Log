using System.Globalization;
using Cook_Log.Application.MealCategories.Queries.ExportMeals;
using CsvHelper.Configuration;

namespace Cook_Log.Infrastructure.Files.Maps
{
    public class MealItemRecordMap : ClassMap<MealItemFileRecord>{
        public MealItemRecordMap(){
            AutoMap(CultureInfo.InvariantCulture);            
        }
    }
    
}

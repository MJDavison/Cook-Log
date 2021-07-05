using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Cook_Log.Application.MealCategories.Commands.CreateMealCategory;
using Cook_Log.Application.MealItems.Commands.CreateMealItem;
using Cook_Log.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Cook_Log.Application.IntegrationTests.MealItems.Commands
{
    using static Testing;
    public class CreateMealItemTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields(){
            var command = new CreateMealItemCommand();

            FluentActions.Invoking(()=>
                SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateMealItem(){
            var userId = await RunAsDefaultUserAsync();

            var categoryId = await SendAsync(new CreateMealCategoryCommand{
                Title = "New List"
            });

            var command = new CreateMealItemCommand{
                CategoryId = categoryId,
                Title ="Tasks"
            };

            var itemId = await SendAsync(command);
            var item = await FindAsync<MealItem>(itemId);

            item.Should().NotBeNull();
            item.CategoryId.Should().Be(command.CategoryId);
            item.Title.Should().Be(command.Title);
            item.Created.Should().BeCloseTo(DateTime.Now, 10000);
            item.LastModified.Should().BeNull();
            
        }
    }
}

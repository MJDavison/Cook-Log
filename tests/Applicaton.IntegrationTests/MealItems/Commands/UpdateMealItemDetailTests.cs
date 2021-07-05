using System;
using System.Threading.Tasks;
using Application.MealItems.Commands.CreateMealItem;
using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.IntegrationTests;
using Cook_Log.Application.MealCategories.Commands.CreateMealCategory;
using Cook_Log.Application.MealItems.Commands.CreateMealItem;
using Cook_Log.Application.MealItems.Commands.UpdateMealItemDetail;
using Cook_Log.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.MealItems.Commands
{
    using static Testing;

    public class UpdateMealItemDetailTests : TestBase
    {
        [Test]
        public void ShouldRequireValidTodoItemId()
        {
            var command = new UpdateMealItemCommand
            {
                
                Id = 99,
                Title = "New Title"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldUpdateMealItem()
        {
            var userId = await RunAsDefaultUserAsync();

            var categoryId = await SendAsync(new CreateMealCategoryCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateMealItemCommand
            {
                CategoryId = categoryId,
                Title = "New Item"
            });

            var command = new UpdateMealItemDetailCommand
            {
                Id = itemId,
                CategoryId = categoryId,                                
            };

            await SendAsync(command);

            var item = await FindAsync<MealItem>(itemId);

            item.CategoryId.Should().Be(command.CategoryId);            
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}

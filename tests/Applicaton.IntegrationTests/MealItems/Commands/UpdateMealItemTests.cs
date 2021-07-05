using System;
using System.Threading.Tasks;
using Application.MealItems.Commands.CreateMealItem;
using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.IntegrationTests;
using Cook_Log.Application.MealCategories.Commands.CreateMealCategory;
using Cook_Log.Application.MealItems.Commands.CreateMealItem;
using Cook_Log.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.MealItems.Commands
{
    using static Testing;

    public class UpdateMealItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidMealItemId()
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

            var command = new UpdateMealItemCommand
            {
                Id = itemId,
                Title = "Updated Item Title"
            };

            await SendAsync(command);

            var item = await FindAsync<MealItem>(itemId);

            item.Title.Should().Be(command.Title);            
            item.LastModified.Should().NotBeNull();
            item.LastModified.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}

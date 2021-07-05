using System;
using System.Threading.Tasks;
using Cook_Log.Application.Common.Exceptions;
using Cook_Log.Application.IntegrationTests;
using Cook_Log.Application.MealCategories.Commands.CreateMealCategory;
using Cook_Log.Application.MealItems.Commands.CreateMealItem;
using Cook_Log.Application.MealItems.Commands.DeleteMealItem;
using Cook_Log.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.MealItems.Commands
{
    using static Testing;
    public class DeleteMealItemTests : TestBase
    {
        [Test]
        public void ShouldRequireValidMealItemId()
        {
            var command = new DeleteMealItemCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteMealItem()
        {
            var categoryId = await SendAsync(new CreateMealCategoryCommand
            {
                Title = "New List"
            });

            var itemId = await SendAsync(new CreateMealItemCommand
            {
                CategoryId = categoryId,
                Title = "New Item"
            });

            await SendAsync(new DeleteMealItemCommand
            {
                Id = itemId
            });

            var category = await FindAsync<MealItem>(categoryId);

            category.Should().BeNull();
        }
    }
}

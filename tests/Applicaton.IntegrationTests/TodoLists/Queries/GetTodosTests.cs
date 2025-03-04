﻿using Cook_Log.Application.TodoLists.Queries.GetTodos;
using Cook_Log.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Cook_Log.Application.IntegrationTests.TodoLists.Queries
{
    using static Testing;

    public class GetTodosTests : TestBase
    {
        [Test]
        public async Task ShouldReturnPriorityLevels()
        {
            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.PriorityLevels.Should().NotBeEmpty();
        }

        [Test]
        public async Task ShouldReturnAllListsAndItems()
        {
            await AddAsync(new TodoList
            {
                Title = "Shopping",
                Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" }
                    }
            });

            var query = new GetTodosQuery();

            var result = await SendAsync(query);

            result.Lists.Should().HaveCount(1);
            result.Lists.First().Items.Should().HaveCount(7);
        }
    }
}

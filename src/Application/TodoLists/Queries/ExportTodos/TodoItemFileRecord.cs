using Cook_Log.Application.Common.Mappings;
using Cook_Log.Domain.Entities;

namespace Cook_Log.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}

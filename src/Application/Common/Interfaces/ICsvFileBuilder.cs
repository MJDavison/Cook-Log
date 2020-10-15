﻿using Cook_Log.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace Cook_Log.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.BusinessLogic.Logic.TestLogic
{
    public interface ITodoRepository
    {
        void Add(TodoItem item);
        IEnumerable<TodoItem> GetAll();
        TodoItem Find(long key);
        void Remove(long key);
        void Update(TodoItem item);
    }
}

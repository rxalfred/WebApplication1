using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.BusinessLogic.Logic.TestLogic;

namespace CrossCutting.Test
{
    public class TestLogic
    {
        private readonly ITodoRepository _todoRepo;
        private readonly ITodoContext _todoContext;
        public DbContextOptions<TodoContext> _options { get; set; }

        public TestLogic(ITodoContext todoContext, ITodoRepository todoRepo)
        {
            _todoContext = todoContext;
            _todoRepo = todoRepo;
        }

        public void TestLogging()
        {
            _todoRepo.Add(new TodoItem());
            //_options = new DbContextOptionsBuilder<TodoContext>()
            //    .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
            //    .Options;
            // Run the test against one instance of the context
            //using (var context = _todoContext)
            //{
            //    var service = _todoRepo;
            //    service.Add(new TodoItem() { });
            //    context.SaveChanges();
            //}
        }
    }
}

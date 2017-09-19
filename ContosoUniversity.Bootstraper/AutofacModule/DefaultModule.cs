using System;
using System.Collections.Generic;
using Autofac;
using System.Text;
using ContosoUniversity.BusinessLogic.Logic.TestLogic;
using ContosoUniversity.BusinessLogic.Logic;

namespace ContosoUniversity.Bootstraper.AutofacModule
{
    public class DefaultModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TodoRepository>().As<ITodoRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserLogic>().As<IUserLogic>().InstancePerLifetimeScope();
            //builder.RegisterType<StudentLogic>().As<IStudentLogic>().InstancePerLifetimeScope();
        }
    }
}
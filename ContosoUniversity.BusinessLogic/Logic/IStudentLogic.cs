﻿using ContosoUniversity.Model.CoreTestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.BusinessLogic.Logic
{
    public interface IStudentLogic
    {
        Task TestStudent();

        Task<Student> TestStudentWithEnrollment();
    }
}

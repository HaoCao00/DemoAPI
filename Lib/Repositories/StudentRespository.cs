﻿using Lib.Data;
using Lib.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        List<Student> GetStudents();
    }
    public class StudentRespository : RepositoryBase<Student>, IStudentRepository
    {
        public StudentRespository(DbContextFactory factory)
            : base(factory)
        {

        }
        public List<Student> GetStudents()
        {
            var query = dataContext.Student.AsQueryable();
            return query.ToList();
        }
    }
}

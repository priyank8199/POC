using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;
        private readonly ILogger<SqlStudentRepository> _logger;

        public SqlStudentRepository(StudentDbContext context,
                                    ILogger<SqlStudentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public Student Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public Student Delete(Student student)
        {
            
                _context.Students.Remove(student);
                _context.SaveChanges();
            
            return student;
        }

       /* public object Delete(Task user)
        {
            Student student = _context.Students.Find(user);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
            return student;

        }*/

       /* public Task Delete(string id)
        {
            throw new NotImplementedException();
        }*/

        public IEnumerable<Student> GetAllStudent()
        {
            return _context.Students;
        }

        public Student GetStudent(int Id)
        {
            var obj = _context.Students.Find(Id);
            return obj;
        }

        public Student Update(Student studentupdate)
        {
            var student = _context.Students.Attach(studentupdate);
            student.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return studentupdate;
        }
    }
}

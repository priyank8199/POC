using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagement.Models
{
    public interface IStudentRepository
    {
        Student GetStudent(int Id);
        IEnumerable<Student> GetAllStudent();
        Student Add(Student student);
        Student Update(Student studentupdate);
        Student Delete(Student student);
       /* Task Delete(string id);
        object Delete(Task user);*/
    }
}

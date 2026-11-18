using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>
        {
            new Student { Id = 1, Name = "Rahul", Email = "rahul@gmail.com", Course = "Computer Science", Age = 21 },
            new Student { Id = 2, Name = "Priya", Email = "priya@gmail.com", Course = "Information Technology", Age = 22 }
        };

        // GET: api/student
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(students);
        }

        // GET: api/student/1
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/student
        [HttpPost]
        public ActionResult<Student> AddStudent(Student student)
        {
            student.Id = students.Count == 0 ? 1 : students.Max(s => s.Id) + 1;
            students.Add(student);

            return CreatedAtAction(nameof(GetStudent),
                new { id = student.Id }, student);
        }

        // PUT: api/student/1
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, Student updatedStudent)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            student.Name = updatedStudent.Name;
            student.Email = updatedStudent.Email;
            student.Course = updatedStudent.Course;
            student.Age = updatedStudent.Age;

            return NoContent();
        }

        // DELETE: api/student/1
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = students.FirstOrDefault(s => s.Id == id);

            if (student == null)
                return NotFound();

            students.Remove(student);

            return NoContent();
        }
    }
}

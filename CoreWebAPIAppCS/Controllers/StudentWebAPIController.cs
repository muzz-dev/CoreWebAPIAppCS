using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoreWebAPIAppCS.Models;

namespace CoreWebAPIAppCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentWebAPIController : ControllerBase
    {
        private readonly DBCollegeContext _context;

        public StudentWebAPIController(DBCollegeContext context)
        {
            _context = context;
        }

        //// GET: api/StudentWebAPI
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        //{
        //    return await _context.Student.ToListAsync();
        //}

        // GET: api/StudentWebAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> GetStudent(int? courseId)
        {
            List<StudentCourse> students = null;
            if (courseId != null)
            {
                students = await (from s in _context.Student
                                  join c in _context.Course
                                  on s.CourseId equals c.CourseId
                                  where s.CourseId == courseId
                                  select new StudentCourse
                                  {
                                      StudentId = s.StudentId,
                                      StudentName = s.StudentName,
                                      Gender = s.Gender,
                                      Contact = s.Contact,
                                      CourseId = s.CourseId,
                                      courseName = c.CourseName
                                  }).ToListAsync<StudentCourse>();
            }
            else
            {
                students = await (from s in _context.Student
                                  join c in _context.Course
                                  on s.CourseId equals c.CourseId
                                  select new StudentCourse
                                  {
                                      StudentId = s.StudentId,
                                      StudentName = s.StudentName,
                                      Gender = s.Gender,
                                      Contact = s.Contact,
                                      CourseId = s.CourseId,
                                      courseName = c.CourseName
                                  }).ToListAsync<StudentCourse>();
            }
            return students;
        }

        // GET: api/StudentWebAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // GET: api/StudentWebAPI/5
        [HttpGet("{id}")]
        [Route("{search}/{id}")]
        public async Task<ActionResult<List<StudentCourse>>> GetStudentByCourseId(int id)
        {
            List<StudentCourse> students = null;

            students = await (from s in _context.Student
                              join c in _context.Course
                              on s.CourseId equals c.CourseId
                              where s.CourseId == id
                              select new StudentCourse
                              {
                                  StudentId = s.StudentId,
                                  StudentName = s.StudentName,
                                  Gender = s.Gender,
                                  Contact = s.Contact,
                                  CourseId = s.CourseId,
                                  courseName = c.CourseName
                              }).ToListAsync<StudentCourse>();


            if (students == null)
            {
                return NotFound();
            }

            return students;
        }


        // GET: api/StudentWebAPI/5
        [HttpGet]
        [Route("{searchByContact}/{start}/{end}")]
        public async Task<ActionResult<List<StudentCourse>>> GetStudentByContact(int start,int end)
        {
            List<StudentCourse> students = null;
            //int end = 8;
            students = await (from s in _context.Student
                              join c in _context.Course
                              on s.CourseId equals c.CourseId
                              where s.StudentId > start &&
                              s.StudentId < end
                              select new StudentCourse
                              {
                                  StudentId = s.StudentId,
                                  StudentName = s.StudentName,
                                  Gender = s.Gender,
                                  Contact = s.Contact,
                                  CourseId = s.CourseId,
                                  courseName = c.CourseName
                              }).ToListAsync<StudentCourse>();


            if (students == null)
            {
                return NotFound();
            }

            return students;
        }

        // PUT: api/StudentWebAPI/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StudentWebAPI
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.StudentId }, student);
        }

        // DELETE: api/StudentWebAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> DeleteStudent(int id)
        {
            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Student.Remove(student);
            await _context.SaveChangesAsync();

            return student;
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}

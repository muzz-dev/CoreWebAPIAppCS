using CoreWebAPIAppCS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPIAppCS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchAPIController : ControllerBase
    {
        private readonly DBCollegeContext _context;
        public SearchAPIController(DBCollegeContext context)
        {
            _context = context;
        }

        // GET: api/SearchWebAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentCourse>>> PostStudent(StudentSearch student)
        {
            List<StudentCourse> students = null;
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
            if (student.studentId != null)
            {
                students = (from s in students
                            where s.StudentId == student.studentId
                            select s).ToList();
            }
            if (student.studentName != null)
            {
                students = (from s in students
                            where s.StudentName.Contains(student.studentName)
                            select s).ToList();
            }
            if (student.start != null)
            {
                students = students.Where(s => s.StudentId > student.start).ToList();
            }
            if (student.end != null)
            {
                students = students.Where(s => s.StudentId < student.end).ToList();
            }
            if (student.courseId != null)
            {
                students = students.Where(s => s.CourseId == student.courseId).ToList();
            }

            return students;
        }
    }
}

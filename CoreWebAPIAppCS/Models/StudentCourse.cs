using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebAPIAppCS.Models
{
    public class StudentCourse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public int CourseId { get; set; }
        public string courseName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCatalog.Domain.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string CourseName { get; set; } = null!;

        public int? RoomNumber { get; set; }

        public bool IsDeleted { get; set; }

        public bool Monday { get; set; }

        public bool Tuesday { get; set; }

        public bool Wednesday { get; set; }

        public bool Thursday { get; set; }

        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }

        public string? ProfessorName { get; set; }

        public string? ProfessorEmail { get; set; }
    }
}

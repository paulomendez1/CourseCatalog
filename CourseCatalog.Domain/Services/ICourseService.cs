using CourseCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCatalog.Domain.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetCoursesAsync(CancellationToken token);
        Task<Course> GetCourseByIdAsync(int courseId, CancellationToken token);
        Task<Course> AddCourseAsync(Course request, CancellationToken token);
        Task<Course> EditCourseAsync(Course request, CancellationToken token);
        Task<bool> DeleteCourseAsync(int courseId, CancellationToken token);
    }
}

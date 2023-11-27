using AutoMapper;
using CourseCatalog.Domain.Contracts;
using CourseCatalog.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCatalog.Domain.Services
{
    public class CourseService : ICourseService
    {
        private readonly IBaseRepository<Course> _courseRepository;

        public CourseService(IBaseRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }
        public async Task<Course> AddCourseAsync(Course request, CancellationToken token)
        {
            var course = new Course
            {
                CourseName = request.CourseName,
                ProfessorName = request.ProfessorName,
                ProfessorEmail = request.ProfessorEmail,
                Friday = request.Friday,
                Thursday = request.Thursday,
                Wednesday = request.Wednesday,
                Tuesday = request.Tuesday,
                Monday = request.Monday,
                RoomNumber = request.RoomNumber,
                IsDeleted = false
            };
            var result = _courseRepository.Add(course, token);
            await _courseRepository.UnitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<bool> DeleteCourseAsync(int courseId, CancellationToken token)
        {
            if (courseId <= 0) throw new InvalidOperationException("");
            var result = await _courseRepository.GetByIdAsync(courseId, token);
            if (result == null) return false;
            result.IsDeleted = true;
            _courseRepository.Update(result, token);
            await _courseRepository.UnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<Course> EditCourseAsync(Course request, CancellationToken token)
        {
            var existingRecord = await _courseRepository.GetByIdAsync(request.CourseId, token);
            if (existingRecord == null)
            {
                throw new ArgumentException($"Course with {request.CourseId} is not present");
            }
            var result = _courseRepository.Update(request, token);
            await _courseRepository.UnitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Course> GetCourseByIdAsync(int courseId, CancellationToken token)
        {
            if (courseId <= 0) throw new ArgumentNullException();

            var entity = await _courseRepository.GetByIdAsync(courseId, token);
           
            return entity;
        }

        public async Task<IEnumerable<Course>> GetCoursesAsync(CancellationToken token)
        {
            var result = await _courseRepository.GetAllAsync(token);

            return result.Where(x=> !x.IsDeleted);
        }
    }
}

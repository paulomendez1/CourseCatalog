using Azure.Core;
using CourseCatalog.API.Filters;
using CourseCatalog.Domain.Contracts;
using CourseCatalog.Domain.Models;
using CourseCatalog.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseCatalog.API.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ILogger<CourseController> _logger;
        public CourseController(ICourseService courseService, ILogger<CourseController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]

        public async Task<IActionResult> Get(CancellationToken token)
        {
            try
            {
                _logger.LogInformation("Getting courses...");

                var courses = await _courseService.GetCoursesAsync(token);

                return Ok(courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} for {ex.Data}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
          

        }

        [HttpGet("{id:int}")]
        [CourseExists]
        public async Task<IActionResult> GetById(int id, CancellationToken token)
        { 
            try
            {
                _logger.LogInformation($"Getting item with ID:{id}...");

                var result = await _courseService.GetCourseByIdAsync(id, token);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]

        public async Task<IActionResult> Post(Course request, CancellationToken token)
        {
            try
            {
                _logger.LogInformation("Creating a course...");

                var result = await _courseService.AddCourseAsync(request, token);

                return CreatedAtAction(nameof(GetById), new { id = result.CourseId }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} for {ex.Data}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpPut("{id:int}")]
        [CourseExists]
        public async Task<IActionResult> Put(int id, Course request, CancellationToken token)
        {
            try
            {
                _logger.LogInformation($"Updating course with ID: {id}");

                request.CourseId = id;
                var result = await _courseService.EditCourseAsync(request, token);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} for {ex.Data}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        [CourseExists]
        public async Task<IActionResult> Delete(int id, CancellationToken token)
        {
            _logger.LogInformation($"Deleting course with ID: {id}");

            if (id <= 0)
            {
                return BadRequest();
            }
            var item = await _courseService.DeleteCourseAsync(id, token);
            return NoContent();
        }
    }
}

using CourseCatalog.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CourseCatalog.API.Filters
{
    public class CourseExistsAttribute : TypeFilterAttribute
    {
        public CourseExistsAttribute() : base(typeof(CourseExistsFilterImpl)) { }
        public class CourseExistsFilterImpl : IAsyncActionFilter
        {
            private readonly ICourseService _courseService;
            private readonly ILogger<CourseExistsAttribute> _logger;
            public CourseExistsFilterImpl(ICourseService courseService, ILogger<CourseExistsAttribute> logger)
            {
                _courseService = courseService;
                _logger = logger;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                _logger.LogInformation($"Checking if there is a Course with the ID provided");
                if (!(context.ActionArguments["id"] is int id))
                {
                    _logger.LogError("No ID or incorrect ID provided for course");
                    context.Result = new BadRequestResult();
                    return;
                }
                var result = await _courseService.GetCourseByIdAsync(id, default(CancellationToken));
                if (result == null)
                {
                    _logger.LogError($"Course with ID: {id} not exist.");
                    context.Result = new NotFoundObjectResult($"Course with id {id} not exist.");
                    return;
                }
                await next();
            }
        }
    }
}

using FUBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace FUCourseManagement.Controllers
{
    public class CoursesController : BaseController<Course, int>
    {
        public CoursesController(ICourseRepository repository)
            : base(repository) { }
    }
}

using FUBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using Repositories;

namespace FUCourseManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EnrollmentRecordsController : BaseController<EnrollmentRecord, int>
    {
        public EnrollmentRecordsController(IEnrollmentRecordRepository repository)
            : base(repository) { }
    }
}

using FUBusiness.Models;
using Repositories;

namespace FUCourseManagement.Controllers
{
    public class EnrollmentRecordsController : BaseController<EnrollmentRecord, int>
    {
        public EnrollmentRecordsController(IEnrollmentRecordRepository repository)
            : base(repository) { }
    }
}

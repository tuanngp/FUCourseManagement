using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessObjects;
using FUBusiness.Models;
using Microsoft.EntityFrameworkCore;

namespace FUDataAccess
{
    public class CourseDAO : BaseDAO<Course, int> { }
}

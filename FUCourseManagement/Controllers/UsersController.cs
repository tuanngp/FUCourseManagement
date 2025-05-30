﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FUBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace FUCourseManagement.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : BaseController<User, int>
    {
        public UsersController(IUserRepository repository)
            : base(repository) { }
    }
}

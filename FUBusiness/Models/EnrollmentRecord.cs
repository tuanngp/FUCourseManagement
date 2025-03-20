﻿using System;
using System.Collections.Generic;

namespace FUBusiness.Models;

public partial class EnrollmentRecord
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? CourseId { get; set; }

    public DateTime? EnrollDate { get; set; }

    public bool? Dropped { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}

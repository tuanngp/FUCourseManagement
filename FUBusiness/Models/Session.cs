﻿using System;
using System.Collections.Generic;

namespace FUBusiness.Models;

public partial class Session
{
    public string? SessionId { get; set; } = null!;

    public int? UserId { get; set; }

    public string Role { get; set; } = null!;

    public DateTime? ExpiresAt { get; set; }

    public virtual User? User { get; set; }

    public override string? ToString()
    {
        return base.ToString();
    }
}

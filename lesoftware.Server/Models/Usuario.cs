﻿using System;
using System.Collections.Generic;

namespace lesoftware.Server.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Usuario1 { get; set; }

    public string? Password { get; set; }

    public DateOnly? Fecha { get; set; }
}

﻿using System;
using System.Collections.Generic;

namespace vue_blog_quangtt.Models.EF;

public partial class Location
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<BlogLocation> BlogLocations { get; set; } = new List<BlogLocation>();
}

﻿using System;
using System.Collections.Generic;

namespace vue_blog_quangtt.Models.EF;

public partial class Type
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}

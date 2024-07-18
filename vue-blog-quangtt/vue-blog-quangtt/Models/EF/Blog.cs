using System;
using System.Collections.Generic;

namespace vue_blog_quangtt.Models.EF;

public partial class Blog
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public bool? State { get; set; }

    public DateTime? Date { get; set; }

    public string? Note { get; set; }

    public string? Detail { get; set; }

    public virtual ICollection<BlogLocation> BlogLocations { get; set; } = new List<BlogLocation>();
}

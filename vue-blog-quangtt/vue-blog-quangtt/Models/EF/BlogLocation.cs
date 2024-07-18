using System;
using System.Collections.Generic;

namespace vue_blog_quangtt.Models.EF;

public partial class BlogLocation
{
    public int Id { get; set; }

    public int? IdBlog { get; set; }

    public int? IdLocation { get; set; }

    public virtual Blog? IdBlogNavigation { get; set; }

    public virtual Location? IdLocationNavigation { get; set; }
}

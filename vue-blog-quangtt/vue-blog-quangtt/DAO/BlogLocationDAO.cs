﻿using vue_blog_quangtt.Models.EF;
using vue_blog_quangtt.Models.VIEW;

namespace vue_blog_quangtt.DAO
{
	public class BlogLocationDAO
	{
        private BlogManagementContext context = new BlogManagementContext();
        public void InsertOrUpdate(BlogLocation item)// thêm mới và sửa 
        {
            if (item.Id == 0)
            {
                context.BlogLocations.Add(item);
            }
            context.SaveChanges();
        }
        public List<BlogLocationVIEW> getByIdBlog(int id)// tìm kiếm theo idBlog
        {
            var query=(from a in context.BlogLocations
                      where a.IdBlog == id
                      select new BlogLocationVIEW {
                          Id= a.Id,
                          IdBlog = a.IdBlog.Value,
                          IdLocation=a.IdLocation.Value,
                      });
            return query.ToList();
        }
        public BlogLocation getItem(int id)//tìm kiếm theo id
        {
            var query = context.BlogLocations.Where(x => x.Id == id).FirstOrDefault();

            return query;
        }
        public void Delete(int id)// hàm xóa
        {
            var blogLocation = getItem(id);
            context.BlogLocations.Remove(blogLocation);
            context.SaveChanges();
        }

    }
}

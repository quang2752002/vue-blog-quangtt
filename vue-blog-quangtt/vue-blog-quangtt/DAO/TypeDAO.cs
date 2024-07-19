﻿using vue_blog_quangtt.Models.EF;
using vue_blog_quangtt.Models.VIEW;

namespace vue_blog_quangtt.DAO
{
    public class TypeDAO
    {
        private BlogManagementContext context = new BlogManagementContext();
        public TypeVIEW getItemView(int id)//tìm loại theo id
        {
            var query = (from a in context.Types                     
                         where a.Id == id
                         select new TypeVIEW
                         {
                             Id = a.Id,
                             Name = a.Name ?? "",                          
                         }).FirstOrDefault();

            return query;
        }
        public TypeVIEW getItemByIdBlog(int id)//tìm loại theo idblog
        {
            var query = (from a in context.Types
                         join b in context.Blogs on a.Id equals b.IdType
                         where b.Id == id
                        
                         select new TypeVIEW
                         {
                             Id = a.Id,
                             Name = a.Name ?? "",
                         }).FirstOrDefault();

            return query;
        }
        public List<TypeVIEW> getList()// lấy danh sách loại
        {
            var query = (from a in context.Types
                         select new TypeVIEW
                         {
                             Id = a.Id,
                             Name = a.Name ?? "",                          
                         });

            return query.ToList();
        }

    }
}

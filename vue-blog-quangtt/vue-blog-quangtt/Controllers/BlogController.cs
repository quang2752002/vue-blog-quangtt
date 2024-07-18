using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Drawing;
using vue_blog_quangtt.DAO;
using vue_blog_quangtt.Models.EF;

namespace vue_blog_quangtt.Controllers
{
	public class BlogController : Controller
	{
        private const string IdBlog = "IdBlog";

        public IActionResult Index()
		{
			return View();
		}
        [HttpPost]
        public JsonResult Showlist(string name = "")//lấy toàn bộ dữ liệu trả về json
        {
            BlogDAO blogDAO = new BlogDAO();

           
            var query = blogDAO.Search( name);
          
            return Json(new { data = query});
        }
        public IActionResult Create() 
        {
            return View();
        }
        public JsonResult CreateBlog(string name, string type, bool state, string arr, DateTime date,string note,string detail)//thêm mới 
        {
           
            BlogDAO blogDAO = new BlogDAO();
            BlogLocationDAO blogLocationDAO = new BlogLocationDAO();
            Blog blog = new Blog
            {
                Name = name,
                Type = type,
                State = state,
                Date = date,
                Note = note,
                Detail = detail
                
            };

            blogDAO.InsertOrUpdate(blog);// thêm mới blog


            // lấy list id của location thêm mới của blog
            if (arr == null) arr = "";
            List<int> locationIds = new List<int>();

            if (!string.IsNullOrEmpty(arr))
            {
                List<string> list = arr.Split('-').ToList();
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (int.TryParse(item, out var result))
                        {
                            locationIds.Add(result);
                        }
                    }
                }
            }


            // thêm mới list id location của blog vào bảng blogLocation
            foreach (var locationId in locationIds)
            {
                BlogLocation blogLocation = new BlogLocation
                {
                    IdBlog = blog.Id,
                    IdLocation = locationId
                };
                blogLocationDAO.InsertOrUpdate(blogLocation);
            }

            return Json(new { mess = "Thêm mới thành công" });
        }



        public IActionResult Edit(int id)
        {
            HttpContext.Session.SetInt32(IdBlog, id);

            return View();
        }
        public JsonResult Update(string name, string type, bool state, string arr, DateTime date,string note, string detail)//chỉnh sửa  blog
        {
            int id = HttpContext.Session.GetInt32(IdBlog) ?? 0;
            BlogDAO blogDAO = new BlogDAO();
            BlogLocationDAO blogLocationDAO = new BlogLocationDAO();
            Blog blog = blogDAO.getItem(id);

            blog.Name = name;
            blog.Type = type;
            blog.State = state;
            blog.Date = date;
            blog.Note = note;
            blog.Detail = detail;
            blogDAO.InsertOrUpdate(blog);   //chỉnh sửa lại thông tin blog

            // lấy list id của location mà blog cập nhật
            if (arr == null) arr = "";
            List<int> locationIds = new List<int>();

            if (!string.IsNullOrEmpty(arr))
            {
                List<string> list = arr.Split('-').ToList();
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        if (int.TryParse(item, out var result))
                        {
                            locationIds.Add(result);
                        }
                    }
                }
            }

            var query = blogLocationDAO.getByIdBlog(id);//lấy list blogLocation theo idBlog
            List<int> listIdBlogLocation = query.Select(loc => loc.Id).ToList();// lấy id 
           
            //xóa dữ liệu location cũ của blog
            foreach( var item in listIdBlogLocation)
            {
                blogLocationDAO.Delete(item);
            }
           
            // cập nhật lại location của blog dữ liệu mới
            foreach (var locationId in locationIds)
            {
                BlogLocation blogLocation = new BlogLocation
                {
                    IdBlog = blog.Id,
                    IdLocation = locationId
                };
                blogLocationDAO.InsertOrUpdate(blogLocation);
            }
            return Json(new { mess = "Chỉnh sửa thành công" });
        }
        public JsonResult Delete(int id)//hàm xóa blog
        {
            BlogDAO blogDAO=new BlogDAO();  
            blogDAO.Delete(id);
            return Json(new { mess = "xóa thành công " });

        }
        public JsonResult listLocation() //list location
        {
            LocationDAO locationDAO=new LocationDAO();
           var query= locationDAO.Search();
            return Json(new { data = query });

        }
        public JsonResult getBlog()// lấy thông tin blog 
        {
            int id = HttpContext.Session.GetInt32(IdBlog) ?? 0;//lấy id là session
            BlogDAO blogDAO = new BlogDAO();
            BlogLocationDAO blogLocationDAO = new BlogLocationDAO();

            var query = blogDAO.getItemView(id);
            var listBlogLocations = blogLocationDAO.getByIdBlog(id);

            var arr = listBlogLocations.Select(loc => loc.IdLocation).ToList();//lây list id của location

            return Json(new { data = query, arr = arr });
        }

    }
}

using BlogApp.Data.Abstract;
using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Conctrete.EfCore
{
    public class EfBlogRepository : IBlogRepository
    {
        private BlogContext context;
        public EfBlogRepository(BlogContext _context)
        {
            context = _context;
        }
        public void AddBlog(Blog etnity)
        {
            context.Blogs.Add(etnity);
            context.SaveChanges();
        }

        public void DeleteBlog(int blogId)
        {
            var blog = context.Blogs.FirstOrDefault(i => i.BlogId == blogId);
            if (blog != null)
            {
                context.Blogs.Remove(blog);
                context.SaveChanges();
            }
        }

        public IQueryable<Blog> GetAll()
        {
            return context.Blogs;

        }

        public Blog GetById(int blogId)
        {
            return context.Blogs.FirstOrDefault(i => i.BlogId == blogId);
        }

        public void SaveBlog(Blog entity)
        {
            if(entity.BlogId == 0)
            {
                entity.Date = DateTime.Now;
                context.Blogs.Add(entity);
            }
            else
            {
                var blog = GetById(entity.BlogId);
                if (blog != null)
                {
                    blog.Title = entity.Title;
                    blog.Description = entity.Description;
                    blog.CategoryId = entity.CategoryId;
                    blog.Image = entity.Image;
                    blog.Body = entity.Body;
                    blog.isApproved = entity.isApproved;
                    blog.isHome = entity.isHome;
                    blog.isSlider = entity.isSlider;
                    context.SaveChanges();
                }
             
            }
            context.SaveChanges();
        }

        public void UpdateBlog(Blog entity)
        {
           
        }
    }
}

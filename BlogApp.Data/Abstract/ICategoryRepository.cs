using BlogApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogApp.Data.Abstract
{
    public interface ICategoryRepository
    {
        Category GetById(int categoryId);
        IQueryable<Category> GetAll();
        void AddCategory(Category etnity);
        void UpdateCategory(Category entity);
        void DeleteCategory(int categoryId);
        void SaveCategory(Category entity);
    }
}

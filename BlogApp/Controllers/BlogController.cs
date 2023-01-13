using BlogApp.Data.Abstract;
using BlogApp.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class BlogController : Controller
    {
        private IBlogRepository _blogRepository;
        private ICategoryRepository _categoryRepository;
        public BlogController(IBlogRepository _blogRepo, ICategoryRepository _categoryRepo)
        {
            _blogRepository = _blogRepo;
            _categoryRepository = _categoryRepo;
        }
        public IActionResult Index(int? id, string q)
        {
            var query = _blogRepository.GetAll().Where(i => i.isApproved);

            if (id != null)
            {
                query = query.Where(i => i.CategoryId == id).OrderByDescending(i => i.Date);


            }
            if (!string.IsNullOrEmpty(q))
            {
                //query = query.Where(i => i.Title.Contains(q) || i.Description.Contains(q) || i.Body.Contains(q));
                query = query.Where(i => EF.Functions.Like(i.Title, "%"+q+"%") || EF.Functions.Like(i.Description, "%" + q + "%") || EF.Functions.Like(i.Body, "%" + q + "%"));

            }
            return View(query);
        }
        public IActionResult List()
        {
            return View(_blogRepository.GetAll()); 
        }
       
        
        //[HttpGet]
        //public IActionResult AddOrUpdate(int? id)
        //{
        //    ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

        //    if (id==null)
        //    {

        //        return View(new Blog());
        //    }
        //    else
        //    {
        //        return View(_blogRepository.GetById((int)id));
        //    }
        //}

        //[HttpPost]
        //public IActionResult AddOrUpdate(Blog entity)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _blogRepository.SaveBlog(entity);
        //        TempData["message"] = $"{entity.Title} kayit edildi";
        //        return RedirectToAction("List");
        //    }
        //    ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

        //    return View(entity);
        //}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(_blogRepository.GetById(id));
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int BlogId)
        {
            _blogRepository.DeleteBlog(BlogId);
            TempData["message"] = $"{BlogId} numarali kayit silindi";
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if (_blogRepository.GetById(id) == null)
            {
                return NotFound();
            }
            
            return View(_blogRepository.GetById(id));
        }




        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");
            return View(new Blog());
         
        }

        [HttpPost]
        public IActionResult Create(Blog entity)
        {
            if (ModelState.IsValid)
            {
                _blogRepository.SaveBlog(entity);
                TempData["message"] = $"{entity.Title} kayit edildi";
                return RedirectToAction("List");
            }
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

            return View(entity);
        }





        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");
            
            return View(_blogRepository.GetById((int) id));
             
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Blog entity, IFormFile file)
        {
           
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    entity.Image = file.FileName;
                    
                    _blogRepository.SaveBlog(entity);
                    TempData["message"] = $"{entity.Title} kayit edildi";
                    return RedirectToAction("List");    
                }
               
            }
                    _blogRepository.SaveBlog(entity);


            ViewBag.Categories = new SelectList(_categoryRepository.GetAll(), "CategoryId", "Name");

            return RedirectToAction("List");
        }
    }
}

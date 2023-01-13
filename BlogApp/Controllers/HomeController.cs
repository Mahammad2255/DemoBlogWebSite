using BlogApp.Data.Abstract;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private IBlogRepository blogRepository;
        public HomeController(IBlogRepository repository)
        {
            blogRepository = repository;
        }
        public IActionResult Index()
        {
            HomeBlogModel model = new HomeBlogModel();

            model.HomeBlogs = blogRepository.GetAll().Where(i => i.isApproved && i.isHome).ToList();
            model.SliderBlogs = blogRepository.GetAll().Where(i => i.isApproved && i.isSlider).ToList();

            return View(model);
        }
        public IActionResult Details()
        {
            return View();
        } 
        public IActionResult List()
        {
            return View();
;        }
    }
}

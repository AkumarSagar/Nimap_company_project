using Nimap_company_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace Nimap_company_project.Controllers
{
    public class CategoryController : Controller
    {
        StoreDbContext dc=new StoreDbContext();
        public ViewResult DisplayCategories()
        {
            var categories = dc.Categories;
            return View(categories);
        }
        public ViewResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public RedirectToRouteResult AddCategory(Category category)
        {
            dc.Categories.Add(category);
            dc.SaveChanges();
            return RedirectToAction("DisplayCategories");
        }
        public ViewResult EditCategory(int CategoryId)
        {
            Category category = dc.Categories.Find(CategoryId);
            return View(category);
        }
        [HttpPost]
        public RedirectToRouteResult UpdateCategory(Category category)
        {
            dc.Entry(category).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("DisplayCategories");
        }
        public RedirectToRouteResult DeleteCategory(int CategoryId)
        {
            Category category=dc.Categories.Find(CategoryId);
            dc.Categories.Remove(category);
            dc.SaveChanges();
            return RedirectToAction("DisplayCategories");
        }
    }
}
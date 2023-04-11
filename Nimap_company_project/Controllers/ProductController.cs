using Nimap_company_project.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;



namespace Nimap_company_project.Controllers
{
    public class ProductController : Controller
    {
        StoreDbContext dc=new StoreDbContext();
       public ViewResult DisplayProducts()
        {  
            dc.Configuration.LazyLoadingEnabled= false;
            var products = dc.Products.Include(P => P.Category); 
            return View(products); 
        }
        public ViewResult DisplayProduct(int ProductId)
        {
            dc.Configuration.LazyLoadingEnabled = false;
            var product = dc.Products.Include(P => P.Category).Where(P=>P.ProductId== ProductId).Single();
            return View(product);
        }
        [HttpGet]
        public ViewResult AddProduct()
        {
            ViewBag.CategoryId = new SelectList(dc.Categories, "CategoryId", "CategoryName");
            return View();

        }
        [HttpPost]
        public RedirectToRouteResult AddProduct(Product product)
        {
            dc.Products.Add(product);
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");

        }
        public ViewResult EditProduct(int ProductId)
        {
            Product product = dc.Products.Find(ProductId);
            ViewBag.CategoryId = new SelectList(dc.Categories, "CategoryId", "CategoryName",product.CategoryId);
            return View(product);
        }
        [HttpPost]
        public RedirectToRouteResult UpdateProduct(Product product)
        {
            dc.Entry(product).State = EntityState.Modified;
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }
        public RedirectToRouteResult DeleteProduct(int ProductId)
        {
            Product product= dc.Products.Find(ProductId);
            dc.Products.Remove(product);
            dc.SaveChanges();
            return RedirectToAction("DisplayProducts");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStokSistemi.Models.Entity;

namespace MvcStokSistemi.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category

        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
           var categories = db.Tbl_Category.ToList();
            return View(categories);
        }

        [HttpGet]
        public ActionResult NewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewCategory(Tbl_Category p)
        {
            db.Tbl_Category.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            var category = db.Tbl_Category.Find(id);
            db.Tbl_Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCategory(int id)
        {
            var cat = db.Tbl_Category.Find(id);
            return View("GetCategory", cat);
        }

        public ActionResult UpdateCategory(Tbl_Category p)
        {
            var cat = db.Tbl_Category.Find(p.Id);
            cat.Name = p.Name;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
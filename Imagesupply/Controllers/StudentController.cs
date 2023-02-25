using Imagesupply.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Imagesupply.Controllers
{
    public class StudentController : Controller
    {

        PracticeEntities1 _db = new PracticeEntities1();
        // GET: Student
        public ActionResult Index()
        {
            var data = _db.registertbls.ToList();
            return View(data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(registertbl s)
        {
            string filename = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
            String extension =Path.GetExtension(s.ImageFile.FileName);
            filename = filename + extension;
            s.photo = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"),filename);
            s.ImageFile.SaveAs(filename);
            _db.registertbls.Add(s);
            int a = _db.SaveChanges();

            if (a > 0)
            {
                ViewBag.message = "<script> alert('successfully added') </script>";
                ModelState.Clear();
            }
            else
            {
                ViewBag.message = "<script> alert('Data Not recorded') </script>";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _db.registertbls.Where(x=>x.id==id).FirstOrDefault();
            Session["Image"] = data.photo;
            return View(data);

        }

        [HttpPost]
        public ActionResult Edit(registertbl r)
        {
            if (ModelState.IsValid == true)
            {
                if(r.ImageFile!=null)
                {
                    string filename = Path.GetFileNameWithoutExtension(r.ImageFile.FileName);
                    String extension = Path.GetExtension(r.ImageFile.FileName);
                    filename = filename + extension;
                    r.photo = "~/Image/" + filename;
                    filename = Path.Combine(Server.MapPath("~/Image/"), filename);
                    r.ImageFile.SaveAs(filename);
                   /* r.photo = Session["Image3"].ToString();*/
                    _db.Entry(r).State = EntityState.Modified;
                    int a = _db.SaveChanges();

                    if (a > 0)
                    {
                        ViewBag.message = "<script> alert('successfully added') </script>";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.message = "<script> alert('Data Not recorded') </script>";
                    }

                    return RedirectToAction("Index");
                }

            }

            return View(r);
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                var data = _db.registertbls.Where(x=>x.id==id).FirstOrDefault();

                if (data != null)
                {
                    _db.Entry(data).State = EntityState.Deleted;
                    int a = _db.SaveChanges();

                    if (a > 0)
                    {
                        ViewBag.message = "Deleted Successfully";
                        return RedirectToAction("Index");
                    }
                }

            }
            return RedirectToAction("Index");
        }


        public ActionResult Details(int id)
        {
            var data = _db.registertbls.Where(x=>x.id==id).FirstOrDefault();
            Session["Image1"] = data.photo.ToString();
            return View(data);
        }
    }
}
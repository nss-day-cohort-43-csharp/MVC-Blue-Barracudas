using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class TagPostController : Controller
    {
        // GET: TagPostController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TagPostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TagPostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagPostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagPostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TagPostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagPostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TagPostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

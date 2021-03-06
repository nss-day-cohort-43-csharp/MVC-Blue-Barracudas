﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        //Gets an instance of our Category Repository and makes it accessible to the rest of the controller
        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            List<Category> categories = _categoryRepo.GetAll();
            return View(categories);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            Category category = new Category();

            return View(category);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                List<Category> categories = _categoryRepo.GetAll();
                
                foreach (Category c in categories)
                {
                    if (c.Name.ToLower() == category.Name.Trim().ToLower())
                    {
                        throw new Exception();
                    }
                }

                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                category.Name = textInfo.ToTitleCase(category.Name);

                _categoryRepo.Add(category);

                return RedirectToAction(nameof(Index), "Category");
            }
            catch
            {
                category.ErrorMessage = "A category with that name already exists!";
                return View(category);
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            Category category = _categoryRepo.GetCategoryById(id);

            if (category == null || id == 1)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                List<Category> categories = _categoryRepo.GetAll();

                foreach (Category c in categories)
                {
                    if (c.Name.ToLower() == category.Name.Trim().ToLower())
                    {
                        throw new Exception();
                    }
                }

                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                category.Name = textInfo.ToTitleCase(category.Name);

                _categoryRepo.Edit(category);

                return RedirectToAction(nameof(Index), "Category");
            }
            catch
            {
                category.ErrorMessage = "A category with that name already exists!";
                return View(category);
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            Category category = _categoryRepo.GetCategoryById(id);

            if (category == null || id == 1)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepo.Delete(id);

                return RedirectToAction(nameof(Index), "Category");
            }
            catch
            {
                return View(category);
            }
        }
    }
}

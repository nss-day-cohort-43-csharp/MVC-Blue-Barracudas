using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Repositories;
using System.Linq;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepo;

        public TagController(ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
        }

        // GET: TagController
        public ActionResult Index()
        {
            var tags = _tagRepo.GetAllTags();
            return View(tags);
        }

        // GET: TagController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                List<Tag> tags = _tagRepo.GetAllTags();
                bool isDuplicate = false;
                //check for duplicate tags
                foreach (Tag t in tags)
                {
                    if (t.Name.ToLower() == tag.Name.ToLower())
                    {
                        //get duplicate taginfo
                        //return to list dont add duplicate
                        isDuplicate = true; 
                    }
                }


                if (isDuplicate == false)
                {
                    _tagRepo.AddTag(tag);
                }

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View(tag);
            }
        }

        // GET: TagController/Edit/5
        public ActionResult Edit(int id)
        {
            Tag tag = _tagRepo.GetTagById(id);
            return View(tag);
        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            try
            {
                _tagRepo.Edit(tag);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(tag);
            }
        }

        // GET: TagController/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepo.GetTagById(id);
            return View(tag);
        }

        // POST: TagController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            try
            {
                _tagRepo.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Repositories;
using System.Linq;
using System.Globalization;

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
            Tag tag = new Tag();
            return View(tag);
        }

        // POST: TagController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                List<Tag> tags = _tagRepo.GetAllTags();
                //check for duplicate tags
                foreach (Tag t in tags)
                {
                    if (t.Name.ToLower() == tag.Name.ToLower().Trim())
                    {
                        throw new Exception();
                    }
                }

                    //title case tag name
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    tag.Name = textInfo.ToTitleCase(tag.Name);

                    _tagRepo.AddTag(tag);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                tag.ErrorMessage = "A tag with that name already exists!";
                return View(tag);
            }
        }

        // GET: TagController/Edit/5
        public ActionResult Edit(int id)
        {
            Tag validTag = _tagRepo.GetTagById(id);

            if(validTag != null)
            {
                Tag tag = _tagRepo.GetTagById(id);
                return View(tag);
            } else
            {
                return NotFound();
            }

        }

        // POST: TagController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tag tag)
        {
            try
            {
                List<Tag> tags = _tagRepo.GetAllTags();

                //check for duplicate tags
                foreach (Tag t in tags)
                {
                    if (t.Name.ToLower() == tag.Name.ToLower().Trim())
                    {
                        throw new Exception();
                    }
                }

                    //title case tag name
                    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                    tag.Name = textInfo.ToTitleCase(tag.Name);

                    _tagRepo.Edit(tag);
           
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                tag.ErrorMessage = "A tag with that name already exists!";
                return View(tag);
            }
        }

        // GET: TagController/Delete/5
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepo.GetTagById(id);

            if(tag != null)
            {
                return View(tag);
            }
            else
            {
                return NotFound();
            }

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

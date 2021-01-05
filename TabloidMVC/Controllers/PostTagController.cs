﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TabloidMVC.Repositories;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Models
{
    public class PostTagController : Controller
    {
        private readonly IPostTagRepository _postTagRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IPostRepository _postRepo;

        public PostTagController(IPostTagRepository postTagRepo, ITagRepository tagRepo, IPostRepository postRepo)
        {
            _postTagRepo = postTagRepo;
            _tagRepo = tagRepo;
            _postRepo = postRepo;
        }

        // GET: PostTagsController
        public ActionResult Index()
        {
            //gey postId fro query string
            int postId = Int32.Parse(HttpContext.Request.Query["postId"]);

            Post post = _postRepo.GetPublishedPostById(postId);

            List<PostTag> postTags = _postTagRepo.GetPostTagsbyPostId(postId);;

            List<Tag> tags = _tagRepo.GetAllTags();

            //add tagids the are attached to post
            foreach(PostTag pTag in postTags)
            {
                int tagId = pTag.TagId;
                tags.RemoveAll(t => t.Id == tagId);
            }

            PostTagIndexViewModel vm = new PostTagIndexViewModel
            {
                Post = post,
                PostTags = postTags,
                Tags = tags
            };

            return View(vm);
        }

        // GET: PostTagsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostTagsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostTagsController/Create
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

        // GET: PostTagsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostTagsController/Edit/5
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

        // GET: PostTagsController/Delete/5
        //Soft Delete
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostTagsController/Delete/5
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

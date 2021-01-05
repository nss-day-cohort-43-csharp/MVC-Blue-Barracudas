using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TabloidMVC.Repositories;

namespace TabloidMVC.Models
{
    public class PostTagController : Controller
    {
        private readonly IPostTagRepository _postTagRepo;

        public PostTagController(IPostTagRepository postTagRepo)
        {
            _postTagRepo = postTagRepo;
        }

        // GET: PostTagsController
        public ActionResult Index()
        {
            //gey postId fro query string
            int postId = Int32.Parse(HttpContext.Request.Query["postId"]);

            List<PostTag> postTags = _postTagRepo.GetPostTagsbyPostId(postId);

            return View(postTags);
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

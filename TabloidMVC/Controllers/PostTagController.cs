﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TabloidMVC.Repositories;
using TabloidMVC.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace TabloidMVC.Models
{
    [Authorize]
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

            Post post = _postRepo.GetPostById(postId);

            if(post == null)
            {
                return NotFound();
            }

            List<PostTag> postTags = _postTagRepo.GetPostTagsbyPostId(postId);;

            //holds tags that are not assigned to post
            List<Tag> tags = _tagRepo.GetAllTags();

            //add tagids the are attached to post
            foreach(PostTag pTag in postTags)
            {
                int tagId = pTag.TagId;
                //remove the tag  if the Id is found
                tags.RemoveAll(t => t.Id == tagId);
            }

            PostTagIndexViewModel vm = new PostTagIndexViewModel
            {
                Post = post,
                PostTags = postTags, //post-tag relationships for this post
                Tags = tags //unsued tags
            };

            return View(vm);
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

        //used when the ADD but on posttag/index is clicked
        public ActionResult AddToPost(PostTag postTag)
        {
            try
            { 
                _postTagRepo.AddTag(postTag);
                return RedirectToAction("Index", "PostTag", new { @postId = postTag.PostId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public ActionResult RemoveFromPost(PostTag postTag)
        {
            try
            {
                //find tag relationshp id
                PostTag foundPostTag = _postTagRepo.GetPostTagbyPostIdAndTagId(postTag.PostId, postTag.TagId);

                //only delete if relatioship exist
                if (foundPostTag != null)
                {
                    _postTagRepo.DeleteTag(foundPostTag.Id);
                }

                return RedirectToAction("Index", "PostTag", new { @postId = postTag.PostId });
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}

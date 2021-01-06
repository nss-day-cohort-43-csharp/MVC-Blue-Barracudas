using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IPostRepository _postRepo;


        public CommentController(ICommentRepository commentRepo, IPostRepository postRepo)
        {
            _commentRepo = commentRepo;
            _postRepo = postRepo;
        }

        // GET: Comment
        public ActionResult Index(int postId)
        {
            try
            {
                PostCommentsViewModel vm = new PostCommentsViewModel()
                {
                    Post = _postRepo.GetPublishedPostById(postId),
                    Comments = _commentRepo.GetAllPostComments(postId)
                };
                if(vm.Post == null)
                {
                    throw new Exception();
                }
                return View(vm); 
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Comment/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Comment/Create
        public ActionResult Create(int postId)
        {
            Comment comment = new Comment()
            {
                PostId = postId
            };
            return View(comment);
        }

        // POST: Comment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comment comment)
        {
            //set the userId
            comment.UserProfileId = GetCurrentUserId();

            //set the create dat
            comment.CreateDateTime = DateTime.Now;

            try
            {
                // add the comment
                _commentRepo.Add(comment);
                return RedirectToAction(nameof(Index), new { postId = comment.PostId });
            }
            catch
            {
                // if exception, show view with current data
                return View(comment);
            }
        }

        // GET: Comment/Edit/5
        public ActionResult Edit(int id)
        {
            //get the comment
            Comment comment = _commentRepo.GetCommentById(id);

            //if null, return not found
            if(comment == null)
            {
                return NotFound();
            }

            //view edit page of comment
            return View(comment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Comment comment)
        {
            try
            {
                // TODO: Add update logic here
                _commentRepo.Edit(comment);
                return RedirectToAction(nameof(Index), new { postId = comment.PostId });
            }
            catch
            {
                return View(comment);
            }
        }

        // GET: Comment/Delete/5
        public ActionResult Delete(int id)
        {
            Comment comment = _commentRepo.GetCommentById(id);
            if(comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int postId, Comment comment)
        {

            try
            {
                // delete comment and redirect to comments index
                _commentRepo.Delete(comment.Id);
                return RedirectToAction(nameof(Index), new { postId = postId });
            }
            catch
            {
                return View(comment);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
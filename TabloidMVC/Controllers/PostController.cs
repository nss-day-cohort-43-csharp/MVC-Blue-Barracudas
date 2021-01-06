using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostTagRepository _postTagRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, IPostTagRepository postTagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _postTagRepository = postTagRepository;
        }

        public IActionResult Index()
        {
            var posts = _postRepository.GetAllPublishedPosts();
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);

            List<PostTag> postTags = _postTagRepository.GetPostTagsbyPostId(id);

            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }

            PostDetailViewModel vm = new PostDetailViewModel
            {
                Post = post,
                PostTags = postTags
            };
            return View(vm);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.Categories = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.Categories = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        //GET:
        public ActionResult Edit(int id)
        {
            Post post = _postRepository.GetPostById(id);

            if (post == null)
            {
                return NotFound();
            }

            List<Category> categories = _categoryRepository.GetAll();

            PostCreateViewModel vm = new PostCreateViewModel()
            {
                Post = post,
                Categories = categories
            };

            return View(vm);
        }

        // POST: Post/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PostCreateViewModel postViewModel)
        {


            try
            {
                _postRepository.UpdatePost(postViewModel.Post);

                return RedirectToAction("Index");
        }
            catch (Exception ex)
            {
                postViewModel.Categories = _categoryRepository.GetAll();
                return View(postViewModel);
            }
        }


        // GET: Post/Delete/5
        public ActionResult Delete(int id)
        {

            Post post = _postRepository.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.DeletePost(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }

        public IActionResult UserPostIndex()
        {
            int currentUserId = GetCurrentUserProfileId();
            var posts = _postRepository.GetCurrentUserPosts(currentUserId);
            return View(posts);
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }



        
    }
}

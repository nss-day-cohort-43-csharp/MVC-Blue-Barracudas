﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        List<Post> GetAllPublishedPosts();
        List<Post> GetCurrentUserPosts(int userProfileId);
        Post GetPublishedPostById(int id);

        Post GetPostById(int id);
        Post GetUserPostById(int id, int userProfileId);

        void UpdatePost(Post post);

        void DeletePost(int id);
    }
}
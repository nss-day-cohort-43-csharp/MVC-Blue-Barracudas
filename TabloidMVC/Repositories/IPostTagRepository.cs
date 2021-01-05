using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Repositories
{
    interface IPostTagRepository
    {
        List<PostTag> GetPostTagsbyPostId(int id);
        void AddTag(PostTag postTag);
    }
}

using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        public List<Comment> GetAllPostComments(int postId);

        public void Delete(int id);
    }
}
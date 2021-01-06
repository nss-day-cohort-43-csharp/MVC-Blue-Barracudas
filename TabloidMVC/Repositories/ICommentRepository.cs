using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ICommentRepository
    {
        public List<Comment> GetAllPostComments(int postId);
        public void Delete(int id);
        public void Add(Comment comment);
        public void Edit(Comment comment);
        public Comment GetCommentById(int id);
    }
}
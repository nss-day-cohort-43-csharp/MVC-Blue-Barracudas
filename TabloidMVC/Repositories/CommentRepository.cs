using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(IConfiguration config) : base(config) { }

        public List<Comment> GetAllPostComments(int postId)
        {
            //create and open a connection
            using (var conn = Connection)
            {
                conn.Open();

                // create a command
                using(var cmd = conn.CreateCommand())
                {
                    // create the sql command
                    cmd.CommandText = @"SELECT c.Id, PostId, UserProfileId, DisplayName, Subject, Content, c.CreateDateTime
                                        FROM Comment c
                                        LEFT JOIN UserProfile u ON u.Id = UserProfileId
                                        WHERE PostId = @postId";
                    cmd.Parameters.AddWithValue("postId", postId);

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Comment> comments = new List<Comment>() { };

                    while(reader.Read())
                    {
                        comments.Add(new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            UserProfile = new UserProfile
                            {
                                DisplayName = reader.GetString(reader.GetOrdinal("DisplayName"))
                            },
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        }
                        );
                    }

                    reader.Close();

                    return comments;
                }
            }
        }
    }
}

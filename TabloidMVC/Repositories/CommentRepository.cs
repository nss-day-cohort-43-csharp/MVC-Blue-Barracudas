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
                                        WHERE PostId = @postId
                                        ORDER BY CreateDateTime DESC";
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

        // remove the comment with the given id
        public void Delete(int id)
        {
            // open a conneciton
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                //start a command
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                            DELETE FROM Comment
                            WHERE Id = @id
                        ";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Comment GetCommentById(int id)
        {
            // start a connection
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                //start a command
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //create and execute command
                    cmd.CommandText = @"
                        SELECT Id, PostId, UserProfileId, [Subject], Content, CreateDateTime
                        FROM Comment
                        WHERE Id = @id
                    ";

                    cmd.Parameters.AddWithValue("@id", id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Comment comment = new Comment
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            UserProfileId = reader.GetInt32(reader.GetOrdinal("UserProfileId")),
                            Subject = reader.GetString(reader.GetOrdinal("Subject")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime"))
                        };

                        reader.Close();
                        return comment;
                    }
                    else
                    {
                        reader.Close();
                        return null;
                    }
                }
            }
        }


        // adds a given comment to the database
        public void Add(Comment comment)
        {
            // create and open a connection
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                //create a command
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    // create the sql command
                    cmd.CommandText = @"INSERT INTO Comment (PostId, UserProfileId, [Subject], Content, CreateDateTime)
                                        VALUES (@postId, @userId, @subject, @content, @date)";
                    cmd.Parameters.AddWithValue("@postId", comment.PostId);
                    cmd.Parameters.AddWithValue("@userId", comment.UserProfileId);
                    cmd.Parameters.AddWithValue("@subject", comment.Subject);
                    cmd.Parameters.AddWithValue("@content", comment.Content);
                    cmd.Parameters.AddWithValue("@date", comment.CreateDateTime);

                    //execute the insert command
                    cmd.ExecuteNonQuery();

                }
            }
        }
    }
}

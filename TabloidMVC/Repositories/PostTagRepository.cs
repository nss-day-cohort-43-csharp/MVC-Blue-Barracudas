using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class PostTagRepository :BaseRepository, IPostTagRepository
    {
        public PostTagRepository(IConfiguration config) : base(config) { }
        public List<PostTag> GetPostTagsbyPostId(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    //get a sorted list of tags
                    cmd.CommandText = @"Select pt.Id, TagId, PostId, t.Name AS TagName
                                        FROM PostTag pt
                                        JOIN Tag t ON t.Id = TagId 
                                        JOIN Post p ON p.Id = PostId 
                                        WHERE @Id = PostId";

                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    //list to store tags
                    List<PostTag> postTags = new List<PostTag>();

                    while (reader.Read())
                    {
                        PostTag postTag = new PostTag
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            TagId =  reader.GetInt32(reader.GetOrdinal("TagId")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId")),
                            Tag = new Tag
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("TagId")),
                                Name = reader.GetString(reader.GetOrdinal("TagName"))
                            }
                        };
                        postTags.Add(postTag);
                    }
                    reader.Close();
                    return postTags;
                }

            }
        }

        //Add new tag
        public void AddTag(PostTag postTag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO PostTag (TagId, PostId)
                        OUTPUT INSERTED.ID
                        VALUES (@tagId, @postId);";

                    cmd.Parameters.AddWithValue("@tagId", postTag.TagId);
                    cmd.Parameters.AddWithValue("@postId", postTag.PostId);
                    int id = (int)cmd.ExecuteScalar();

                    postTag.Id = id;
                }

            }
        }
    }
}

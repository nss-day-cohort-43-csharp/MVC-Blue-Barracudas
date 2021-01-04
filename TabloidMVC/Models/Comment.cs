using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        [Required]
        [StringLength(255, MinimumLength =1)]
        public string Subject { get; set; }

        [Required]
        [MinLength(1)]
        public string Content { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}

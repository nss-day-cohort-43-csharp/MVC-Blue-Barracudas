﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TabloidMVC.Models.ViewModels
{
    public class PostCommentsViewModel
    {
        public Post Post { get; set; }

        public List<Comment> Comments { get; set; }
    }
}

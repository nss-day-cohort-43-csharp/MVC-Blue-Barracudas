﻿using System.Collections.Generic;

namespace TabloidMVC.Models.ViewModels
{
    public class PostCreateViewModel
    {
        public Post Post { get; set; }
        public List<Category> Categories { get; set; }
    }
}

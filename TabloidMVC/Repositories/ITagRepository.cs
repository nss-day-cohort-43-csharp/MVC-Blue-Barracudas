﻿using System.Collections.Generic;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAllTags();
        void AddTag(Tag tag);
        void Edit(Tag tag);
        void Delete(int id);
        Tag GetTagById(int id);
    }
}

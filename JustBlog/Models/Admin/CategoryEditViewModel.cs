using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models.Admin
{
    public class CategoryEditViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Post> IncludedPosts { get; set; }
    }
}
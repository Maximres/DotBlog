using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models.Admin
{
    public class PostsForTag
    {
        public IEnumerable<Post> Posts { get; set; }
    }
}
using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace JustBlog.Models
{
    public class AdminPostsViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
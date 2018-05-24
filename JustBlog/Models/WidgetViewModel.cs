using JustBlog.Core.Abstract;
using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class WidgetViewModel
    {
        public WidgetViewModel(IBlogRepository blog)
        {
            Categories = blog.Categories();
            Tags = blog.Tags();
            LatestPosts = blog.Posts(1, 10);
        }

        public IEnumerable<Tag> Tags { get; private set; }
        public IEnumerable<Category> Categories { get; private set; }
        public IEnumerable<Post> LatestPosts { get; private set; }
    }
}
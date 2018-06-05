using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models.Admin
{
    public class TagEditViewModel
    {
        public Tag Tag { get; set; }
        public bool IncludePosts { get; set; }
    }
}
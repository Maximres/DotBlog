using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustBlog.Models.Admin
{
    public class PostEditViewModel
    {
        public Post Post { get; set; }
        public int CategoryListId { get; set; }
        public List<SelectListItem> CategoriesList { get; set; }
        public IHtmlString TagsListJson { get; set; }
    }
}

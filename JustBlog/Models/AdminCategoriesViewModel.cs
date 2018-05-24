using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class AdminCategoriesViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public string Tittle { get; set; }
    }
}
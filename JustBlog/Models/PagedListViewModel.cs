using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class PagedListViewModel
    {
        public PagingInfo PagingInfo { get; set; }
        public ListViewModel ViewModel { get; set; }
    }
}
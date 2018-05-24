using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustBlog.Core.Objects;

namespace JustBlog.Core.Abstract
{
    public interface IBlogRepository : IPostRepository, ITagRepository, ICategoryRepository
    {
       
    }
}

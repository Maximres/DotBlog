using JustBlog.Core.Abstract;
using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JustBlog.Models
{
    public class ListViewModel
    {
        /// <summary>
        /// Возвращает последнии записи начиная со страницы и в определенном количестве
        /// </summary>
        /// <param name="blog">Репозиторий блога</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pCount">Количество записей</param>
        public ListViewModel(IBlogRepository blog, int page, int pCount)
        {
            Posts = blog.Posts(page, pCount);
            TotalPosts = blog.TotalPosts();
        }

        /// <summary>
        /// Возвращает записи в зависимости от критерия, по определенному значению <paramref name="slug"/> с определенной страницы <paramref name="page"/> в количестве <paramref name="pCount"/>.
        /// </summary>
        /// <param name="blog">Репозиторий блога</param>
        /// <param name="criterion">Критерий выбора записей</param>
        /// <param name="slug">URL идентификатор</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pCount">Количество записей</param>
        public ListViewModel(IBlogRepository blog,
            Criterion criterion, string slug,  int page, int pCount)
        {
            switch (criterion)
            {
                case Criterion.Tag:
                    Posts = blog.PostsForTag(slug, page, pCount);
                    TotalPosts = blog.TotalPostsForTag(slug);
                    Tag = blog.Tag(slug);
                    break;
                case Criterion.Category:
                    Posts = blog.PostsForCategory(slug, page, pCount);
                    TotalPosts = blog.TotalPostsForCategory(slug);
                    Category = blog.Category(slug);
                    break;
                case Criterion.Search:
                    Posts = blog.PostsForSearch(slug, page, pCount);
                    TotalPosts = blog.TotalPostsForSearch(slug);
                    Search = slug;
                    break;
                default:
                    Posts = blog.Posts(page, pCount);
                    TotalPosts = blog.TotalPosts();
                    break;
                
            }
        }

        public IEnumerable<Post> Posts { get; private set; }
        public int TotalPosts { get; private set; }
        public Category Category { get; private set; }
        public Tag Tag { get; private set; }
        public string Search { get; private set; }
    }

    /// <summary>
    /// Критерий выбора записей
    /// </summary>
    public enum Criterion
    {
        Tag,
        Category,
        Search
    }
}
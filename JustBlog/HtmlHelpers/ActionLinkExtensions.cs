using JustBlog.Core.Objects;
using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace JustBlog.HtmlHelpers
{
    public static class ActionLinkExtensions
    {
        /// <summary>
        /// Создает ссылку на пост
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="post"></param>
        /// <returns></returns>
        public static MvcHtmlString PostLink(this HtmlHelper helper, Post post)
        {
            return helper.ActionLink(post.Title, "Post", "Blog",
                new
                {
                    year = post.PostedOn.Year,
                    month = post.PostedOn.Month,
                    title = post.UrlSlug
                },
                new
                {
                    title = post.Title
                });
        }

        /// <summary>
        /// Создает ссылку для поиска постов по категории
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public static MvcHtmlString CategoryLink(this HtmlHelper helper,
            Category category)
        {
            return helper.ActionLink(category.Name, "Category", "Blog",
                new
                {
                    category = category.UrlSlug
                },
                new
                {
                    title = String.Format("See all posts in {0}", category.Name)
                });
        }

        /// <summary>
        /// Создает ссылку для поиска постов по тегу
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag)
        {
            return helper.ActionLink(tag.Name, "Tag", "Blog", 
                new { tag = tag.UrlSlug },
                new
                {
                    title = String.Format("See all posts in {0}", tag.Name),
                    @class = "badge badge-primary"
                });
        }

        /// <summary>
        /// Создает ссылку для поиска постов по тегу с добавлением аттрибута "class"
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="tag"></param>
        /// <param name="htmlClasses"></param>
        /// <returns></returns>
        public static MvcHtmlString TagLink(this HtmlHelper helper, Tag tag, string htmlClasses)
        {
            return helper.ActionLink(tag.Name, "Tag", "Blog",
                new { tag = tag.UrlSlug },
                new
                {
                    title = String.Format("See all posts in {0}", tag.Name),
                    @class = "badge badge-primary " + htmlClasses,
                    
                });
        }
    }
}
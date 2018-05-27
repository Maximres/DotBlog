using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBlog.Core.Abstract
{
    public interface IPostRepository
    {
        /// <summary>
        /// Возвращает все посты
        /// </summary>
        /// <returns></returns>
        IEnumerable<Post> Posts(bool onlyPublished);

        /// <summary>
        /// Удаляет пост по Id
        /// </summary>
        /// <param name="id"></param>
        Post DeletePost(int id);

        /// <summary>
        /// Получаем пост по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Post Post(int id, bool onlyPublished);

        /// <summary>
        /// Возвращает определенный пост, в зависимости от его имени в строке запроса <paramref name="titleSlug"/>
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="titleSlug"></param>
        /// <returns></returns>
        Post Post(int year, int month, string titleSlug);

        /// <summary>
        /// Добавляет/изменяет в БД пост
        /// </summary>
        /// <param name="post"></param>
        void SavePost(Post post);

        /// <summary>
        /// Возвращает посты начиная с номера <paramref name="pageNo"/> в количестве <paramref name="pageSize"/>
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Post> Posts(int pageNo, int pageSize);

        /// <summary>
        /// Возвращает посты, отсортированные определенным образом <paramref name="sortByAscending"/> и по определенному свойству <paramref name="sortColumn"/>
        /// </summary>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <param name="sortByAscending"></param>
        /// <param name="sortColumn">Свойства класса <see cref="Post"/></param>
        /// <returns></returns>
        IEnumerable<Post> Posts(int pageNo, int pageSize, bool sortByAscending, SortOnColumnEnum sortColumn = SortOnColumnEnum.PostedOn);

        /// <summary>
        /// Возвращает количество постов
        /// </summary>
        /// <returns></returns>
        int TotalPosts(bool onlyPublished = true);

        /// <summary>
        /// Возвращает все посты, которые соответсвуют категории <paramref name="categorySlug"/>
        /// </summary>
        /// <param name="categorySlug"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);

        /// <summary>
        /// Возвращает количество постов по определенной категории <paramref name="categorySlug"/> в строке запроса 
        /// </summary>
        /// <param name="categorySlug"></param>
        /// <returns></returns>
        int TotalPostsForCategory(string categorySlug);

        /// <summary>
        /// Возвращает все посты, соответсвующие определенному тегу <paramref name="tagSlug"/>
        /// </summary>
        /// <param name="tagSlug"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);

        /// <summary>
        /// Возвращает количество постов по определенному тегу <paramref name="tagSlug"/> в строке запроса 
        /// </summary>
        /// <param name="tagSlug"></param>
        /// <returns></returns>
        int TotalPostsForTag(string tagSlug);

        /// <summary>
        /// Возвращает все посты, которые соответсвуют поисковому запросу <paramref name="search"/>
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNo"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<Post> PostsForSearch(string search, int pageNo, int pageSize);

        /// <summary>
        /// Возвращает количество постов, которые соответсвуют поисковому запросу <paramref name="search"/>
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        int TotalPostsForSearch(string search);

        /// <summary>
        /// Возвращает истину, если UrlSlug уже существует.
        /// </summary>
        /// <param name="slug"></param>
        /// <returns></returns>
        Post PostWithSameSlug(string slug);
    }
}

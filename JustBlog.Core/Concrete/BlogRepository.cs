using JustBlog.Core.Abstract;
using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Reflection;

namespace JustBlog.Core.Concrete
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository() : this(new BlogContext()) { }

        public BlogRepository(BlogContext context)
        {
            _context = context;
        }

        #region Posts

        /// <summary>
        /// Возвращает пост определенного дня и соответсвующий <paramref name="titleSlug"/>
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="titleSlug"></param>
        /// <returns></returns>
        public Post Post(int year, int month, string titleSlug)
        {
            //var posts = _context.Posts;
            //posts.Include(s => s.Category).Include(s => s.Tags);
            //return posts.
            return _context.Posts.
                Include(i => i.Category).
                Include(i => i.Tags).
                Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug)).FirstOrDefault();
        }

        public Post Post(int id, bool onlyPublished = true)
        {
            return _context.Posts.
                    Include(i => i.Category).
                    Include(i => i.Tags).
                    Where(i => !onlyPublished || i.Published == true).
                    FirstOrDefault(s => s.Id == id);

        }

        public IEnumerable<Post> Posts(int pageNo, int pageSize)
        {
            return _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        Where(p => p.Published).
                        OrderByDescending(o => o.PostedOn).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
        }

        public IEnumerable<Post> Posts(int pageNo, int pageSize, bool sortByAscending, SortOnColumnEnum sortColumn = SortOnColumnEnum.PostedOn)
        {
            var stringEnumType = Enum.GetName(typeof(SortOnColumnEnum), sortColumn);
            IList<Post> posts;
            switch (stringEnumType)
            {
                case "Title":
                    posts = _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        OrderOption(o => o.Title, sortByAscending).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
                    break;
                case "Published":
                    posts = _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        OrderOption(o => o.Published, sortByAscending).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
                    break;
                case "Modified":
                    posts = _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        OrderOption(o => o.Modified, sortByAscending).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
                    break;
                case "Category":
                    posts = _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        OrderOption(o => o.Modified, sortByAscending).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
                    break;
                case "PostedOn":
                default:
                    posts = _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        OrderOption(o => o.PostedOn, sortByAscending).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
                    break;
            }

            return posts;
        }

        public IEnumerable<Post> Posts(bool onlyPublished = true)
        {
            //if (onlyPublished)
            //{
            //    return _context.Posts.
            //                Include(c => c.Category).
            //                Include(c => c.Tags).
            //                Where(p => p.Published == true).
            //                ToList();
            //}
            return _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        Where(p => !onlyPublished || p.Published == true).
                        ToList();
        }

        public IEnumerable<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            return _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug)).
                        OrderByDescending(o => o.PostedOn).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
        }

        public IEnumerable<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            return _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        Where(p => p.Published &&
                                  (p.Tags.Any(t => t.Name.Equals(search)) || p.Title.Contains(search) || p.ShortDescription.Contains(search) || p.Category.Name.Contains(search))).
                        OrderByDescending(o => o.PostedOn).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
        }

        public IEnumerable<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            return _context.Posts.
                        Include(c => c.Category).
                        Include(c => c.Tags).
                        Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug))).
                        OrderByDescending(o => o.PostedOn).
                        Skip((pageNo - 1) * pageSize).
                        Take(pageSize).
                        ToList();
        }

        /// <summary>
        /// Добавляет/изменяет в БД пост
        /// </summary>
        /// <param name="post"></param>
        public void SavePost(Post post)
        {
            try
            {

                if (post.Id == 0)
                {
                    _context.Posts.Add(post);
                }
                else
                {
                    var dbEntry = _context.Posts.Include(t => t.Tags).Include(c => c.Category).FirstOrDefault(s => s.Id == post.Id);

                    if (dbEntry != null)
                    {
                        if (post.Tags == null || post.Tags.Count <= 0)
                        {
                            dbEntry.Tags.Clear();
                        }
                        else
                        {
                            dbEntry.Tags.Clear();
                            foreach (var item in post.Tags)
                            {
                                dbEntry.Tags.Add(item);
                            }
                        }

                        _context.Entry(dbEntry).CurrentValues.SetValues(post);
                        //dbEntry.Category = post.Category;
                        //dbEntry.CategoryId = post.CategoryId;
                        //dbEntry.Description = post.Description;
                        //dbEntry.Meta = post.Meta;
                        //dbEntry.Modified = post.Modified;
                        //dbEntry.PostedOn = post.PostedOn;
                        //dbEntry.Published = post.Published;
                        //dbEntry.ShortDescription = post.ShortDescription;
                        //dbEntry.Tags = post.Tags;
                        //dbEntry.Title = post.Title;
                        //dbEntry.UrlSlug = post.UrlSlug;

                    }

                }
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public int TotalPosts(bool onlyPublished = true)
        {
            return _context.Posts.Where(p => !onlyPublished || p.Published == true).Count();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _context.Posts.Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug)).Count();
        }

        public int TotalPostsForSearch(string search)
        {
            return _context.Posts.Where(p => p.Published &&
                                  (p.Tags.Any(t => t.Name.Equals(search)) ||
                                  p.Title.Contains(search) ||
                                  p.ShortDescription.Contains(search) ||
                                  p.Category.Name.Contains(search))).Count();
        }

        public int TotalPostsForTag(string tagSlug)
        {
            return _context.Posts.Where(t => t.Tags.Any(u => u.UrlSlug.Equals(tagSlug))).Count();
        }

        public Post DeletePost(int id)
        {
            var post = _context.Posts.Find(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }
            return post;
        }

        #endregion

        #region Tags

        /// <summary>
        /// Добавляет/изменяет в БД тег
        /// </summary>
        /// <param name="tag"></param>
        public void SaveTag(Tag tag)
        {
            if (tag.Id == 0)
            {
                _context.Tags.Add(tag);
            }
            else
            {
                var dbEntry = _context.Tags.Find(tag);
                if (dbEntry != null)
                {
                    dbEntry.Description = tag.Description;
                    dbEntry.Name = tag.Name;
                    dbEntry.Posts = tag.Posts;
                    dbEntry.UrlSlug = tag.UrlSlug;
                }
            }
            _context.SaveChanges();
        }

        /// <summary>
        /// Возвращает тег <paramref name="tagSlug"/>
        /// </summary>
        /// <param name="tagSlug"></param>
        /// <returns></returns>
        public Tag Tag(string tagSlug)
        {
            return _context.Tags.FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }

        public IEnumerable<Tag> Tags()
        {
            return _context.
                Tags.
                Include(p => p.Posts).
                OrderBy(n => n.Name).ToList();
        }

        /// <summary>
        /// Возвращает теги, название который представлены в <paramref name="names"/>
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        public IEnumerable<Tag> Tags(params string[] names)
        {
            var tags = _context.Tags.OrderBy(n => n.Name).ToList();

            //получает все 
            var similarTags = tags.Join(names,
                                t => t.Name,
                                n => n,
                                (tg, str) => new Tag
                                {
                                    Id = tg.Id,
                                    Description = tg.Description,
                                    Name = str,
                                    Posts = tg.Posts,
                                    UrlSlug = tg.UrlSlug
                                }).ToList();

            //TODO: протестировать время исполнения
            //извлекает сущетсвующие теги из БД, чтобы избежать их дубликации
            var existingTags = _context.Tags.ToList().Where(s => s.NameSameAs(similarTags)).ToList();

            return existingTags;
        }

        public Tag DeleteTag(int id)
        {
            var tag = _context.Tags.Find(id);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
                _context.SaveChanges();
            }
            return tag;
        }

        #endregion

        #region Categories

        /// <summary>
        /// Возвращает все категори
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Category> Categories()
        {
            return _context.Caregories.
                OrderBy(p => p.Name).
                ToList();
        }

        /// <summary>
        /// Возвращает категорию в зависимости от <paramref name="categorySlug"/>
        /// </summary>
        /// <param name="categorySlug"></param>
        /// <returns></returns>
        public Category Category(string categorySlug)
        {
            return _context.Caregories.FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }

        /// <summary>
        /// Вовзращает определенную категории по <paramref name="id"/>
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <returns></returns>
        public Category Category(int id)
        {
            return _context.Caregories.FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Добавляет/Изменяет в БД категорию
        /// </summary>
        /// <param name="category"></param>
        public void SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                _context.Caregories.Add(category);
            }
            else
            {
                var dbEntry = _context.Caregories.Find(category);
                if (dbEntry != null)
                {
                    dbEntry.Name = category.Name;
                    dbEntry.Posts = category.Posts;
                    dbEntry.UrlSlug = category.UrlSlug;
                    dbEntry.Description = category.Description;
                }
            }
            _context.SaveChanges();
        }

        public Category DeleteCategory(int id)
        {
            var category = _context.Caregories.Find(id);
            if (category != null)
            {
                _context.Caregories.Remove(category);
                _context.SaveChanges();
            }
            return category;
        }

        #endregion

        #region helpers
        private static IList<string> PropertyNames<T>(T objectToCheck)
        {
            return
                objectToCheck.GetType()
                             .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                             .Select(x => x.Name)
                             .ToList();
        }

        public Post PostWithSameSlug(string slug)
        {
            return _context.Posts.Where(s => s.UrlSlug.ToLower() == slug.Trim().ToLower()).FirstOrDefault();
        }
        #endregion
    }

    static class IQueryableExtention
    {
        internal static IOrderedQueryable<Post> OrderOption<Post, TKey>(this IQueryable<Post> queryable,
                                                                            Expression<Func<Post, TKey>> expression,
                                                                            bool sortByAscending)
        {
            return sortByAscending ? queryable.OrderBy(expression) : queryable.OrderByDescending(expression);
        }


        internal static bool NameSameAs(this Tag query, ICollection<Tag> tags)
        {
            //Tag temp = null;
            if (tags == null)
            {
                return false;
            }

            foreach (var tag in tags)
            {
                if (tag.Name == query.Name)
                {
                    tags.Remove(tag);
                    return true;
                }
            }
            return false;
        }
    }
}

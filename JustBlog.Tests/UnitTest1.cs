using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using JustBlog.Core.Abstract;
using JustBlog.Core.Concrete;
using JustBlog.Core.Objects;
using JustBlog.Models;
using JustBlog.Controllers;
using JustBlog.Infrastructure;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Threading;

namespace JustBlog.Tests
{
    [TestClass]
    public class BlogUnitTest
    {
        /// <summary>
        /// Настраивает и возвращает <paramref name="BlogContext"/> 
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="posts"></param>
        /// <param name="caregories"></param>
        /// <returns></returns>
        Mock<BlogContext> MockDbContext(Mock<DbSet<Tag>> tags, Mock<DbSet<Post>> posts, Mock<DbSet<Category>> caregories)
        {
            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(s => s.Tags).Returns(tags.Object);
            mockContext.Setup(s => s.Caregories).Returns(caregories.Object);
            mockContext.Setup(s => s.Posts).Returns(posts.Object);

            return mockContext;
        }

        /// <summary>
        /// Возвращает проинициализированный <paramref name="BlogContext"/> 
        /// </summary>
        /// <returns></returns>
        Mock<BlogContext> MockDbContext()
        {

            List<Tag> tags = null;

            //List<Tag> tags = null;
            List<Category> category = null;
            List<Post> posts = new List<Post>{
                new Post {Id = 1, Description = "Lorem", PostedOn = DateTime.Now, Published = false, UrlSlug = "Lorem", CategoryId = 1, },
                new Post {Id = 2, Description = "Lorem1", PostedOn = DateTime.Now, Published = true, UrlSlug = "Lorem1",  CategoryId = 1, },
                new Post {Id = 3, Description = "Lorem2", PostedOn = DateTime.Now, Published = false, UrlSlug = "Lorem2",  CategoryId = 1, },
                new Post {Id = 4, Description = "Lorem3", PostedOn = DateTime.Now, Published = false, UrlSlug = "Lorem3",  CategoryId = 1, },
            };

            tags = new List<Tag>
            {
                new Tag {Name = "asp", Id = 1, Posts = posts.ToList()},
                new Tag {Name = "c#", Id = 2, Posts = posts.ToList()},
                new Tag {Name = "c++", Id = 3, Posts = posts.ToList()},
                new Tag {Name = "lisp", Id = 4, Posts = posts.ToList()},
                new Tag {Name = "sql", Id = 5, Posts = posts.ToList()},
            };

            category = new List<Category>
            {
                new Category { Id = 1, Posts = posts.ToList()},
                new Category {Id = 2, Posts = posts.ToList() }
            };

            foreach (var item in posts)
            {
                item.Category = category.First() ?? new Category { Id = 1, Posts = posts.ToList() };
                item.Tags = tags;
            }

            var _context = new Mock<BlogContext>();
            _context.Setup(s => s.Caregories).Returns(category.GetMockSet().Object);
            _context.Setup(s => s.Tags).Returns(tags.GetMockSet().Object);
            _context.Setup(s => s.Posts).Returns(posts.GetMockSet().Object);

            return _context;

        }

        [TestMethod]
        public void Can_Get_Valid_Tags_From_String()
        {
            var data = new List<Tag>
            {
                new Tag {Name = "asp", Id = 1},
                new Tag {Name = "c#", Id = 2},
                new Tag {Name = "c++", Id = 3},
                new Tag {Name = "lisp", Id = 4},
                new Tag {Name = "sql", Id = 5},
            };

            var tags = new[]
            {
                "asp", "sql", "js", "asp", "js"
            };

            var tagsEmpty = new string[] 
            {
            };

            var mockContext = new Mock<BlogContext>();
            mockContext.Setup(s => s.Tags).Returns(data.GetMockSet().Object);
            var rep = new BlogRepository(mockContext.Object);

            var result = rep.Tags(tags);
            var result2 = rep.Tags(tags);
            var emptyRes = rep.Tags(tagsEmpty);

            Assert.IsNotNull(result, "result is null");
            Assert.AreEqual(2, result.Count(), "results count != 2");
            Assert.IsNotNull(result2, "result2 iss null");
            Assert.IsInstanceOfType(emptyRes,typeof(IEnumerable<Tag>), "emptyRes isnt IEnumerable<Tag>");
            Assert.IsNull(emptyRes.FirstOrDefault(), "emptyRes isnt Null");
        }

        [TestMethod]
        public void Can_Get_Posts()
        {
            
            //A
            var _context = MockDbContext();
            var target = new BlogRepository(_context.Object);

            //A
            var total = target.TotalPosts(false);

            var tags = target.Tags();
            var cats = target.Categories();

            //A
            Assert.AreEqual(4, total);
            Assert.IsNotNull(tags, "tags are null");
            Assert.IsNotNull(cats, "categories are null");


        }

        [TestMethod]
        public void Can_Save_Post()
        {
            var _context = MockDbContext();
            //var _contextObsolete = MockDbContext();
            var target = new BlogRepository(_context.Object);
            Post post = new Post { Id = 0, Description = "LoremNew", PostedOn = DateTime.Now, Published = false, UrlSlug = "added" };

            int postsCountBefore = target.TotalPosts(false);
            target.SavePost(post);
            int postsCountAfter = target.TotalPosts(false);

            Post returnedPost = target.Post(2018, 5, "added");

            Assert.AreNotEqual(postsCountAfter, postsCountBefore, "post added");
            Assert.IsNotNull(returnedPost, "post exists");
        }

        [TestMethod]
        public void Can_Change_Post()
        {
            /*
             * existing post in context
             * new Post {Id = 2, Description = "Lorem1", PostedOn = DateTime.Now, Published = true, UrlSlug = "Lorem1",  CategoryId = 1, },
             */
            var _context = MockDbContext();
            var target = new BlogRepository(_context.Object);
            var _post = new Post { Id = 2, Description = "NewDescription", PostedOn = DateTime.Now, Published = true, UrlSlug = "someSlug", CategoryId = 1, };

            int postsCountBefore = target.TotalPosts(false);
            target.SavePost(_post);
            int postsCountAfter = target.TotalPosts(false);
            Post publishedPost = target.Post(2, false);
            Post unpublishedPost = target.Post(1, false);
            Post unpublishedPostWhenPublishedFilterOn = target.Post(1, true);
            Post nonexistingPost = target.Post(1000, false);

            Assert.IsNotNull(publishedPost, "result is null");
            Assert.IsNotNull(unpublishedPost, "result2 in null");
            Assert.IsNull(unpublishedPostWhenPublishedFilterOn, "aint null");
            Assert.IsNull(nonexistingPost, "res2 isnt null");
            Assert.AreEqual(postsCountBefore, postsCountAfter, "aint added to list");

        }

    }

    public static class ExtClass
    {
        /// <summary>
        /// Mocks any DbSet with implementation some collection's methods 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Mock<DbSet<T>> GetMockSet<T>(this List<T> list) where T : class
        {
            var queryable = list.AsQueryable();
            var mockList = new Mock<DbSet<T>>(MockBehavior.Loose);

            mockList.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockList.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockList.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockList.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            mockList.Setup(m => m.Include(It.IsAny<string>())).Returns(mockList.Object);
            //mockList.Setup(m => m.Local).Returns(list);
            mockList.Setup(m => m.Add(It.IsAny<T>())).Returns((T a) => { list.Add(a); return a; });
            mockList.Setup(m => m.AddRange(It.IsAny<IEnumerable<T>>())).Returns((IEnumerable<T> a) => { foreach (var item in a.ToArray()) list.Add(item); return a; });
            mockList.Setup(m => m.Remove(It.IsAny<T>())).Returns((T a) => { list.Remove(a); return a; });
            mockList.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<T>>())).Returns((IEnumerable<T> a) => { foreach (var item in a.ToArray()) list.Remove(item); return a; });

            return mockList;
        }
    }
}

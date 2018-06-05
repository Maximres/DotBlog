using JustBlog.Core.Abstract;
using JustBlog.Core.Objects;
using JustBlog.Models;
using JustBlog.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using System.Web.Mvc;
using System.Web.Caching;
using Newtonsoft.Json;
using System.IO;

namespace JustBlog.Controllers
{
    /// <summary>
    /// Управление блогом
    /// </summary>
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private static Random random = new Random();
        private static readonly object locker = new object();
        private const int PAGESIZE = 15;
        private readonly IBlogRepository _repository;

        public AdminController(IBlogRepository blog)
        {
            _repository = blog;
        }

        /// <summary>
        /// Возвращает представление для управления блогом
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTagsData(int page = 1)
        {
            return PartialView(_repository.Posts((page > 0 && page <= _repository.TotalPosts()) ? page : page, PAGESIZE));
        }

        private static int GetRandomNumber(int min, int max = 10000)
        {
            lock (locker)
            {
                return random.Next(min, max);
            }
        }
    }
}
using JustBlog.Core.Abstract;
using JustBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustBlog.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public ActionResult Posts(int page = 1)
        {
            ViewBag.Title = "Latest Posts";

            return View("List", new ListViewModel(_blogRepository, page, 10));
        }

        // TODO: get the posts for the category and return the view.
        public ActionResult Category(string category, int page = 1)
        {
            var viewModel = new ListViewModel(_blogRepository, Criterion.Category, category, page, 10);
            if (viewModel.Category == null)
            {
                return HttpNotFound("Категория не найдена");
            }
            ViewBag.Title = $"Последние посты с категорией '{viewModel.Category.Name}'";
            return View("List", viewModel);
        }

        // TODO: get the posts for the tag and return the view.
        public ActionResult Tag(string tag, int page = 1)
        {
            var tagViewModel = new ListViewModel(_blogRepository, Criterion.Tag, tag, page, 10);
            if (tagViewModel.Tag == null)
            {
                return HttpNotFound("Записи с данным тегом не найдены");
            }
            ViewBag.Title = $"Последние посты с тегом {tagViewModel.Tag.Name}";
            return View("List", tagViewModel);
        }

        //TODO: implement searching
        public ViewResult Search(string search, int page = 1)
        {
            ViewBag.Title = $"Результат поиска по заспросу {search}";

            var viewModel = new ListViewModel(_blogRepository, Criterion.Search, search, page, 10);
            return View("List", viewModel);
        }

        //TODO: expose a post
        public ViewResult Post(int year, int month, string title)
        {
            var post = _blogRepository.Post(year, month, title);

            if (post == null)
                throw new HttpException(404, "Post not found");

            if (post.Published == false && User.Identity.IsAuthenticated == false)
                throw new HttpException(401, "The post is not published");

            return View(post);
        }

        //TODO: represents a side bar
        [ChildActionOnly]
        public PartialViewResult Sidebars()
        {
            var widgetViewModel = new WidgetViewModel(_blogRepository);
            return PartialView("_Sidebars", widgetViewModel);
        }
    }
}
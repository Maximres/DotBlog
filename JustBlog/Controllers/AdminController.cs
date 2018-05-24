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
    [Authorize(Roles = "admin")]

    //[RouteArea("Admin")]
    //[RoutePrefix("Panel")]
    //[Route("action = Index")]
    public class AdminController : Controller
    {
        private const int PAGESIZE = 15;
        private readonly IBlogRepository _repository;

        public AdminController(IBlogRepository blog)
        {
            _repository = blog;
        }

        // GET: Admin
        // TODO: return start data to init admin tables
        //[Route("Index")]
        public ActionResult Index()
        {
            //AdminPostsViewModel model = new AdminPostsViewModel()
            //{
            //    PagingInfo = new PagingInfo
            //    {
            //        CurrentPage = page,
            //        ItemsPerPage = 15,
            //        TotalItems = _repository.TotalPosts()
            //    },
            //    Posts = _repository.Posts(page, 15, true)
            //};
            return View();
        }


        //[Route("{id:int}", Name = "EditPost")]
        [HttpGet]
        //[OutputCache()]
        public ActionResult EditPost(int id)
        {
            var post = _repository.Posts(false).FirstOrDefault(p => p.Id == id);
            var tags = _repository.Tags();
            var categories = _repository.Categories();

            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var cat in categories)
            {
                items.Add(new SelectListItem
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                    Selected = cat.Id == post.Category.Id ? true : false
                }
                );
            }
            if (post != null)
            {
                PostEditViewModel model = new PostEditViewModel
                {
                    Post = post,

                    //доступные категори
                    CategoriesList = items,
                    //new SelectList(
                    //    categories,
                    //    "Id",
                    //    "Name",
                    //    post.Category.Id),

                    //доступные теги
                    TagsListJson = new HtmlString(JsonConvert.SerializeObject(tags.Select(s => s.Name).ToArray()))

                };

                var _tagsValue = String.Join(",", post.Tags.Select(n => n.Name));
                ViewData["tagsValue"] = new HtmlString(_tagsValue);

                HttpRuntime.Cache["CategorySelectList"] = model.CategoriesList;

                return View(model);
            }

            return View("EditPost", new Post());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(PostEditViewModel postEditViewModel, string tags)
        {
            //var tags1 = Request.Form.GetValues("tags");
            //TODO: удаляет категорию из проверки,
            //чтобы позже добавить в зависимости от CategoryListId
            ModelState.Remove("Post.Category");

            var tagsArray = tags.Split(',');
            if (ModelState.IsValid)
            {
                var _categoryId = postEditViewModel.CategoryListId;
                var _category = _repository.Category(_categoryId);
                var _post = postEditViewModel.Post;
                var _tags = tagsArray != null ? _repository.Tags(tagsArray).ToList() : new List<Tag>();
                _post.Tags = _tags;
                                        var __tags = _repository.Tags();
                _post.Category = _category;
                _post.CategoryId = _categoryId;
                //TODO: unroll this command below when post is valid
                                        _repository.SavePost(_post);
                                        __tags = _repository.Tags();
                var postss = _repository.Posts(false);
                TempData["message"] = string.Format("{0} has been saved", postEditViewModel.Post.Title);

                return Json(new { success = true, responseText = "Your message successfuly sent!" }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            //return View(postEditViewModel);
            return Json(new { success = false, responseText = " The attached file is not supported." }, JsonRequestBehavior.AllowGet);
        }

        //[Route("CreatePost")]
        public ActionResult CreatePost()
        {
            return View("EditPost", new Post());
        }

        //TODO: implenet 3 actions that serves data for post/tag/category panel
        //[Route("Posts/{page:int?}")]
        public ActionResult GetPostsData(string col, bool asc = false, int page = 1)
        {
            return PartialView(_repository.Posts((page > 0 && page <= _repository.TotalPosts()) ? page : page, PAGESIZE, asc, col));
        }

        //[Route("Categories/{page:int?}")]
        public ActionResult GetCategoriesData(int page = 1)
        {
            ViewBag.Title = "Categories";
            return PartialView(_repository.Categories());
        }

        //TODO: handle ajax form post
        [HttpPost]
        //[Route("DeleteCategory/{id}", Name = "DeleteCat")]
        public ActionResult DeleteCategory(int id)
        {
            //var cat = _repository.DeleteCategory(id);
            //if (cat != null)
            //{
            //    ViewData["message"] = $"Категория '{cat.Name}' удалена";
            //}
            return PartialView("GetCategoriesData", _repository.Categories());
        }

        //[Route("Tags/{page:int?}")]
        public ActionResult GetTagsData(int page = 1)
        {
            return PartialView(_repository.Posts((page > 0 && page <= _repository.TotalPosts()) ? page : page, PAGESIZE));
        }
    }
}
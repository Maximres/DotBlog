using JustBlog.Core.Abstract;
using JustBlog.Core.Objects;
using JustBlog.Models.Admin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using X.PagedList;
using X.PagedList.Mvc.Common;
using X.PagedList.Mvc;
using System.Web.Mvc;

namespace JustBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class PostController : Controller
    {
        readonly IBlogRepository _repository;
        private const int PAGESIZE = 15;

        public PostController(IBlogRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        //[OutputCache()]
        public ActionResult EditPost(int id)
        {
            var post = _repository.Posts(false).FirstOrDefault(p => p.Id == id) ?? new Post() {PostedOn = DateTime.Now, Modified = DateTime.Now, Published = true};
            var tags = _repository.Tags();
            var categories = _repository.Categories();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var cat in categories)
            {
                items.Add(new SelectListItem
                {
                    Text = cat.Name,
                    Value = cat.Id.ToString(),
                    Selected = cat.Id == post?.Category?.Id ? true : false
                }
                );
            }
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

            var _tagsValue = String.Join(",", post?.Tags?.Select(n => n.Name));
            ViewData["tagsValue"] = new HtmlString(_tagsValue);
            HttpRuntime.Cache["CategorySelectList"] = items;

            return PartialView("~/Views/Admin/EditPost.cshtml", model);
        }

        public ActionResult DeletePost(int id)
        {
            var post = _repository.Post(id, false);
            if (post != null)
            {
                Post deletedPost = _repository.DeletePost(id);
            }
            var list = _repository.Posts(1, PAGESIZE, false, SortOnColumnEnum.PostedOn);
            return PartialView("~/Views/Admin/GetPostsData.cshtml", list);
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
            var slugExists = _repository.PostWithSameSlug(postEditViewModel.Post.UrlSlug);
            if (slugExists != null && slugExists.Id != postEditViewModel.Post.Id)
            {
                ModelState.AddModelError(String.Empty, "Slug уже существует");
                return Json(new { success = false, responseText = "Точно такой же Slug уже существует" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                var _categoryId = postEditViewModel.CategoryListId;
                var _category = _repository.Category(_categoryId);
                var _post = postEditViewModel.Post;
                var _tags = tagsArray != null ? _repository.Tags(tagsArray).ToList() : new List<Tag>();
                _post.Tags = _tags;
                _post.Category = _category;
                _post.CategoryId = _categoryId;
                //_post.UrlSlug = _post.UrlSlug.ToLower();
                //TODO: unroll this command below when post is valid
                _repository.SavePost(_post);

                TempData["message"] = string.Format("{0} has been saved", postEditViewModel.Post.Title);

                return Json(new { success = true, responseText = "Успех!" }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index");
            }
            //return View(postEditViewModel);
            return Json(new { success = false, responseText = "Неверный ввод" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreatePost()
        {
            return View("EditPost", new Post());
        }

        public ActionResult GetPostsData(bool asc = false, int page = 1, string col = "PostedOn")
        {
            var parsedColumn = Enum.TryParse(col, out SortOnColumnEnum columnEnum);
            //var page = (page > 0 && page <= _repository.TotalPosts()) ? page : page;
            var list = _repository.Posts(page, PAGESIZE, false, columnEnum);
            return PartialView("~/Views/Admin/GetPostsData.cshtml", list);
        }
    }
}
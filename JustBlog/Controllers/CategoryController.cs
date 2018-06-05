using JustBlog.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustBlog.Core.Objects;
using JustBlog.Models.Admin;

namespace JustBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private const int PAGESIZE = 5;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPostRepository _postRepository;


        public CategoryController(ICategoryRepository categoryRepository, IPostRepository postRepository)
        {
            _categoryRepository = categoryRepository;
            _postRepository = postRepository;
        }

        public ActionResult GetCategoriesData(int page = 1)
        {
            ViewBag.Title = "Categories";
            return PartialView("~/Views/Category/_GetCategoriesData.cshtml", _categoryRepository.Categories());
        }

        [HttpGet]
        public ActionResult EditCategory(int id = 0, int page = 1)
        {
            var _category = _categoryRepository.Category(id);
            CategoryEditViewModel model;
            if (_category != null)
            {
                var _posts = _postRepository.PostsForCategory(_category.UrlSlug, page, PAGESIZE);
                model = new CategoryEditViewModel
                {
                    Category = _category,
                    IncludedPosts = _posts
                };

            }
            else
            {
                var newCategory = new Category { Id = 0 };
                model = new CategoryEditViewModel
                {
                    Category = newCategory,
                    IncludedPosts = null
                };
            }
            return PartialView("_EditCategory", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult EditCategory(Category category, Post[] removingPosts)
        {
            var slugExists = _categoryRepository.Category(category.UrlSlug);
            if (slugExists != null && slugExists.Id != category.Id)
            {
                ModelState.AddModelError(String.Empty, "Slug уже существует");
                return Json(new { success = false, responseText = "Точно такой же Slug уже существует" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                ///TODO: save category
                try
                {
                _categoryRepository.SaveCategory(category);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                
                if (removingPosts != null)
                {
                    //TODO: remove posts
                }
                return Json(new { success = true, responseText = "Категория сохранена" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "Ошибка!" }, JsonRequestBehavior.AllowGet);
        }

        //TODO: handle ajax form post
        [HttpPost]
        public ActionResult DeleteCategory(int id)
        {
            //var cat = _repository.DeleteCategory(id);
            //if (cat != null)
            //{
            //    ViewData["message"] = $"Категория '{cat.Name}' удалена";
            //}
            return PartialView("~/Views/Admin/_GetCategoriesData.cshtml", _categoryRepository.Categories());
        }


    }
}
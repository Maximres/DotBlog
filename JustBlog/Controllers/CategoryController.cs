﻿using JustBlog.Core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JustBlog.Core.Objects;

namespace JustBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private const int PAGESIZE = 15;
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public ActionResult GetCategoriesData(int page = 1)
        {
            ViewBag.Title = "Categories";
            return PartialView("~/Views/Admin/GetCategoriesData.cshtml", _repository.Categories());
        }

        public ActionResult EditCategory(int id)
        {
            var _category = _repository.Category(id) ?? new Category { Id = 0};

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
            return PartialView("~/Views/Admin/GetCategoriesData.cshtml", _repository.Categories());
        }


    }
}
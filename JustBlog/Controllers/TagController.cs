using JustBlog.Core.Abstract;
using JustBlog.Core.Objects;
using JustBlog.Models.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace JustBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IPostRepository _postRepository;
        private const int PAGESIZE = 15;

        public TagController(ITagRepository repository, IPostRepository postRepository)
        {
            _tagRepository = repository;
            _postRepository = postRepository;
        }

        public ActionResult GetTagsData(int page = 1)
        {
            ViewBag.Title = "Tags";
            return PartialView("_GetTagsData", _tagRepository.Tags());
        }

        [HttpGet]
        public ActionResult EditTag(int id = 0)
        {
            var tag = _tagRepository.Tag(id);
            TagEditViewModel model = null;
            if (tag != null)
            {
                model = new TagEditViewModel
                {
                    Tag = tag,
                    IncludePosts = tag.Posts?.Count <= 0 ? false : true
                };
            }
            else
            {
                model = new TagEditViewModel
                {
                    Tag = new Tag { Id = 0 },
                    IncludePosts = false
                };
            }
            return PartialView("_EditTag", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTag(Tag tag)
        {
            var slugExists = _tagRepository.Tag(tag.UrlSlug);
            if (slugExists != null && slugExists.Id != tag.Id)
            {
                ModelState.AddModelError(String.Empty, "Slug уже существует");
                return Json(new { success = false, responseText = "Точно такой же Slug уже существует" }, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                
                try
                {
                    _tagRepository.SaveTag(tag);
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, responseText = ex.Message }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { success = true, responseText = $"Тег '{tag.Name}' успешно сохранен" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, responseText = "Неверный ввод" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PostsForTag(int id, int page = 1)
        {
            var tag = _tagRepository.Tag(id);
            var posts = _postRepository.PostsForTag(tag.UrlSlug, page, 15).Select(s => new { Id = s.Id, Title = s.Title  });
            //PostsForTag data = null;
            //if (tag != null)
            //{
            //    data = new PostsForTag
            //    {
            //        Posts = tag.Posts
            //    };
            //}
            //else
            //{
            //    data = new PostsForTag
            //    {
            //        Posts = null
            //    };

            //}
            //return Json(new
            //{
            //    isEmpty = posts is null,
            //    posts = posts
            //});
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(posts);
            //var obj = JsonConvert.SerializeObject(posts);
            return Json(json, JsonRequestBehavior.AllowGet);
        } 

        public ActionResult DeleteTag(int id)
        {
            Tag deletedTag = _tagRepository.DeleteTag(id);
            return PartialView("_GetTagsData", _tagRepository.Tags());
        }

    }
}
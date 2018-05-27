using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JustBlog.Controllers
{
    [Authorize(Roles = "admin")]
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult TakeImage()
        {
            throw new NotImplementedException();
        }

        public FileResult GetFile()
        {
            return new FileStreamResult(new FileStream("~/Scripts/froala-editor/js/editor.jpg", FileMode.Open), "image/jpg");
        }
    }
}
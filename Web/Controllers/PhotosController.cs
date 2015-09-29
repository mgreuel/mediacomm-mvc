using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Forum;
using MediaCommMvc.Web.Photos;

namespace MediaCommMvc.Web.Controllers
{
    public partial class PhotosController : RavenController
    {
        private readonly ImageGenerator imageGenerator;

        public PhotosController(ImageGenerator imageGenerator, UserStorage userStorage) : base(userStorage)
        {
            this.imageGenerator = imageGenerator;
        }

        public virtual ActionResult Index()
        {
            return this.View();
        }

        public virtual ActionResult Upload()
        {
            return this.View(new UploadPhotosViewModel());
        }

        [HttpPost]
        public virtual ActionResult Upload(HttpPostedFileBase[] files)
        {
            HttpPostedFileBase file = this.Request.Files[0];
            string filename = @"C:\temp\output\orig.jpg";
            file.SaveAs(filename);

            this.imageGenerator.GenerateAllImageSizes(filename);

            return this.Json(new[] {
                new
                {
                    name = file.FileName,
                    size = file.ContentLength,
                    delete_url = "http://none",
                    delete_type = "none",
                    url = "http://none",
                    thumbnail_url = "http://none"
                }
            });
        }
    }
}
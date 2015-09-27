using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using ImageProcessor;
using ImageProcessor.Imaging.Formats;

using MediaCommMvc.Web.Forum;
using MediaCommMvc.Web.Photos;

namespace MediaCommMvc.Web.Controllers
{
    public partial class PhotosController : Controller
    {
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

                ResizeImage(file, new Size(1900,1100));
                ResizeImage(file, new Size(1300,700));
                ResizeImage(file, new Size(600,600));
                ResizeImage(file, new Size(200,200));
            ResizeImage(file, new Size(175, 175));
                //ResizeImage(file, new Size(2048,1535));
                //ResizeImage(file, new Size(2048,1535));
                //ResizeImage(file, new Size(2048,1535));

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

        private static void ResizeImage(HttpPostedFileBase file,Size size)
        {
            string filename = @"C:\temp\output\orig.jpg";
            file.SaveAs(filename);

            byte[] photoBytes = System.IO.File.ReadAllBytes(filename);

            //ISupportedImageFormat format = new JpegFormat { Quality = quality };
           // Size size = new Size(1800, 900);

            using (MemoryStream inStream = new MemoryStream(photoBytes))
            {
                using (FileStream outStream = new FileStream($@"C:\temp\output\{size}_{PhotoOptions.JpegQuality}.jpg", FileMode.Create))
                {
                    using (ImageFactory imageFactory = new ImageFactory(preserveExifData: false))
                    {
                        // Load, resize, set the format and quality and save an image.
                        imageFactory.Load(inStream)
                            .Constrain(size)
                            .AutoRotate()
                            //.Format(format)
                            .Quality(PhotoOptions.JpegQuality)
                            .Save(outStream);
                    }

                    // Do something with the stream.
                }
            }
        }
    }
}
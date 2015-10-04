using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Forum;
using MediaCommMvc.Web.Photos;

namespace MediaCommMvc.Web.Controllers
{
    public partial class PhotosController : RavenController
    {
        private readonly ImageGenerator imageGenerator;

        private readonly PhotoMetaDataStorage photoMetaDataStorage;

        public PhotosController(ImageGenerator imageGenerator, UserStorage userStorage, PhotoMetaDataStorage photoMetaDataStorage) : base(userStorage)
        {
            this.imageGenerator = imageGenerator;
            this.photoMetaDataStorage = photoMetaDataStorage;
        }

        public virtual ActionResult Index()
        {
            return this.View(new PhotoIndexViewModel());
        }

        public virtual ActionResult Upload()
        {
            return this.View(new UploadPhotosViewModel { ExistingAlbums = this.photoMetaDataStorage.GetAllAlbumNames() });
        }

        [HttpPost]
        public virtual ActionResult Upload(string album)
        {
            HttpPostedFileBase file = this.Request.Files[0];

            string filename = @"C:\temp\output\orig.jpg";

            this.photoMetaDataStorage.SavePhoto(album, file.FileName);

            file.SaveAs(filename);

            HostingEnvironment.QueueBackgroundWorkItem(token => this.imageGenerator.GenerateAllImageSizes(filename, this.Config.PhotoStorageRootFolder));

            // see https://github.com/blueimp/jQuery-File-Upload/wiki/Setup for the return object structure
            return this.Json(
                new
                {
                    files = new[]
                                    {
                                        new
                                            {
                                                name = file.FileName,
                                                size = file.ContentLength,
                                                //delete_url = "http://none",
                                                //delete_type = "none",
                                                url = "http://placehold.it/350x350",
                                                //thumbnail_url = "http://placehold.it/150x150"
                                            }
                                    }
                });
        }
    }
}
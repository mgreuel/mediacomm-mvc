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
        private readonly PhotoMetaDataStorage photoMetaDataStorage;

        private readonly PhotoImporter photoImporter;

        public PhotosController(UserStorage userStorage, PhotoMetaDataStorage photoMetaDataStorage, PhotoImporter photoImporter) : base(userStorage)
        {
            this.photoMetaDataStorage = photoMetaDataStorage;
            this.photoImporter = photoImporter;
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
            
            this.photoImporter.ImportPhoto(file.InputStream, file.FileName, album, this.Config.PhotoStorageRootFolder);

            // see https://github.com/blueimp/jQuery-File-Upload/wiki/Setup for the return object structure
            return this.Json(
                new
                {
                    files = new[]
                                    {
                                        new
                                            {
                                                name = file.FileName,
                                                size = file.ContentLength
                                            }
                                    }
                });
        }
    }
}
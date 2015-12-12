using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;

using MediaCommMvc.Web.Features.Account;
using MediaCommMvc.Web.Features.Photos;
using MediaCommMvc.Web.Features.Photos.ViewModels;
using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
    [RoutePrefix("photos")]
    [Route("{action=index}")]
    public partial class PhotosController : RavenController
    {
        private readonly PhotoMetaDataStorage photoMetaDataStorage;

        private readonly PhotoImporter photoImporter;

        private readonly PhotoStorage photoStorage;

        public PhotosController(UserStorage userStorage, PhotoMetaDataStorage photoMetaDataStorage, PhotoImporter photoImporter, PhotoStorage photoStorage, Config config) : base(userStorage, config)
        {
            this.photoMetaDataStorage = photoMetaDataStorage;
            this.photoImporter = photoImporter;
            this.photoStorage = photoStorage;
        }

        [Route("album/{albumName}")]
        public virtual ActionResult Album(string albumName)
        {
            PhotoAlbumViewModel x = this.photoMetaDataStorage.GetAllbum(albumName);
            return this.View(x);
        }

        [Route("albumCover/{albumName}")]
        [OutputCache(Location = OutputCacheLocation.Client, Duration = 60 * 60)]
        public virtual ActionResult AlbumCover(string albumName)
        {
            string filename = this.photoMetaDataStorage.GetAlbumCoverFilename(albumName);

            return this.File(this.photoStorage.GetStoragePathForPhoto(albumName, filename, "thumbnail"), "image/jpeg");
        }

        public virtual ActionResult Index()
        {
            return this.View(this.photoMetaDataStorage.GetAllbumIndex());
        }

        public virtual ActionResult Upload()
        {
            return this.View(new UploadPhotosViewModel { ExistingAlbums = this.photoMetaDataStorage.GetAllAlbumTitles() });
        }

        [Route("view/{albumname}/{filename}/{size}")]
        [OutputCache(Location = OutputCacheLocation.Client, Duration = 60 * 60 * 24)]
        public virtual ActionResult ViewPhoto(string albumName, string filename, string size)
        {
            return this.File(this.photoStorage.GetStoragePathForPhoto(albumName, filename, size), "image/jpeg");
        }

        [HttpPost]
        public virtual ActionResult Upload(string albumTitle)
        {
            HttpPostedFileBase file = this.Request.Files[0];

            if (file == null || !IsValidPhotoFilename(file.FileName))
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
            }
            
            this.photoImporter.ImportPhoto(file.InputStream, file.FileName, albumTitle);

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

        private bool IsValidPhotoFilename(string fileName)
        {
            return fileName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || fileName.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase);
        }
    }
}
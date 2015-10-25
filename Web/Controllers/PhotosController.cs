using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

using MediaCommMvc.Web.Account;
using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Photos;
using MediaCommMvc.Web.Photos.ViewModels;

namespace MediaCommMvc.Web.Controllers
{
    [Authorize]
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

        public virtual ActionResult Index()
        {
            return this.View(new PhotoIndexViewModel());
        }

        public virtual ActionResult Upload()
        {
            return this.View(new UploadPhotosViewModel { ExistingAlbums = this.photoMetaDataStorage.GetAllAlbumNames() });
        }

        [Route("Photos/View/{album}/{filename}/{size}")]
        [OutputCache(Location = OutputCacheLocation.Client, Duration = 60 * 60 * 24)]
        public virtual ActionResult ViewPhoto(string album, string filename, string size)
        {
            return this.File(this.photoStorage.GetStoragePathForPhoto(album, filename, size), "image/jpeg");
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
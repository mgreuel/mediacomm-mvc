using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

using MediaCommMvc.Web.Infrastructure;

namespace MediaCommMvc.Web.Photos
{
    public class PhotoImporter
    {
        private readonly PhotoMetaDataStorage photoMetaDataStorage;

        private readonly ImageGenerator imageGenerator;

        public PhotoImporter(PhotoMetaDataStorage photoMetaDataStorage, ImageGenerator imageGenerator)
        {
            this.photoMetaDataStorage = photoMetaDataStorage;
            this.imageGenerator = imageGenerator;
        }

        public void ImportPhoto(Stream inputStream, string filename, string album, string photoStorageRootFolder)
        {
            Image originalImage = Image.FromStream(inputStream);
            RotateImageIfRequired(originalImage);

            string targetPath = this.GetStoragePathForImage(filename, album, photoStorageRootFolder);
            originalImage.Save(targetPath);

            this.photoMetaDataStorage.SavePhoto(album, filename);

            HostingEnvironment.QueueBackgroundWorkItem(token => this.imageGenerator.GenerateAllImageSizes(targetPath));
        }

        private static void RotateImageIfRequired(Image originalImage)
        {
            if (ExifRotation.ImageNeedsRotation(originalImage))
            {
                ExifRotation.ExifOrientations imageRotation = ExifRotation.DetermineImageRotation(originalImage);
                ExifRotation.RotateImageUsingExifOrientation(originalImage, imageRotation);
            }
        }

        private string GetStoragePathForImage(string filename, string album, string photoStorageRootFolder)
        {
            string directoryPath = Path.Combine(photoStorageRootFolder, album);
            string targetPath = Path.Combine(directoryPath, filename);

            Directory.CreateDirectory(directoryPath);

            if (File.Exists(targetPath))
            {
                targetPath = targetPath.Insert(targetPath.LastIndexOf("."), DateTime.UtcNow.ToString("_yyyyMMdd_HHmmss"));
            }

            return targetPath;
        }
    }
}

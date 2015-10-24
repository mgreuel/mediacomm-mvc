using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Hosting;

using MediaCommMvc.Web.Photos.Models;

namespace MediaCommMvc.Web.Photos
{
    public class PhotoImporter
    {
        private readonly ImageGenerator imageGenerator;

        private readonly ImageSizeCalculator imageSizeCalculator;

        private readonly ImageRotator imageRotator;

        private readonly PhotoMetaDataStorage photoMetaDataStorage;

        public PhotoImporter(PhotoMetaDataStorage photoMetaDataStorage, ImageGenerator imageGenerator, ImageSizeCalculator imageSizeCalculator, ImageRotator imageRotator)
        {
            this.photoMetaDataStorage = photoMetaDataStorage;
            this.imageGenerator = imageGenerator;
            this.imageSizeCalculator = imageSizeCalculator;
            this.imageRotator = imageRotator;
        }

        public void ImportPhoto(Stream inputStream, string filename, string album, string photoStorageRootFolder)
        {
            Image originalImage = Image.FromStream(inputStream);
            this.imageRotator.RotateImageIfRequired(originalImage);

            string targetPath = this.GetStoragePathForImage(filename, album, photoStorageRootFolder);
            originalImage.Save(targetPath);

            List<ImageSize> targetSizes = PhotoOptions.MaxSizesToGenerate.Select(size => this.imageSizeCalculator.CalculateTargetSize(originalImage, size)).ToList();

            this.photoMetaDataStorage.SavePhoto(album, new Photo { Filename = filename, Width = originalImage.Width, Height = originalImage.Height, ImageSizes = targetSizes });

            HostingEnvironment.QueueBackgroundWorkItem(token => this.imageGenerator.GenerateAllImageSizes(targetPath, targetSizes));
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
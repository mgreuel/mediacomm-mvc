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

        private readonly PhotoStorage photoStorage;

        public PhotoImporter(PhotoMetaDataStorage photoMetaDataStorage, ImageGenerator imageGenerator, ImageSizeCalculator imageSizeCalculator, ImageRotator imageRotator, PhotoStorage photoStorage)
        {
            this.photoMetaDataStorage = photoMetaDataStorage;
            this.imageGenerator = imageGenerator;
            this.imageSizeCalculator = imageSizeCalculator;
            this.imageRotator = imageRotator;
            this.photoStorage = photoStorage;
        }

        public void ImportPhoto(Stream inputStream, string filename, string album, string photoStorageRootFolder)
        {
            using (Image originalImage = Image.FromStream(inputStream))
            {
                this.imageRotator.RotateImageIfRequired(originalImage);

                string targetPath = this.photoStorage.StorePhoto(originalImage, filename, album);
                
                List<ImageSize> targetSizes = PhotoOptions.MaxSizesToGenerate.Select(size => this.imageSizeCalculator.CalculateTargetSize(originalImage, size)).ToList();

                this.photoMetaDataStorage.SavePhoto(album, new Photo { Filename = filename, Width = originalImage.Width, Height = originalImage.Height, ImageSizes = targetSizes });

                HostingEnvironment.QueueBackgroundWorkItem(token => this.imageGenerator.GenerateAllImageSizes(targetPath, targetSizes));
            }
        }
    }
}
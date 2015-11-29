using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;

using MediaCommMvc.Web.Features.Photos.Models;

namespace MediaCommMvc.Web.Features.Photos
{
    public class ImageGenerator
    {
        // the jpg encoder requires the parameter to be of type long!
        private const long JpegQuality = 85;

        private static readonly ImageCodecInfo JpegEncoder = ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

        private readonly ImageResizer imageResizer;

        private readonly PhotoStorage photoStorage;

        private static int numberOfOperationsInProgress;

        public ImageGenerator(ImageResizer imageResizer, PhotoStorage photoStorage)
        {
            this.imageResizer = imageResizer;
            this.photoStorage = photoStorage;
        }

        public void GenerateAllImageSizes(string originalImageFilePath, List<ImageSize> sizesToGenerate)
        {
            // too many parallel threads generating images cause OutOfMemoryExceptions
            while (numberOfOperationsInProgress >= 4)
            {
                Thread.Sleep(1000);
            }

            Interlocked.Increment(ref numberOfOperationsInProgress);

            try
            {
                using (Image sourceImage = Image.FromFile(originalImageFilePath))
                {
                    foreach (ImageSize size in sizesToGenerate)
                    {
                        FileInfo originalImageFile = new FileInfo(originalImageFilePath);
                        string targetFilename = this.photoStorage.GetFileNameForImageSize(originalImageFile.Name, size.Name);
                        string targetFilePath = Path.Combine(originalImageFile.DirectoryName, targetFilename);

                        using (Image newImage = this.imageResizer.GetResizedImage(sourceImage, size))
                        {
                            this.SaveJpeg(targetFilePath, newImage);
                        }
                    }
                }
            }
            finally
            {
                Interlocked.Decrement(ref numberOfOperationsInProgress);
            }
        }

        private void SaveJpeg(string path, Image image)
        {
            using (EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, JpegQuality))
            {
                using (EncoderParameters encoderParams = new EncoderParameters(1) { Param = { [0] = qualityParam } })
                {
                    image.Save(path, JpegEncoder, encoderParams);
                }
            }
        }
    }
}
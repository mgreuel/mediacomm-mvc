using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

using MediaCommMvc.Web.Photos.Models;

namespace MediaCommMvc.Web.Photos
{
    public class ImageGenerator
    {
        // the jpg encoder requires the parameter to be of type long!
        private const long JpegQuality = 85;

        private static readonly ImageCodecInfo JpegEncoder = ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

        private readonly ImageResizer imageResizer;

        private readonly PhotoStorage photoStorage;

        public ImageGenerator(ImageResizer imageResizer, PhotoStorage photoStorage)
        {
            this.imageResizer = imageResizer;
            this.photoStorage = photoStorage;
        }

        public void GenerateAllImageSizes(string originalImageFilePath, List<ImageSize> sizesToGenerate)
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
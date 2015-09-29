using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace MediaCommMvc.Web.Photos
{
    public class ImageGenerator
    {
        // the jpg encoder required the parameter to be of type long!
        private const long JpegQuality = 85;

        private class ImageSize
        {
            public int MaxWidth { get; set; }

            public int MaxHeight { get; set; }

            public string Name { get; set; }
        }

        private static readonly List<ImageSize> SizesToGenerate = new List<ImageSize>
        {
            new ImageSize { Name = "large", MaxWidth = 1900, MaxHeight = 1100 },
            new ImageSize { Name = "medium", MaxWidth = 1300, MaxHeight = 700 },
            new ImageSize { Name = "small", MaxWidth = 600, MaxHeight = 600 },
            new ImageSize { Name = "thumbnail", MaxWidth = 200, MaxHeight = 200 }
        };

        private static readonly ImageCodecInfo JpegEncoder = ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);

        private readonly ImageResizer imageResizer;

        public ImageGenerator(ImageResizer imageResizer)
        {
            this.imageResizer = imageResizer;
        }

        public void GenerateAllImageSizes(string filePath)
        {
            using (Image image = Image.FromFile(filePath))
            {
                foreach (ImageSize size in SizesToGenerate)
                {
                    using (Bitmap newImage = this.imageResizer.ResizeImage(image, size.MaxWidth, size.MaxHeight))
                    {
                        this.SaveJpeg($@"C:\temp\output\custom{size.Name}_{JpegQuality}.jpg", newImage);
                    }
                }
            }
        }

        private void SaveJpeg(string path, Image image)
        {
            EncoderParameter qualityParam = new EncoderParameter(Encoder.Quality, JpegQuality);

            EncoderParameters encoderParams = new EncoderParameters(1) { Param = {[0] = qualityParam } };
            image.Save(path, JpegEncoder, encoderParams);
        }
    }
}
using System;
using System.Drawing;

using MediaCommMvc.Web.Photos.Models;

namespace MediaCommMvc.Web.Photos
{
    public class ImageSizeCalculator
    {
        public ImageSize CalculateTargetSize(Image image, ImageSize maxImageSize)
        {
            float originalHeight = Convert.ToSingle(image.Height);
            float originalWidth = Convert.ToSingle(image.Width);

            float scale = Math.Max(originalWidth / maxImageSize.Width, originalHeight / maxImageSize.Height);
            int targetWidth = Convert.ToInt32(originalHeight / scale);
            int targetHeight = Convert.ToInt32(originalWidth / scale);

            return new ImageSize { Name = maxImageSize.Name, Width = targetWidth, Height = targetHeight };
        }
    }
}
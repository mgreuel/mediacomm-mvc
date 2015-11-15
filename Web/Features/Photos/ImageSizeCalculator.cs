using System;
using System.Drawing;

using MediaCommMvc.Web.Features.Photos.Models;

namespace MediaCommMvc.Web.Features.Photos
{
    public class ImageSizeCalculator
    {
        public ImageSize CalculateTargetSize(Image image, ImageSize maxImageSize)
        {
            float originalWidth = Convert.ToSingle(image.Width);
            float originalHeight = Convert.ToSingle(image.Height);

            float scale = Math.Max(originalWidth / maxImageSize.Width, originalHeight / maxImageSize.Height);
            int targetWidth = Convert.ToInt32(originalWidth / scale);
            int targetHeight = Convert.ToInt32(originalHeight / scale);

            return new ImageSize { Name = maxImageSize.Name, Width = targetWidth, Height = targetHeight };
        }
    }
}
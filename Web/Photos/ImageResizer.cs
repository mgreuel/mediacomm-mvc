using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

using MediaCommMvc.Web.Photos.Models;

namespace MediaCommMvc.Web.Photos
{
    public class ImageResizer
    {
        /// <summary>
        /// see http://stackoverflow.com/a/24199315
        /// </summary>
        public Image GetResizedImage(Image image, ImageSize targetSize)
        {
            var destRect = new Rectangle(0, 0, targetSize.Width, targetSize.Height);
            var destImage = new Bitmap(targetSize.Width, targetSize.Height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MediaCommMvc.Web.Photos
{
    public class ImageResizer
    {
        /// <summary>
        /// see http://stackoverflow.com/a/24199315
        /// </summary>
        public Bitmap ResizeImage(Image image, int maxWidth, int maxHeight)
        {
            Size targetSize = CalculateTargetSize(image, maxWidth, maxHeight);

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

        private static Size CalculateTargetSize(Image image, int maxWidth, int maxHeight)
        {
            float originalHeight = Convert.ToSingle(image.Height);
            float originalWidth = Convert.ToSingle(image.Width);

            float scale = Math.Max(originalHeight / maxHeight, originalWidth / maxWidth);
            int targetWidth = Convert.ToInt32(originalHeight / scale);
            int targetHeight = Convert.ToInt32(originalWidth / scale);

            Size targetSize = new Size(targetHeight, targetWidth);
            return targetSize;
        }
    }
}
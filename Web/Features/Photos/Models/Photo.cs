using System.Collections.Generic;

namespace MediaCommMvc.Web.Features.Photos.Models
{
    public class Photo
    {
        public string Filename { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsLandscape => this.Width >= this.Height;

        public List<ImageSize> ImageSizes { get; set; }
    }
}
using System.Collections.Generic;

namespace MediaCommMvc.Web.Photos.Models
{
    public class Photo
    {
        public string Filename { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public List<ImageSize> ImageSizes { get; set; }
    }
}
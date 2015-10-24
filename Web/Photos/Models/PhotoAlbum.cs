using System.Collections.Generic;

namespace MediaCommMvc.Web.Photos.Models
{
    public class PhotoAlbum
    {
        public PhotoAlbum()
        {
            this.Photos = new List<Photo>();
        }
        public string Id { get; set; }

        public string Title { get; set; }

        public List<Photo> Photos { get; set; }
    }
}

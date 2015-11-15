using System;
using System.Collections.Generic;

namespace MediaCommMvc.Web.Features.Photos.Models
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

        public DateTime Created { get; set; }

        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCommMvc.Web.Photos
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

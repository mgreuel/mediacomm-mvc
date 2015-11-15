using System.Collections;
using System.Collections.Generic;

namespace MediaCommMvc.Web.Features.Photos.ViewModels
{
    public class PhotoAlbumViewModel 
    {
        public string Title { get; set; }

        public IList<PhotoItemViewModel> Photos { get; set; }

        public string Name { get; set; }
    }
}
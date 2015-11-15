using System.Collections.Generic;
using System.Linq;

namespace MediaCommMvc.Web.Features.Photos.ViewModels
{
    public class PhotoIndexViewModel 
    {
        public IEnumerable<IGrouping<int, PhotoAlbumItemViewModel>> AlbumsByYear { get; set; }
    }
}
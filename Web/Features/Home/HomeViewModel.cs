using System.Collections.Generic;

using MediaCommMvc.Web.Features.Forum.ViewModels;
using MediaCommMvc.Web.Features.Photos.ViewModels;

namespace MediaCommMvc.Web.Features.Home
{
    public class HomeViewModel
    {
        public ForumOverview ForumOverview { get; set; }

        public IList<PhotoAlbumItemViewModel> PhotosAlbums { get; set; }
    }
}
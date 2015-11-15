using System;

namespace MediaCommMvc.Web.Features.Photos.ViewModels
{
    public class PhotoAlbumItemViewModel
    {
        public string Title { get; set; }

        public int PhotoCount { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }
    }
}
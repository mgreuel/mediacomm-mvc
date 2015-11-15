using System.Collections.Generic;

namespace MediaCommMvc.Web.Features.Photos.ViewModels
{
    public class UploadPhotosViewModel 
    {
        public IEnumerable<string> ExistingAlbums { get; set; }
    }
}
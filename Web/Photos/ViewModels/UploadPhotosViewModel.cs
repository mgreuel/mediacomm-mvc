using System.Collections.Generic;

namespace MediaCommMvc.Web.Photos.ViewModels
{
    public class UploadPhotosViewModel 
    {
        public IEnumerable<string> ExistingAlbums { get; set; }
    }
}
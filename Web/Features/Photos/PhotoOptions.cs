using System.Collections.Generic;

using MediaCommMvc.Web.Features.Photos.Models;

namespace MediaCommMvc.Web.Features.Photos
{
    public class PhotoOptions
    {
        public static readonly List<ImageSize> MaxSizesToGenerate = new List<ImageSize>
                                                                      {
                                                                          new ImageSize { Name = "large", Width = 1900, Height = 1100 },
                                                                          new ImageSize { Name = "medium", Width = 1300, Height = 700 },
                                                                          new ImageSize { Name = "small", Width = 600, Height = 600 },
                                                                          new ImageSize { Name = "thumbnail", Width = 170, Height = 170 }
                                                                      };
    }
}
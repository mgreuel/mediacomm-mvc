using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Photos.Models;

using Raven.Client;

namespace MediaCommMvc.Web.Photos
{
    public class PhotoMetaDataStorage
    {
        private readonly IDocumentSession documentSession;

        public PhotoMetaDataStorage(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public IEnumerable<string> GetAllAlbumNames()
        {
            return this.documentSession.Query<PhotoAlbum>().Select(a => a.Title).ToList();
        }

        public void SavePhoto(string album, Photo photo)
        {
            var photoAlbum = this.documentSession.Query<PhotoAlbum>().SingleOrDefault(a => a.Title == album) ?? new PhotoAlbum { Title = album};
            photoAlbum.Photos.Add(photo);
            this.documentSession.Store(photoAlbum);
        }
    }
}
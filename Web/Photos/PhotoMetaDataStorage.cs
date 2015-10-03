using System.Collections.Generic;
using System.Linq;

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
            return new List<string> { "Silvester 2012", "silvester 2013", "Geburtstag Thea", "Thea Geburtstag 2014" }; //return this.documentSession.Query<PhotoAlbum>().Select(a => a.Title).ToList();
        }
    }
}
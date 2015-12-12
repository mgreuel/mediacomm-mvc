using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Features.Photos.Models;
using MediaCommMvc.Web.Features.Photos.ViewModels;
using MediaCommMvc.Web.Helpers;

using Raven.Client;

namespace MediaCommMvc.Web.Features.Photos
{
    public class PhotoMetaDataStorage
    {
        private readonly IDocumentSession documentSession;

        public PhotoMetaDataStorage(IDocumentSession documentSession)
        {
            this.documentSession = documentSession;
        }

        public IEnumerable<string> GetAllAlbumTitles()
        {
            return this.documentSession.Query<PhotoAlbum>().Select(a => a.Title).ToList();
        }

        public void SavePhoto(string albumTitle, Photo photo)
        {
            string albumName = PathHelper.GetValidDirectoryName(albumTitle);

            var photoAlbum = this.documentSession.Load<PhotoAlbum>(PhotoAlbum.GetIdForName(albumName)) ?? new PhotoAlbum { Title = albumTitle, Name = albumName, Created = DateTime.UtcNow };
            photoAlbum.Photos.Add(photo);
            this.documentSession.Store(photoAlbum);
        }

        public PhotoIndexViewModel GetAllbumIndex()
        {
            IList<PhotoAlbumItemViewModel> albums = this.documentSession.Query<PhotoAlbum>().Select(a => new PhotoAlbumItemViewModel { PhotoCount = a.Photos.Count, Title = a.Title, Date = a.Created, Name = a.Name }).ToList();

            return new PhotoIndexViewModel { AlbumsByYear = albums.GroupBy(a => a.Date.Year) };
        }

        public string GetAlbumCoverFilename(string albumName)
        {
            return (this.documentSession.Query<PhotoAlbum>().Single(a => a.Name == albumName).Photos.First(p => p.IsLandscape) ?? this.documentSession.Query<PhotoAlbum>().Single(a => a.Name == albumName).Photos.First()).Filename;
        }

        public PhotoAlbumViewModel GetAllbum(string albumName)
        {
            var photoAlbum = this.documentSession.Query<PhotoAlbum>().Single(a => a.Name == albumName);

            return new PhotoAlbumViewModel
            {
                Name = photoAlbum.Name,
                Title = photoAlbum.Title,
                Photos = photoAlbum.Photos.Select(p => new PhotoItemViewModel
                {
                    LargeWidth = p.ImageSizes.Single(iz => iz.Name == "large").Width,
                    LargeHeight = p.ImageSizes.Single(iz => iz.Name == "large").Height,
                    MediumWidth = p.ImageSizes.Single(iz => iz.Name == "medium").Width,
                    MediumHeight = p.ImageSizes.Single(iz => iz.Name == "medium").Height,
                    SmallWidth = p.ImageSizes.Single(iz => iz.Name == "small").Width,
                    SmallHeight = p.ImageSizes.Single(iz => iz.Name == "small").Height,
                    Filename = p.Filename
                }).ToList()
            };
        }
    }
}
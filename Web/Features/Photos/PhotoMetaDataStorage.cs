using System;
using System.Collections.Generic;
using System.Linq;

using MediaCommMvc.Web.Features.Photos.Models;
using MediaCommMvc.Web.Features.Photos.ViewModels;
using MediaCommMvc.Web.Helpers;

using Microsoft.Ajax.Utilities;

using Raven.Client;

namespace MediaCommMvc.Web.Features.Photos
{
    public class PhotoMetaDataStorage
    {
        private readonly IDocumentSession documentSession;

        private readonly PhotoNotificationSender photoNotificationSender;

        public PhotoMetaDataStorage(IDocumentSession documentSession, PhotoNotificationSender photoNotificationSender)
        {
            this.documentSession = documentSession;
            this.photoNotificationSender = photoNotificationSender;
        }

        public IEnumerable<string> GetAllAlbumTitles()
        {
            return this.documentSession.Query<PhotoAlbum>().Select(a => a.Title).ToList();
        }

        public void SavePhoto(string albumTitle, Photo photo)
        {
            string albumName = PathHelper.GetValidDirectoryName(albumTitle);

            var photoAlbum = this.documentSession.Load<PhotoAlbum>(PhotoAlbum.GetIdForName(albumName));
            
            if (photoAlbum == null)
            {
                photoAlbum = new PhotoAlbum { Title = albumTitle, Name = albumName, Created = DateTime.UtcNow };

                this.photoNotificationSender.SendNewPhotoAlbumNotifications(albumTitle);
            }

            photoAlbum.Photos.Add(photo);
            this.documentSession.Store(photoAlbum);
        }

        public PhotoIndexViewModel GetAllbumIndex()
        {
            IList<PhotoAlbumItemViewModel> albums = this.documentSession.Query<PhotoAlbum>().Select(a => new PhotoAlbumItemViewModel { PhotoCount = a.Photos.Count, Title = a.Title, Date = a.Created, Name = a.Name }).ToList();

            return new PhotoIndexViewModel { AlbumsByYear = albums.GroupBy(a => a.Date.Year) };
        }

        public IList<PhotoAlbumItemViewModel> GetNewestAlbums(int count)
        {
            return this.documentSession.Query<PhotoAlbum>()
                .OrderByDescending(a => a.Created)
                .Take(count)
                .Select(a => new PhotoAlbumItemViewModel
                {
                    PhotoCount = a.Photos.Count,
                    Title = a.Title,
                    Date = a.Created,
                    Name = a.Name
                }).ToList();
        }

        public string GetAlbumCoverFilename(string albumName)
        {
            return (this.documentSession.Query<PhotoAlbum>().Single(a => a.Name == albumName).Photos.FirstOrDefault(p => p.IsLandscape) ?? this.documentSession.Query<PhotoAlbum>().Single(a => a.Name == albumName).Photos.First()).Filename;
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
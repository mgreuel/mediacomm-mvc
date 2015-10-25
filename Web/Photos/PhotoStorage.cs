﻿using System;
using System.Drawing;
using System.IO;

using MediaCommMvc.Web.Infrastructure;
using MediaCommMvc.Web.Photos.Models;

namespace MediaCommMvc.Web.Photos
{
    public class PhotoStorage
    {
        private readonly Config config;

        public PhotoStorage(Config config)
        {
            this.config = config;
        }

        public string StorePhoto(Image image, string filename, string album)
        {
            string directoryPath = Path.Combine(this.config.PhotoStorageRootFolder, album);
            string targetPath = Path.Combine(directoryPath, filename);

            Directory.CreateDirectory(directoryPath);

            if (File.Exists(targetPath))
            {
                targetPath = targetPath.Insert(targetPath.LastIndexOf("."), DateTime.UtcNow.ToString("_yyyyMMdd_HHmmss"));
            }

            image.Save(targetPath);

            return targetPath;
        }

        public string GetStoragePathForPhoto(string album, string filename, string size)
        {
            return Path.Combine(this.config.PhotoStorageRootFolder, album, this.GetFileNameForImageSize(filename, size));
        }

        public string GetFileNameForImageSize(string originalFileName, string size)
        {
            string extension = originalFileName.Substring(originalFileName.LastIndexOf("."));
            return $"{originalFileName.Replace(extension, String.Empty)}_{size}{extension}";
        }
    }
}
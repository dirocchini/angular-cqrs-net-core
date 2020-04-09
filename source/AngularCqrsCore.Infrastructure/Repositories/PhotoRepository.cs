using System.Collections.Generic;
using System.IO;
using Application.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Entities;
using Persistence.Common.Repositories;

namespace Persistence.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {

        
        private Cloudinary _cloudinary;

        public PhotoRepository(AngularCoreContext dbContext) : base(dbContext)
        {
            
        }


        public void Authenticate(string cloudName, string apiKey, string apiSecret)
        {
            Account account = new Account(cloud: cloudName, apiKey: apiKey, apiSecret: apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public (string url, string publicId)? SavePhoto(string fileName, Stream content)
        {
            if (content.Length > 0)
            {
                var upload = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, content),
                    Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                };

                var photo = _cloudinary.Upload(upload);
                return (url: photo.Uri.ToString(), publicId: photo.PublicId);
            }

            return null;
        }

        public void DestroyPhoto(string publicId)
        {
            _cloudinary.Destroy(new DeletionParams(publicId));
        }


        public IEnumerable<Photo> GetPhotos()
        {
            return new List<Photo>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Application.Interfaces;
using CloudinaryDotNet;
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

        public void SavePhoto()
        {
            
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return new List<Photo>();
        }
    }
}

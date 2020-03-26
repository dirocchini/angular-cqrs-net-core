using System.Collections.Generic;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPhotoRepository
    {
        void Authenticate(string cloudName, string api, string apiSecret);

        void SavePhoto();
        
        IEnumerable<Photo> GetPhotos();
    }
}

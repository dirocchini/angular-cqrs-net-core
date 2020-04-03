using System.Collections.Generic;
using System.IO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IPhotoRepository
    {
        void Authenticate(string cloudName, string api, string apiSecret);

        (string url, string publicId)? SavePhoto(string fileName, Stream content);
        void DestroyPhoto(string publicId);

        IEnumerable<Photo> GetPhotos();
    }
}

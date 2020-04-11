using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Api.Dto
{
    public class PhotoDto : CheckUserValidation
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string  Description { get; set; }
        public string PublicId { get; set; }


        public int TokenUserId { get; set; }
        public int Id { get; set; }
    }
}

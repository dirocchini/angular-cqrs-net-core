using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

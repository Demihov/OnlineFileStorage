using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace BLL.DTO.File
{
    public class FilePostRequest
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}

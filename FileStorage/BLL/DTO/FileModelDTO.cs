using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTO
{
    public class FileModelDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Published { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public DateTime Published { get; set; } = DateTime.Now;
    }
}

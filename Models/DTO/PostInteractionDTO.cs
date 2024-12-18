using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class CommentRequestDTO
    {
        public string? Text { get; set; }
        public byte[]? Image { get; set; }
    };

    public class CommentDTO
    {
        public int Id { get; set; }
        public int Recipe_blog_id { get; set; }
        public string? Text { get; set; }
        public byte[]? Image { get; set; }
        public DateTime Timestamp { get; set; }
    };

}

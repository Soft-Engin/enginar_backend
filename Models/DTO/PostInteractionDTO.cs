using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class PostInteractionDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }
        //public string InteractionType { get; set; } // "Like", "Comment", "Bookmark"
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class InteractionRequestDTO
    {
        public string InteractionType { get; set; } // "Like", "Comment", "Bookmark"
        public string Comment { get; set; } // Optional for non-comment interactions
    }

    public class CommentDTO
    {
        public string Text { get; set; }
    };

}

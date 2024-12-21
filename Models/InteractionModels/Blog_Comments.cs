using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InteractionModels
{
    public class Blog_Comments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public int BlogId { get; set; }
        [ForeignKey("BlogId")]
        public Blogs Blog { get; set; }

        [MaxLength(500)]
        public string? CommentText { get; set; }

        public byte[][]? Images { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}

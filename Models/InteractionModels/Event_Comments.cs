using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.InteractionModels
{
    public class Event_Comments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public Users User { get; set; }

        [Required]
        public int EventId { get; set; }
        [ForeignKey("EventId")]
        public Events Event { get; set; }

        [MaxLength(500)]
        public string? CommentText { get; set; }

        public byte[][]? Images { get; set; }
        public int ImagesCount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}

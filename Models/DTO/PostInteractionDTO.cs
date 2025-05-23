﻿using System;
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
        public byte[][]? Images { get; set; }
    };

    public class CommentDTO
    {
        public int Id { get; set; }
        public int Object_id { get; set; }
        public string? Text { get; set; }
        //public byte[][]? Images { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int ImagesCount { get; set; }
        public DateTime Timestamp { get; set; }
    };

}

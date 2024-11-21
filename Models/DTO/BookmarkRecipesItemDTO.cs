using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class BookmarkRecipesItemDTO
    {
        public string UserName { get; set; }

        public string Header { get; set; }

        public string BodyText { get; set; }
    }
}
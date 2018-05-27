using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBlog.Core.Objects.Metadata
{
    public class TagMetaData
    {
        [ScaffoldColumn(false)]
        public int Id
        { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50 символов")]
        public string Name
        { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50 символов")]
        [RegularExpression(@"^[a-zA-z-_]{2,}$", ErrorMessage = "Только латинские символы, дефис и нижний прочерк")]
        public string UrlSlug
        { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(200, ErrorMessage = "Максимальная длина 20 символов")]
        [DataType(DataType.MultilineText)]
        public string Description
        { get; set; }
    }
}

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
        [MaxLength(50)]
        public string Name
        { get; set; }

        [Required]
        [MaxLength(50)]
        public string UrlSlug
        { get; set; }

        [Required]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Description
        { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace JustBlog.Core.Objects
{
    [DisplayName("Категория")]
    public partial class CategoryMetaData
    {
        [ScaffoldColumn(false)]
        public int Id
        { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name
        { get; set; }

        [Display(Name = "Описание")]
        [UIHint("MultilineText")]
        [DataType(DataType.MultilineText)]
        public string Description
        { get; set; }

        public string UrlSlug
        { get; set; }

    }
}

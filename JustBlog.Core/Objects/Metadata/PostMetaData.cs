using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace JustBlog.Core.Objects.Metadata
{
    [DisplayName("Пост")]
    public partial class PostMetaData
    {
        [ScaffoldColumn(false)]
        public int Id
        { get; set; }

        [Required]
        [Display(Name = "Заголовок")]
        [MaxLength(500)]
        public string Title
        { get; set; }

        [Required]
        [Display(Name = "Краткое описание")]
        [MaxLength(5000)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription
        { get; set; }

        [Required]
        [AllowHtml]
        [Display(Name = "Содержание")]
        [MaxLength(5000)]
        [DataType(DataType.MultilineText)]
        public string Description
        { get; set; }

        [Required]
        [Display(Name = "Метаинформация")]
        [MaxLength(1000)]
        public string Meta
        { get; set; }

        [Required]
        [Display(Name = "Query параметр")]
        [MaxLength(200)]
        public string UrlSlug
        { get; set; }

        [Required]
        [Display(Name = "Опубликован")]
        public bool Published
        { get; set; }

        [Required]
        [Display(Name = "Публикация")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
            ApplyFormatInEditMode = true)]
        public DateTime PostedOn
        { get; set; }

        
        [Display(Name = "Изменения")]
        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}",
        //    ApplyFormatInEditMode = true)]
        public DateTime? Modified
        { get; set; }

        [ScaffoldColumn(false)]
        public int CategoryId
        { get; set; }

        [Required]
        public Category Category
        { get; set; }

    }
}

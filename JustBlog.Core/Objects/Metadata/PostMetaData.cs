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

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Заголовок")]
        [MinLength(3, ErrorMessage = "Минимальная длина 10 символа")]
        [MaxLength(500, ErrorMessage = "Максимальная длина 500 символов")]
        public string Title
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Краткое описание")]
        [MinLength(3, ErrorMessage = "Минимальная длина 10 символа")]
        [MaxLength(5000, ErrorMessage = "Максимальная длина 5000 символов")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [AllowHtml]
        [Display(Name = "Содержание")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(5000, ErrorMessage = "Максимальная длина 5000 символов")]
        [DataType(DataType.MultilineText)]
        public string Description
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Метаинформация")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(1000, ErrorMessage = "Максимальная длина 1000 символов")]
        [RegularExpression(@"^[a-zA-z-_\s0-9]{2,}$", ErrorMessage = "Только латинские символы, дефис и нижний прочерк")]
        public string Meta
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Query параметр")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(200, ErrorMessage = "Максимальная длина 200 символов")]
        [RegularExpression(@"^[a-zA-z-_0-9]{2,}$", ErrorMessage = "Только латинские символы, цифры, дефис и нижний прочерк")]
        public string UrlSlug
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Опубликован")]
        public bool Published
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
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

        [Required(ErrorMessage = "Обязательное поле")]
        public Category Category
        { get; set; }

    }
}

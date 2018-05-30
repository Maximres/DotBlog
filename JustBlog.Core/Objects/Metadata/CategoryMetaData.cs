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

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 100 символов")]
        public string Name
        { get; set; }

        [Display(Name = "Описание")]
        [UIHint("MultilineText")]
        [DataType(DataType.MultilineText)]
        [MinLength(20, ErrorMessage = "Минимальная длина 20 символа")]
        [MaxLength(200, ErrorMessage = "Максимальная длина 200 символов")]
        public string Description
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        [RegularExpression(@"^[a-zA-z-_0-9]{2,}$", ErrorMessage = "Только латинские символы, цифры, дефис и нижний прочерк")]
        public string UrlSlug
        { get; set; }

    }
}

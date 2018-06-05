using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JustBlog.Core.Objects
{
    //[DataContract]

    [DisplayName("Теги")]
    public partial class TagMetaData
    {
        //[DataMember]
        [ScaffoldColumn(false)]
        public int Id
        { get; set; }

        //[DataMember]
        [Required(ErrorMessage = "Обязательное поле")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 50 символов")]
        public string Name
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Query параметр")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(50, ErrorMessage = "Максимальная длина 200 символов")]
        [RegularExpression(@"^[a-zA-z-_0-9]{3,}$", ErrorMessage = "Только латинские символы, цифры, дефис и нижний прочерк")]
        public string UrlSlug
        { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        [MaxLength(200, ErrorMessage = "Максимальная длина 20 символов")]
        [DataType(DataType.MultilineText)]
        public string Description
        { get; set; }
    }
}

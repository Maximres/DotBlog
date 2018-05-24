using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace JustBlog.Core.Objects
{
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
        /// <summary>
        /// Первичный ключ (явно не указывается)
        /// </summary>
        public int Id
        { get; set; }

        /// <summary>
        /// Название категории, используемое для отображения
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// Заголовок, используемый в строке запроса
        /// </summary>
        public string UrlSlug
        { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        public string Description
        { get; set; }

        //[ScriptIgnore]
        /// <summary>
        /// Навигационное свойство. Список связанный постов с данной категорией
        /// </summary>
        public ICollection<Post> Posts
        { get; set; }

        public Category()
        {
            Posts = new List<Post>();
        }
    }
}

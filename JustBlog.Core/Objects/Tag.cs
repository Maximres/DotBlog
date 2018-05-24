using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using JustBlog.Core.Objects.Metadata;

namespace JustBlog.Core.Objects
{
    [MetadataType(typeof(TagMetaData))]
    public class Tag
    {
        /// <summary>
        /// Первичный ключ (явно не указывается)
        /// </summary>
        public int Id
        { get; set; }

        /// <summary>
        /// Название тега, используемое для отображения
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// Заголовок, используемый в строке запросов
        /// </summary>
        public string UrlSlug
        { get; set; }

        /// <summary>
        /// Описание тега
        /// </summary>
        public string Description
        { get; set; }

        /// <summary>
        /// Навигационное свойство. Список связанный постов с тегом
        /// </summary>
        public ICollection<Post> Posts
        { get; set; }
    }
}

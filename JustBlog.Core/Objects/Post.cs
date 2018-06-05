using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JustBlog.Core.Objects;

namespace JustBlog.Core.Objects
{
    [MetadataType(typeof(PostMetaData))]
    public partial class Post
    {
        /// <summary>
        /// Первичный ключ (явно не указывается)
        /// </summary>
        public int Id
        { get; set; }

        /// <summary>
        /// Заголовок, который отображается на главной странице
        /// </summary>
        public string Title
        { get; set; }

        /// <summary>
        ///Краткое описание, которое отображается на главной странице
        /// </summary>
        public string ShortDescription
        { get; set; }

        /// <summary>
        /// Контент поста
        /// </summary>
        public string Description
        { get; set; }

        /// <summary>
        /// Метаинформация для SEO
        /// </summary>
        public string Meta
        { get; set; }

        /// <summary>
        /// Альтернатива Title. Используется в строке запроса
        /// </summary>
        public string UrlSlug
        { get; set; }

        /// <summary>
        /// Указывает, опубликован ли пост
        /// </summary>
        public bool Published
        { get; set; }

        /// <summary>
        /// Дата и время публикации
        /// </summary>
        public DateTime PostedOn
        { get; set; }

        /// <summary>
        /// Определяет, был ли пост изменен
        /// </summary>
        public DateTime? Modified
        { get; set; }

        /// <summary>
        /// Внешний ключ для Category
        /// </summary>
        public int CategoryId
        { get; set; }

        /// <summary>
        /// Навигационное свойство к типу Category
        /// </summary>
        public Category Category
        { get; set; }

        /// <summary>
        /// Навигационное поле. Список все тегов
        /// </summary>
        public ICollection<Tag> Tags
        { get; set; }

        public Post()
        {
            Tags = new List<Tag>();
        }
    }
}

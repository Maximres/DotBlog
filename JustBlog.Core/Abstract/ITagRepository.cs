using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBlog.Core.Abstract
{
    public interface ITagRepository
    {
        /// <summary>
        /// Возвращает определенный тег, в зависимости от имени строке запроса <paramref name="tagSlug"/>
        /// </summary>
        /// <param name="tagSlug">Параметр, отображаемый в строке запроса</param>
        /// <returns></returns>
        Tag Tag(string tagSlug);

        /// <summary>
        /// Возвращает тег по <paramref name="id"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Tag Tag(int id);

        /// <summary>
        /// Возвращает все теги
        /// </summary>
        /// <returns></returns>
        IEnumerable<Tag> Tags();

        /// <summary>
        /// Возвращает теги, название который представлены в <paramref name="names"/>
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        IEnumerable<Tag> Tags(params string[] names);

        /// <summary>
        /// Удаляет тег по Id
        /// </summary>
        /// <param name="id"></param>
        Tag DeleteTag(int id);

        /// <summary>
        /// Добавляет/изменяет в БД тег
        /// </summary>
        /// <param name="tag"></param>
        void SaveTag(Tag tag);
    }
}

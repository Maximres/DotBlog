using JustBlog.Core.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustBlog.Core.Abstract
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Вовзращает определенную категории по <paramref name="categorySlug"/>
        /// </summary>
        /// <param name="categorySlug"></param>
        /// <returns></returns>
        Category Category(string categorySlug);

        /// <summary>
        /// Вовзращает определенную категории по <paramref name="id"/>
        /// </summary>
        /// <param name="id">ID категории</param>
        /// <returns></returns>
        Category Category(int id);

        /// <summary>
        /// Удаляет категорию по Id
        /// </summary>
        /// <param name="id"></param>
        Category DeleteCategory(int id);

        /// <summary>
        /// Возвращает все категории
        /// </summary>
        /// <returns></returns>
        IEnumerable<Category> Categories();

        /// <summary>
        /// Добавляет/Изменяет в БД категорию
        /// </summary>
        /// <param name="category"></param>
        void SaveCategory(Category category);

        Category CategoryWithSameSlug(string slug);
    }
}

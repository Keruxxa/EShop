using EShop.Domain.Entities;

namespace EShop.Application.Features.Models
{
    /// <summary>
    ///     Представляет модель для отображения элемента в списке
    /// </summary>
    /// <typeparam name="TKey">Тип Id</typeparam>
    public class SelectListItem<TKey> : EntityBase<TKey> where TKey : struct
    {
        /// <summary>
        ///     Наименование
        /// </summary>
        public string Name { get; set; }


        public static SelectListItem<int> CreateItem(Country country)
        {
            return new SelectListItem<int>()
            {
                Id = country.Id,
                Name = country.Name
            };
        }
    }
}

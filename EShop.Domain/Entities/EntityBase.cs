namespace EShop.Domain.Entities
{
    /// <summary>
    ///     Базовая сущность, имеющая поле Id обобщенного типа <typeparamref name="TKey"/>
    /// </summary>
    /// <typeparam name="TKey">
    ///     Тип Id
    /// </typeparam>
    public abstract class EntityBase<TKey> where TKey : struct
    {
        public TKey Id { get; set; }
    }
}

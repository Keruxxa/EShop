namespace EShop.Domain.Entities;

/// <summary>
///     Представляет страну
/// </summary>
public class Country : EntityBase<int>
{
    /// <summary>
    ///     Название
    /// </summary>
    public string Name { get; private set; }


    private Country() { }


    /// <param name="name">Наименование</param>
    public Country(string name)
    {
        Name = name;
    }

    /// <summary>
    ///     Устанавливает значение наименования <see cref="Name"/>
    /// </summary>
    public void UpdateName(string name)
    {
        Name = name;
    }
}

namespace EShop.Domain.Entities;

/// <summary>
///     Представляет узел дерева замыкания категорий
/// </summary>
public class CategoryClosureNode
{
    /// <summary>
    ///     Id категории-предка
    /// </summary>
    public int AncestorCategoryId { get; }

    /// <summary>
    ///     Категория-предок
    /// </summary>
    public Category AncestorCategory { get; }

    /// <summary>
    ///     Id категории-потомка
    /// </summary>
    public int DescendantCategoryId { get; }


    public CategoryClosureNode(int ancestorCategoryId, int descendantCategoryId)
    {
        AncestorCategoryId = ancestorCategoryId;
        DescendantCategoryId = descendantCategoryId;
    }
}

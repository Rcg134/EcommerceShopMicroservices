namespace Catalog.API.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException(Guid id)
        : base($"Product with id {id} was not found.")
    {
    }

    public ProductNotFoundException(string category)
        : base($"Product with category {category} was not found.")
    {
    }

}

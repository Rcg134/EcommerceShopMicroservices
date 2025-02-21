namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid Id)
        : base("Product", Id)
    {
    }

    public ProductNotFoundException(string category)
        : base("Category", category)
    {
    }

}

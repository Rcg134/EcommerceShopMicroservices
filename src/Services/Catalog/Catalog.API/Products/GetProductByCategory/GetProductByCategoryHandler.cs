namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Product);

public class GetProductByCategoryHandler
    (IDocumentSession session, ILogger<GetProductByCategoryHandler> logger)
    : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle
        (GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByCategoryResult.Handle called with {@Query}", query);

        var products = await session.Query<Product>()
                          .Where(p => p.Category.Any(c => c.Equals(query.Category, StringComparison.OrdinalIgnoreCase)))
                          .ToListAsync(cancellationToken);


        if (products is null)
        {
            throw new ProductNotFoundException(query.Category);
        }

        return new GetProductByCategoryResult(products);
    }
}

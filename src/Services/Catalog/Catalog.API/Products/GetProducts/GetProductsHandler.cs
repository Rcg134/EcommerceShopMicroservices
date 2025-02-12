namespace Catalog.API.Products.GetProducts;

public record GetproductsQuery(): IQuery<GetproductsResult>;  // Query

public record GetproductsResult(IEnumerable<Product> Products); // Result


internal class GetProductsQueryHandler
    (IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
    : IQueryHandler<GetproductsQuery, GetproductsResult>
{
    public async Task<GetproductsResult> Handle(GetproductsQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductQueryHandler.Handle called with {@Query}", query);

        var products = await session.Query<Product>()
            .ToListAsync(cancellationToken);

        return new GetproductsResult(products);
    }
}
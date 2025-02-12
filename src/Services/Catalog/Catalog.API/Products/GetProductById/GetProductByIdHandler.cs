namespace Catalog.API.Products.GetProductById;

public record GetProductsIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

public record GetProductByIdResult(Product Product);

public class GetProductByIdHandler 
    (IDocumentSession session, ILogger<GetProductByIdHandler> logger)
    : IQueryHandler<GetProductsIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle
        (GetProductsIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductByIdResult.Handle called with {@Query}", query);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}

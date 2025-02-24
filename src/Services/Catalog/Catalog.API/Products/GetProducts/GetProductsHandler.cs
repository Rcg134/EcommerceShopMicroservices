

namespace Catalog.API.Products.GetProducts;

public record GetproductsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetproductsResult>;  // Query

public record GetproductsResult(IEnumerable<Product> Products); // Result


internal class GetProductsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetproductsQuery, GetproductsResult>
{
    public async Task<GetproductsResult> Handle(GetproductsQuery query, CancellationToken cancellationToken)
    {

        var products = await session.Query<Product>()
            .ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

        return new GetproductsResult(products);
    }
}
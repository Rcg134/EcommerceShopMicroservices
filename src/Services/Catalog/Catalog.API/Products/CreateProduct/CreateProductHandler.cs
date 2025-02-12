namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(
        IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create Product Entity From Command Object
            var product = command.Adapt<Product>();


            // Save To Database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);


            // Return CreatreProductResult Result
            return new CreateProductResult(product.Id);
        }
    }
}

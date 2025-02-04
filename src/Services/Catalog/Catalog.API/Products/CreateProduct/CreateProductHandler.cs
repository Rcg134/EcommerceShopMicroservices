using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string description, string ImageFile, decimal Price)
    :IRequest<CreatreProductResult>;
    public record CreatreProductResult(Guid Id);

    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatreProductResult>
    {
        public Task<CreatreProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Business logic to create a product

            throw new NotImplementedException();
        }
    }
}

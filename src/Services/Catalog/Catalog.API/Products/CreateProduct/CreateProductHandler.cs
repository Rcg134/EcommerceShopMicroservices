
using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string description, string ImageFile, decimal Price)
    : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create Product Entity From Command Object
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };


            // Save To Database



            // Return CreatreProductResult Result
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}

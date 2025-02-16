# EcommerceShop

## Create a new project

- Create a new with blank solution
- Make root folder as "src" to enapsulate all the projects

### Create Folder Structure

```bash

 Services --> Basket
              --> Basket.API
          --> Catalog
              --> Catalog.API
          --> Discount
              --> Discount.API
          --> Ordering
              --> Ordering.API
              -->Ordering.Application
              -->Ordering.Domain
              -->Ordering.Infrastructure
```

### Port Numbers for microservices

```bash

| Microservices | Local Env | Docker Env | Docker Inside |
|---------------|-----------|------------|---------------|
| Catelog       | 5000-5050 | 6000-6060  | 8080-8081     |
| Basket        | 5001-5051 | 6001-6061  | 8080-8081     |
| Discount      | 5002-5052 | 6002-6062  | 8080-8081     |
| Ordering      | 5003-5053 | 6003-6063  |               |

```

# CQRS with MediatR in .NET Core

## 📌 Overview

- This guide demonstrates how to implement the CQRS (Command Query Responsibility Segregation) pattern using MediatR in a .NET Core application. The focus is on handling queries efficiently by separating read (queries) from write (commands) operations.

## 🔧 Prerequisites

- .NET Core 8 or later

- MediatR NuGet package

- Dependency Injection

- Entity Framework Core or any persistence layer (optional for data access)

## 📂 Folder Structure of Vertical Slice Architecture

```bash
Catalog.API/
│-- Products/
│   │-- GetProductById/
│   │   ├── GetProductsIdQuery.cs
│   │   ├── GetProductByIdResult.cs
│   │   ├── GetProductByIdHandler.cs
│   │-- Commands/ (for write operations)
│   └── Queries/ (for read operations)
│-- Controllers/
│-- Program.cs
```

## 📜 Step-by-Step Implementation

### 1️⃣ Define the Query

- Create a query record to request a product by ID.

```bash
namespace Catalog.API.Products.GetProductById;

public record GetProductsIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

```

### 2️⃣ Define the Response Model

- This will be the return type when querying a product.

```bash
namespace Catalog.API.Products.GetProductById;

public record GetProductByIdResult(Product Product);
```

### 3️⃣ Implement the Query Handler

- The handler processes the query and returns the result.

```bash
using MediatR;
using Marten;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

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
        logger.LogInformation("Fetching product with ID {Id}", query.Id);

        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}
```

# EcommerceShop

## Create a new project
 - Create a new with blank solution
 - Make root folder as "src" to enapsulate all the projects

### Create Folder Structure

````bash

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
````

### Port Numbers for microservices

`````bash

| Microservices | Local Env | Docker Env | Docker Inside |
|---------------|-----------|------------|---------------|
| Catelog       | 5000-5050 | 6000-6060  | 8080-8081     |
| Basket        | 5001-5051 | 6001-6061  | 8080-8081     |
| Discount      | 5002-5052 | 6002-6062  | 8080-8081     |
| Ordering      | 5003-5053 | 6003-6063  |               |

`````
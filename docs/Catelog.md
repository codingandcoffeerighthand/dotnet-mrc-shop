# Catelog

- Simple & fast API: use ASP.NET Minimal APIs (I'm familiar and it's fast).
- Database: Should NoSQL (Mongo). But i use Postges (i like Postgres, it used in another serivce in project and i want keep simple to managing database), Marten (for use Postges like NoSQL). So, we have ACID of RDBMS and performace of NoSQL.
- CQRS: of course, MediatR.
- DataMapping: Mapster instead of AutoMapper for simpler data conversion.
- Carter: Easy define Minimal API.
- FluentValidation: popular validate inputs and add MediatR validation pipline.
- Docker: for Postgres, etc.

## 1. Domain models

### 1.1 Product

+ Id: Guid
+ Name: string
+ Description: string
+ ImagFile: string
+ Price: decimal
+ Category: List\<string>

### 1.2 Domaint evnet

+ product change price

## 2 Use Cases

- CRUD:
    + Listing products and categories
    + Get product with product Id
    + Get products with category
    + Create new product
    + Update product
    + Delete product

***Note***: main use case are listing and search products and caretories
### 3 APIs (Restfull)

| Use case | Method | Request URI | query| Body|
|   :-----|   :-----|   :-----| :-----| :-----|
|List all product | Get | /products | 
|Fetch a specific product | Get | /products/{id} |
|Get products by category | Get | /products/catetory| catetory |
|Create a new product | Post | /products | | {product}|
|Update a product | Put | /products/{id} | | {product}|
|Delete a product | Delete | /products/{id}|

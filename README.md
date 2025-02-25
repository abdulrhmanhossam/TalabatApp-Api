# TalabatApp API - Food Delivery Backend System

[![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-8.0-blue)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF_Core-8.0-red)](https://learn.microsoft.com/en-us/ef/core/)
[![Redis](https://img.shields.io/badge/Redis-7.x-critical)](https://redis.io/)

A sophisticated food delivery backend system with advanced architectural patterns and payment integrations.

## Features 🚀
- **Specification Pattern Implementation**
- **Repoistory Pattern Implementation**
- **Multi-PaymentMethod Support**
- **Redis Caching Layer**
- **JWT Authentication**
- **Order Management System**
- **Menu & Restaurant Catalog**
- **Rate Limiting**
- **Swagger Documentation**

## Technologies Stack 💻
- **ASP.NET Core 8** - Web Framework
- **Entity Framework Core** - ORM
- **SQL Server** - Primary Database
- **Redis** - Distributed Caching
- **AutoMapper** - Object Mapping
- **FluentValidation** - Validation

## Architecture Patterns 🏗️
### Repository Pattern
### Specification Pattern
```csharp
// Base Specification
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    Expression<Func<T, object>> OrderBy { get; }
    Expression<Func<T, object>> OrderByDescending { get; }
}

// Product Specification Example
public class ProductsWithFiltersSpec : BaseSpecification<Product>
{
    public ProductsWithFiltersSpec(ProductSpecParams productParams)
        : base(p => 
            (!productParams.RestaurantId.HasValue || p.RestaurantId == productParams.RestaurantId) &&
            (string.IsNullOrEmpty(productParams.Search) || p.Name.Contains(productParams.Search))
        )
    {
        AddInclude(p => p.Restaurant);
        ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
        
        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }
}

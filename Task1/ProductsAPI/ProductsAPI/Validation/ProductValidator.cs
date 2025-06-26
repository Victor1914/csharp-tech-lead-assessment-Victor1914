namespace ProductsAPI.Validation;

using Interfaces;
using Models;

public class ProductValidator : IProductValidator
{
    public string Validate(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            return "Name is required.";

        if (product.Price <= 0)
            return "Price must be positive.";

        return null;
    }
}
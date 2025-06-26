namespace ProductsAPI.Interfaces;

using Models;

public interface IProductValidator
{
    string Validate(Product product);
}
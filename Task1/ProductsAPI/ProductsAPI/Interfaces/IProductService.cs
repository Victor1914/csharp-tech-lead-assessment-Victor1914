namespace ProductsAPI.Interfaces;

using Models;

public interface IProductService
{
    List<Product> GetAll();

    Product GetById(int id);

    Product Add(Product product);

    bool Update(int id, Product updated);

    bool Delete(int id);
}
namespace ProductsAPI.Business;

using Interfaces;
using Models;

public class ProductService : IProductService
{
    private readonly List<Product> _products = [];
    private int _nextId = 1;

    public List<Product> GetAll() => _products;

    public Product GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

    public Product Add(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);

        return product;
    }

    public bool Update(int id, Product updated)
    {
        var existing = GetById(id);

        if (existing == null)
            return false;

        existing.Name = updated.Name;
        existing.Description = updated.Description;
        existing.Price = updated.Price;

        return true;
    }

    public bool Delete(int id)
    {
        var product = GetById(id);

        if (product == null) 
            return false;

        _products.Remove(product);

        return true;
    }
}
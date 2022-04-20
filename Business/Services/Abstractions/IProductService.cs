using Data.Entities;

namespace Business.Services.Abstractions;

public interface IProductService
{
    public List<Product> GetProducts();
    public Product GetProduct(int id);
    public void AddProduct(Product product);
    public void UpdateProduct(Product product);
    public void DeleteProduct(int id);
}
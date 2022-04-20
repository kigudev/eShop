using Business.Services.Abstractions;
using Data.Entities;

namespace Business.Services.Implementations;

public class ProductService : IProductService
{
    private List<Product> ProductList = TestData.ProductList;

    public List<Product> GetProducts()
    {
        return ProductList;
    }

    public Product GetProduct(int id)
    {
        return ProductList.FirstOrDefault(c => c.Id == id);
    }

    public void AddProduct(Product product)
    {
        ProductList.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        var entity = ProductList.FirstOrDefault(c => c.Id == product.Id);
        
        if (entity != null)
            entity.Update(product.Name, product.Description, product.Price);
        else
            throw new ApplicationException("El producto no fue encontrado");
    }

    public void DeleteProduct(int id)
    {
        var entity = ProductList.FirstOrDefault(c => c.Id == id);

        if (entity != null)
            ProductList.Remove(entity);
        else
            throw new ApplicationException("El producto no fue encontrado");
    }

   
}
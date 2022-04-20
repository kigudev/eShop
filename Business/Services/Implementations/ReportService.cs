using Business.Models;
using Business.Services.Abstractions;
using Data.Entities;
using Data.Enums;

namespace Business.Services.Implementations;

public class ReportService : IReportService
{
    private List<Product> ProductList = TestData.ProductList;
    public List<ProductReportDto> GetTop5ProductosMasCaros()
    {
        return ProductList
            .OrderByDescending(c => c.Price)
            .Take(5)
            .Select(c => new ProductReportDto
        {
            Name = c.Name,
            Price = c.Price
        }).ToList();
    }

    public List<ProductReportUnitDto> GetProductosCon5UnidadesOMenos()
    {
        return ProductList
            .OrderByDescending(c => c.Stock)
            .Take(5)
            .Select(c => new ProductReportUnitDto
            {
                Name = c.Name,
                Units = c.Stock
            }).ToList();
    }

    public List<ProductReportsBrandDto> GetProductosPorMarca()
    {
        return ProductList
            .OrderBy(c => c.Name)
            .Select(c => new ProductReportsBrandDto
            {
                Name = c.Name,
                Brand = c.Brand
            }).ToList();
    }

    public List<ProductReportByDepartment> GetDepartamentosConSubdepartamentosYProductos()
    {
        return ProductList
            .OrderBy(c => c.Name)
            .Select(c => new ProductReportByDepartment
            {
                Name = c.Name,
                Department = c.Subdeparment?.Deparment?.Name,
                Subdeparment = c.Subdeparment?.Name
            }).ToList();
    }

    public Product GetProductoConUnidadesMasCompradas(List<PurchaseOrder> purchaseOrders)
    {
        return purchaseOrders.Where(c => c.Status == PurchaseOrderStatus.Paid)
            .SelectMany(c => c.PurchasedProducts)
            .GroupBy(c => c.Id)
            .Select(c => new {Product = c.First(), Sum = c.Sum(d => d.Stock)})
            .OrderByDescending(c => c.Sum)
            .FirstOrDefault()?.Product;
    }
    
    // o1 - p1, p2, p3
    // o3 - p1
    // o4 - p2, p3
    // = 
    // p1,p2,p3,p1,p2,p3
    // p1 - 2. p2 - 2, p3 - 2
    // "p1"- p1 - sum([2])
    //
}
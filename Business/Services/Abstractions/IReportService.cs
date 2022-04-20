using Business.Models;

namespace Business.Services.Abstractions;

public interface IReportService
{
    public List<ProductReportDto> GetTop5ProductosMasCaros();
    public List<ProductReportUnitDto> GetProductosCon5UnidadesOMenos();
    public List<ProductReportsBrandDto> GetProductosPorMarca();
    public List<ProductReportByDepartment> GetDepartamentosConSubdepartamentosYProductos();
}
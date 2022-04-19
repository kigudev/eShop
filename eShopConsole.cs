using Business.Services.Abstractions;
using Business.Services.Implementations;
using Data.Entities;

namespace eShop;

public partial class eShopConsole
{
    private readonly IProductService _productService;
    private readonly IDepartmentService _departmentService;
    private readonly IReportService _reportService;
    private readonly IPurchaseOrderService _purchaseOrderService;

    public eShopConsole()
    {
        _productService = new ProductService();
        _departmentService = new DepartmentService();
        _reportService = new ReportService();
        _purchaseOrderService = new PurchaseOrderService();
    }
    
    public bool MainMenu()
    {
        Console.Clear();
        Console.WriteLine("Elige una opción:");
        Console.WriteLine("1. Agregar Producto");
        Console.WriteLine("2. Editar Producto");
        Console.WriteLine("3. Consultar Productos");
        Console.WriteLine("4. Consultar Producto");
        Console.WriteLine("5. Eliminar Producto");
        Console.WriteLine("6. Reportes");
        Console.WriteLine("7. Ordenes de compra");
        Console.WriteLine("8. Salir del sistema");

        switch (Console.ReadLine())
        {
            case "1":
                AgregarProducto();
                break;
            case "2":
                EditarProducto();
                break;
            case "3":
                ConsultarProductos();
                break;
            case "4":
                ConsultarProducto();
                break;
            case "5":
                EliminarProducto();
                break;
            case "6":
                MenuDeReportes();
                break;
            case "7":
                MenuDeOrdenesDeCompra();
                break;
            default:
                return false;
        }

        return true;
    }

    private void EliminarProducto()
    {
        Console.WriteLine("Id del producto a eliminar:");
        var id = Console.ReadLine();
        
        try
        {
            _productService.DeleteProduct(IntentarObtenerInt(id));
            Console.WriteLine("Producto eliminado correctamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }

    private void ConsultarProducto()
    {
        Console.WriteLine("Id del producto:");
        var id = Console.ReadLine();
        try
        {
            var product = _productService.GetProduct(IntentarObtenerInt(id));
            Console.WriteLine($"ID \t Nombre \t Marca \t SKU \t Stock \t Descripción");
            Console.WriteLine(product);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();
    }

    private void ConsultarProductos()
    {
        Console.WriteLine($"ID \t Nombre \t Marca \t SKU \t Stock \t Descripción");
        foreach (var p in _productService.GetProducts())
        {
            Console.WriteLine(p);
        }

        Console.ReadLine();
    }

    private void AgregarProducto()
    {
        Console.WriteLine("Agrega los valores necesario para registrar un producto");
        Console.WriteLine("Id:");
        var id = Console.ReadLine();
        Console.WriteLine("Nombre:");
        var name = Console.ReadLine();
        Console.WriteLine("Precio:");
        var price = Console.ReadLine();
        Console.WriteLine("Descripción:");
        var description = Console.ReadLine();
        Console.WriteLine("Marca:");
        var brand = Console.ReadLine();
        Console.WriteLine("SKU:");
        var sku = Console.ReadLine();

        try
        {
            var subdepartamento = SolicitarSubdepartamento();
            
            var idAux = IntentarObtenerInt(id);
            if (!decimal.TryParse(id, out decimal priceAux))
            {
                throw new ApplicationException("El precio es inválido");
            }

            var product = new Product(idAux, name, priceAux, description, brand, sku);
            product.AddSubdepartment(subdepartamento);
            _productService.AddProduct(product);
            Console.WriteLine("Producto agregado correctamente");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("La opción seleccionada está fuera del rango permitido");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.ReadLine();
    }

    private Subdeparment SolicitarSubdepartamento()
    {
        Console.WriteLine("Elige el departamento:");
        // mostrar los departamentos
        var deparments = _departmentService.GetDeparments();
        for (int i = 0; i < deparments.Count; i++)
        {
            Console.WriteLine($"{i+1}. {deparments.ElementAt(i)}");
        }
        // elegir un departamento
        var departmentPosition = IntentarObtenerInt(Console.ReadLine()) - 1;
        var deparment = deparments.ElementAt(departmentPosition);
        Console.WriteLine($"Elige el subdepartamento de {deparment.Name}:");
        // mostrar subdepartamentos del departamento
        for (int i = 0; i < deparment.Subdeparments.Count; i++)
        {
            Console.WriteLine($"{i+1}. {deparment.Subdeparments.ElementAt(i)}");
        }
        // elegir un subdepartamento
        var subdepartmentPosition = IntentarObtenerInt(Console.ReadLine()) - 1;
        var subdepartment = deparment.Subdeparments.ElementAt(subdepartmentPosition);
        subdepartment.Deparment = deparment;
        return subdepartment;
    }

    private void EditarProducto()
    {
        Console.WriteLine("Agrega los valores necesario para registrar un producto");
        Console.WriteLine("Id:");
        var id = Console.ReadLine();
        Console.WriteLine("Nombre:");
        var name = Console.ReadLine();
        Console.WriteLine("Precio:");
        var price = Console.ReadLine();
        Console.WriteLine("Descripción:");
        var description = Console.ReadLine();
        
        try
        {
            var idAux = IntentarObtenerInt(id);
            if (!decimal.TryParse(price, out decimal priceAux))
            {
                throw new ApplicationException("El precio es inválido");
            }
            
            var product = _productService.GetProduct(idAux);
            product.Update(name, description, priceAux);
            _productService.UpdateProduct(product);
            Console.WriteLine("Producto editado correctamente");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.ReadLine();
    }

    private int IntentarObtenerInt(string id)
    {
        if (!int.TryParse(id, out int idAux))
        {
            throw new ApplicationException("No se pudo castear el ID correctamente");
        }

        return idAux;
    }
}
namespace eShop;

public partial class eShopConsole
{
    private void MenuDeReportes()
    {
        Console.WriteLine("Elige una opción:");
        Console.WriteLine("1. Top 5 de productos más caros ordenados por precio más alto");
        Console.WriteLine("2. Productos con 5 unidades o menos ordenados por unidades");
        Console.WriteLine("3. Nombre de productos por marcas ordenados por nombre de producto");
        Console.WriteLine("4. Agrupación de departamentos con subdepartamentos y nombres de productos");
        Console.WriteLine("5. Regresar");
        
        switch (Console.ReadLine())
        {
            case "1":
                Top5ProductosMasCaros();
                break;
            case "2":
                ProductosCon5UnidadesOMenos();
                break;
            case "3":
                ProductosPorMarca();
                break;
            case "4":
                DepartamentosConSubdepartamentosYProductos();
                break;
            default:
                return;
        }

        Console.ReadLine();
    }

    private void DepartamentosConSubdepartamentosYProductos()
    {
        var data = _reportService.GetDepartamentosConSubdepartamentosYProductos();

        foreach (var deparment in data.GroupBy(c => c.Department))
        {
            Console.WriteLine("Departamento: " + deparment.Key);

            foreach (var subdeparment in deparment.GroupBy(c => c.Subdeparment))
            {
                Console.WriteLine("Subdepartamento: " + subdeparment.Key);

                foreach (var p in subdeparment)
                {
                    Console.WriteLine(p.Name);
                }
            }
        }  
    }

    private void ProductosPorMarca()
    {
        var data = _reportService.GetProductosPorMarca();

        foreach (var dto in data.GroupBy(c => c.Brand))
        {
            Console.WriteLine(dto.Key);

            foreach (var p in dto)
            {
                Console.WriteLine(p.Name);
            }
        }  
    }

    private void ProductosCon5UnidadesOMenos()
    {
        var data = _reportService.GetTop5ProductosMasCaros();

        foreach (var dto in data)
        {
            Console.WriteLine($"{dto.Name} {dto.Price}");
        }    
    }

    private void Top5ProductosMasCaros()
    {
        var data = _reportService.GetProductosCon5UnidadesOMenos();

        foreach (var dto in data)
        {
            Console.WriteLine($"{dto.Name} {dto.Units}");
        }
    }
}
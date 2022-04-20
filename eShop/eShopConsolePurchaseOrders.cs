using System.Linq;
using Business;
using Data.Entities;
using Data.Enums;

namespace eShop;

public partial class eShopConsole
{
    private void MenuDeOrdenesDeCompra()
    {
        Console.WriteLine("Elige una opción:");
        Console.WriteLine("1. Agregar orden de compra");
        Console.WriteLine("2. Consultar ordenes de compra");
        Console.WriteLine("3. Cambiar estatus");
        Console.WriteLine("N. Regresar");

        switch (Console.ReadLine())
        {
            case "1":
                AgregarOrdenDeCompra();
                break;
            case "2":
                ConsultarOrdenesDeCompra();
                break;
            case "3":
                CambiarEstatusOrdenCompra();
                break;
            default:
                return;
        }
    }

    private void CambiarEstatusOrdenCompra()
    {
        Console.WriteLine("¿A qué órden quieres cambiarle el estatus?");
        var poId = IntentarObtenerInt(Console.ReadLine());

        Console.WriteLine("A qué estátus quieres cambiarlo?");
        foreach (var status in Enum.GetNames<PurchaseOrderStatus>())
        {
            Console.WriteLine(status);
        }
        var statusAux = Console.ReadLine();

        PurchaseOrderStatus newStatus;
        var didParse = Enum.TryParse(statusAux, out newStatus);
        if (didParse)
        {
            var po = _purchaseOrderService.ChangeStatus(poId, newStatus);
            // actualizar (sumar) el stock de los productos originales que fueron comprados por la orden de compra
            // que haya sido pagada.
            Console.WriteLine("Orden de compra actualizada correctamente");
        }
        else
        {
            Console.WriteLine("El estátus solicitado no existe");
        }
        Console.ReadLine();
    }

    private void ConsultarOrdenesDeCompra()
    {
        foreach (var purchaseOrder in _purchaseOrderService.GetPurchaseOrders())
        {
            Console.WriteLine(purchaseOrder);
        }

        Console.ReadLine();
    }

    private void AgregarOrdenDeCompra()
    {
        Console.WriteLine("Elige un proveedor:");
        // mostrar los departamentos
        var providers = TestData.GetProviders();
        for (int i = 0; i < providers.Count; i++)
        {
            Console.WriteLine($"{i+1}. {providers.ElementAt(i).Name}");
        }
        var providerPosition = IntentarObtenerInt(Console.ReadLine()) - 1;
        var provider = providers.ElementAt(providerPosition);
        
        var products = _productService.GetProducts();
        var productsOfOrder = new List<Product>();
        do
        {
            productsOfOrder.Add(AgregarProductosAOrden(products));

            Console.WriteLine("Agregar más productos? S/N");
            var response = Console.ReadLine();
            if(response.ToLower() is "n")
                break;
        } 
        while (true);

        var order = new PurchaseOrder(provider, productsOfOrder);
        _purchaseOrderService.AddPurchaseOrder(order);
        Console.WriteLine("Orden de compra agregada correctamente");
        Console.ReadLine();
    }

    private Product AgregarProductosAOrden(List<Product> products)
    {
        Console.WriteLine("Agrega un producto:");
        for (int i = 0; i < products.Count; i++)
        {
            Console.WriteLine($"{i+1}. {products.ElementAt(i).Name}");
        }
        var productPosition = IntentarObtenerInt(Console.ReadLine()) - 1;
        var product = products.ElementAt(productPosition);

        var productAux = new Product(product.Id, product.Name, product.Price, product.Description, product.Brand, product.Sku);
        Console.WriteLine("¿Cuȧntas unidades?");
        productAux.UpdateStock(IntentarObtenerInt(Console.ReadLine()));
        return productAux;
    }
    
}
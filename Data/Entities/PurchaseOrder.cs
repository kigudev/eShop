using System.Text;
using Data.Enums;

namespace Data.Entities;

public class PurchaseOrder
{
    public int Id { get; set; }
    public decimal Total { get; private set; }
    public DateTime PurchaseDate { get; private set; }
    public Provider Provider { get; private set; }
    public List<Product> PurchasedProducts { get; private set; }
    public PurchaseOrderStatus Status { get; private set; }

    private static int consecutiveNumber = 1;
    public PurchaseOrder(Provider provider, List<Product> purchasedProducts, DateTime? purchaseDate = null)
    {
        if(provider == null)
            throw new ArgumentNullException("El proveedor no puede ser vacio");
        
        if(purchasedProducts == null || !purchasedProducts.Any())
            throw new ArgumentNullException("Hay que agregar productos a la orden");

        Status = PurchaseOrderStatus.Pending;
        Id = consecutiveNumber++;
        Total = purchasedProducts.Sum(c => c.Price * c.Stock);
        Provider = provider;
        PurchasedProducts = purchasedProducts;
        PurchaseDate = purchaseDate ?? DateTime.Now;
    }
    
    public override string ToString()
    {
        var orderString = new StringBuilder();
        orderString.Append($"Orden de compra #{Id} con estátus {Status.ToString().ToUpper()} hecha a {Provider.Name} con {PurchasedProducts.Count} productos por un total de {Total:C}. Desglose:\n");
        foreach (var purchasedProduct in PurchasedProducts)
        {
            orderString.Append($"{purchasedProduct.Name} - {purchasedProduct.Stock:N0} unidades = {purchasedProduct.Stock * purchasedProduct.Price:C}\n");
        }

        return orderString.ToString();
    }

    public void ChangeStatus(PurchaseOrderStatus status)
    {
        Status = status;
    }
}
using Business.Services.Abstractions;
using Data.Entities;
using Data.Enums;

namespace Business.Services.Implementations;

public class PurchaseOrderService: IPurchaseOrderService
{
    private List<PurchaseOrder> _purchaseOrders = new List<PurchaseOrder>();
    
    public void AddPurchaseOrder(PurchaseOrder purchaseOrder) => _purchaseOrders.Add(purchaseOrder);

    public List<PurchaseOrder> GetPurchaseOrders() => _purchaseOrders;
    public PurchaseOrder ChangeStatus(int purchaseOrderId, PurchaseOrderStatus status)
    {
        var po = _purchaseOrders.FirstOrDefault(c => c.Id == purchaseOrderId);

        if (po != null)
        {
            po.ChangeStatus(status);
            return po;
        }

        throw new ApplicationException("No se encontró la orden solicitada");
    }
}
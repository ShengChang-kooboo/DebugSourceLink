using Chapter1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chapter1.Abstract
{
    public interface IMockDataSource
    {
        IEnumerable<SellableItem> GetSellableItems();
        SellableItem GetSellableItemByBarcode(string barcode);
        Task<SellableItem> AddSellableItem(SellableItem sellableItem);


        Task<IEnumerable<CustomCustomer>> GetCustomCustomersAsync();

        Task<IEnumerable<CustomOrder>> GetCustomOrdersAsync();
        
        Task<CustomCustomer> GetCustomCustomerByIdAsync(int customerId);

        Task<IEnumerable<CustomOrder>> GetOrdersByCustomCustomerIdAsync(int customerId);

        Task<CustomCustomer> AddCustomCustomerAsync(CustomCustomer customCustomer);

        Task<CustomOrder> AddCustomOrderAsync(CustomOrder customOrder);


        Task<CustomOrder> GetCustomOrderByIdAsync(int orderId);
        Task<CustomOrderSellableItemRelation> AddCustomOrderSellableItemRelationAsync(CustomOrderSellableItemRelation relation);
        Task<IEnumerable<CustomOrderSellableItemRelation>> GetCustomOrderSellableItemRelationByOrderIdAsync(int orderId);
    }
}

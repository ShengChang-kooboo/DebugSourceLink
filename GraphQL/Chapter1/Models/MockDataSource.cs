using Chapter1.Abstract;
using Chapter1.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chapter1.Models
{
    public class MockDataSource : IMockDataSource
    {
        #region Members.
        private ApplicationDbContext _applicationDbContext;
        #endregion

        #region Constructors.
        public MockDataSource(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        #endregion

        #region Methods.
        public IEnumerable<SellableItem> GetSellableItems()
        {
            return _applicationDbContext.SellableItems;
        }

        public SellableItem GetSellableItemByBarcode(string barcode)
        {
            return _applicationDbContext.SellableItems.FirstOrDefault(i => i.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<SellableItem> AddSellableItem(SellableItem sellableItem)
        {
            var addItem = await _applicationDbContext.SellableItems.AddAsync(sellableItem);
            await _applicationDbContext.SaveChangesAsync();
            return addItem.Entity;
        }


        public async Task<IEnumerable<CustomCustomer>> GetCustomCustomersAsync()
        {
            return await _applicationDbContext.CustomCustomer
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<IEnumerable<CustomOrder>> GetCustomOrdersAsync()
        {
            return await _applicationDbContext.CustomOrder
                            .AsNoTracking()
                            .ToListAsync();
        }

        public async Task<CustomCustomer> GetCustomCustomerByIdAsync(int customerId)
        {
            return await _applicationDbContext.CustomCustomer.FindAsync(customerId);
        }

        public async Task<IEnumerable<CustomOrder>> GetOrdersByCustomCustomerIdAsync(int customerId)
        {
            return await _applicationDbContext.CustomOrder
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<CustomCustomer> AddCustomCustomerAsync(CustomCustomer customCustomer)
        {
            var addedCustomer = await _applicationDbContext.CustomCustomer.AddAsync(customCustomer);
            await _applicationDbContext.SaveChangesAsync();
            return addedCustomer.Entity;
        }

        public async Task<CustomOrder> AddCustomOrderAsync(CustomOrder customOrder)
        {
            var addedOrder = await _applicationDbContext.CustomOrder.AddAsync(customOrder);
            await _applicationDbContext.SaveChangesAsync();
            return addedOrder.Entity;
        }

        public async Task<CustomOrder> GetCustomOrderByIdAsync(int orderId)
        {
            return await _applicationDbContext.CustomOrder
                .Where(o => o.OrderId == orderId)
                .FirstOrDefaultAsync();
        }

        public async Task<CustomOrderSellableItemRelation> AddCustomOrderSellableItemRelationAsync(CustomOrderSellableItemRelation relation)
        {
            var addedOrderSellableItemRelation = await _applicationDbContext.CustomOrderSellableItemRelation.AddAsync(relation);
            await _applicationDbContext.SaveChangesAsync();
            return addedOrderSellableItemRelation.Entity;
        }

        public async Task<IEnumerable<CustomOrderSellableItemRelation>> GetCustomOrderSellableItemRelationByOrderIdAsync(int orderId)
        {
            return await _applicationDbContext.CustomOrderSellableItemRelation
                .Where(o => o.OrderId == orderId)
                .ToListAsync();
        }
        #endregion
    }

    //public class MockDataSource
    //{
    //    #region Members.
    //    public IList<SellableItem> SellableItems { get; set; }
    //    #endregion

    //    #region Constructors.
    //    public MockDataSource()
    //    {
    //        SellableItems = new List<SellableItem>
    //        {
    //            new SellableItem{ Barcode="123", Title="Headphone", SellingPrice=50},
    //            new SellableItem{ Barcode="456", Title="Keyboard", SellingPrice=40},
    //            new SellableItem{ Barcode="789", Title="Monitor", SellingPrice=100}
    //        };
    //    }
    //    #endregion

    //    #region Methods.
    //    public SellableItem GetSellableItemByBarcode(string barcode)
    //    {
    //        return SellableItems.FirstOrDefault(i => i.Barcode.Equals(barcode, StringComparison.OrdinalIgnoreCase));
    //    }
    //    #endregion
    //}
}

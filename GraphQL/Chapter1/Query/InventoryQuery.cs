using Chapter1.Abstract;
using Chapter1.DAL;
using Chapter1.Models;
using Chapter1.Models.GraphqlType;
using GraphQL.Types;
using System.Collections;
using System.Collections.Generic;

namespace Chapter1.Query
{
    /// <summary>
    /// 提供Query Schema，让GraphQL Clients进行查询
    /// </summary>
    public class InventoryQuery : ObjectGraphType
    {
        #region Constructors.
        public InventoryQuery(ApplicationDbContext applicationDbContext, IMockDataSource mockDataSource)
        {
            Field<SellableItemType>(name: "inventoryItem", description: "Search inventoryitems",
                        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "barcode" }),
                        resolve: fieldContext =>
                        {
                            var strBarcodeArg = fieldContext.GetArgument<string>("barcode");
                            return new MockDataSource(applicationDbContext).GetSellableItemByBarcode(strBarcodeArg);
                        });
            /*查询示例（感觉GraphQL查询比较方便，可用Mutation增加数据但操作步骤多。）
query testGraphQLInNET($barcode: String!){
  inventoryItem (barcode: $barcode){
    sellingPrice
    title
  } 
}

mutation {
  createInventoryItem(newItem: {title: "GPU", barcode: "112", sellingPrice: 3.55555}){
    title
    barcode
  }
}
             */


            Field<ListGraphType<CustomCustomerType>, IEnumerable<CustomCustomer>>()
                .Name("AllCustomers")
                .ResolveAsync(fieldContext =>
                {
                    return mockDataSource.GetCustomCustomersAsync();
                });
            Field<ListGraphType<CustomOrderType>, IEnumerable<CustomOrder>>()
                .Name("AllOrders")
                .ResolveAsync(fieldContext =>
                {
                    return mockDataSource.GetCustomOrdersAsync();
                });
        }
        #endregion
    }
}

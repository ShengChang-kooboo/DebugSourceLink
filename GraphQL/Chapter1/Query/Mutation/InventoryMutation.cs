using Chapter1.Abstract;
using Chapter1.Models;
using Chapter1.Models.GraphqlType;
using GraphQL.Types;

namespace Chapter1.Query.Mutation
{
    /// <summary>
    /// 提供Mutation Schema，让GraphQL Clients可调用添加SellableItem API
    /// </summary>
    public class InventoryMutation: ObjectGraphType
    {
        #region Constructors.
        public InventoryMutation(IMockDataSource mockDataSource)
        {
            Field<SellableItemType>(name: "createInventoryItem", description: "Create inventoryitems",
                        arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<SellableItemInputType>>() { Name="newItem"}
                        ),
                        resolve: fieldContext =>
                        {
                            var newSellableItem = fieldContext.GetArgument<SellableItem>("newItem");
                            return mockDataSource.AddSellableItem(newSellableItem);
                        });
            Field<CustomCustomerType>(name: "createCustomCustomer",
                        arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<CustomCustomerInputType>>() { Name="newCustomer"}
                        ),
                        resolve: fieldContext =>
                        {
                            var newCustomer = fieldContext.GetArgument<CustomCustomer>("newCustomer");
                            return mockDataSource.AddCustomCustomerAsync(newCustomer);
                        });
            Field<CustomOrderType>(name: "createCustomOrder",
                        arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<CustomOrderInputType>>() { Name = "newOrder" }
                        ),
                        resolve: fieldContext =>
                        {
                            var newOrder = fieldContext.GetArgument<CustomOrder>("newOrder");
                            return mockDataSource.AddCustomOrderAsync(newOrder);
                        });
            Field<CustomOrderSellableItemRelationType>(name: "addCustomOrderSellableItemRelation"
                        ,arguments: new QueryArguments
                        (
                            new QueryArgument<NonNullGraphType<CustomOrderSellableItemRelationInputType>>()
                            {
                                Name = "customOrderSellableItemRelation"
                                ,DefaultValue = "input the relation of customOrder and sellableItem"
                            }
                        ),
                        resolve: fieldContext =>
                        {
                            var newOrderAndSellableItemRelation = fieldContext.GetArgument<CustomOrderSellableItemRelation>("customOrderSellableItemRelation");
                            return mockDataSource.AddCustomOrderSellableItemRelationAsync(newOrderAndSellableItemRelation);
                        }
                        );
        }
        #endregion
    }
}

using Chapter1.Query;
using Chapter1.Query.Mutation;
using GraphQL;
using GraphQL.Types;

namespace Chapter1.CustomSchema
{
    public class InventorySchema:Schema
    {
        #region Constructors.
        /*!!!!不清楚问题原因，也不清楚解决办法 Original Version: https://www.cnblogs.com/lwqlun/p/9949559.html*/
        public InventorySchema(IDependencyResolver dependencyResolver)
            : base(dependencyResolver)
        {
            base.Query = dependencyResolver.Resolve<InventoryQuery>();
            base.Mutation = dependencyResolver.Resolve<InventoryMutation>();
        }

        //public InventorySchema(InventoryQuery inventoryQuery, InventoryMutation inventoryMutation)
        //{
        //    base.Query = inventoryQuery;
        //    base.Mutation = inventoryMutation;
        //}
        #endregion
    }
}

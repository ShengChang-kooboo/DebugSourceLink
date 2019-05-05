using Chapter1.DAL;
using Chapter1.Models;
using Chapter1.Models.GraphqlType;
using GraphQL.Types;

namespace Chapter1.Query
{
    /// <summary>
    /// 提供Query Schema，让GraphQL Clients进行查询
    /// </summary>
    public class HelloGraphQL : ObjectGraphType
    {
        #region Constructors.
        public HelloGraphQL(ApplicationDbContext applicationDbContext)
        {
            Field<StringGraphType>(name: "hello", resolve: fieldContext => "world of GraphQL");
            Field<StringGraphType>(name: "howdy", resolve: fieldContext => "universe");

            /*为什么GraphQL查询field名称，必须要从小写字母开头？强制驼峰命名吗？！*/
            //Field<SellableItemType>(name: "productItem", resolve: fieldContext =>
            //{
            //    return new SellableItem
            //    {
            //        Barcode = "123",
            //        Title = "Headphone",
            //        SellingPrice = 99.01M
            //    };
            //});
            Field<SellableItemType>(name: "productItem", description: "Search sellableitems",
                        arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "barcode" }),
                        resolve: fieldContext =>
                        {
                            var strBarcodeArg = fieldContext.GetArgument<string>("barcode");
                            return new MockDataSource(applicationDbContext).GetSellableItemByBarcode(strBarcodeArg);
                        });
        }
        #endregion
    }
}

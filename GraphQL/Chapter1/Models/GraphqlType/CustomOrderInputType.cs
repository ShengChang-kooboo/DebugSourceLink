using GraphQL.Types;

namespace Chapter1.Models.GraphqlType
{
    /// <summary>
    /// 定义输入类型
    /// </summary>
    public class CustomOrderInputType:InputObjectGraphType
    {
        #region Constructors.
        public CustomOrderInputType()
        {
            base.Name = "CustomOrderInput";
            Field<NonNullGraphType<StringGraphType>>("tag");
            Field<NonNullGraphType<DateGraphType>>("createAt");
            Field<NonNullGraphType<IntGraphType>>("customerId");
        }
        #endregion
    }
}

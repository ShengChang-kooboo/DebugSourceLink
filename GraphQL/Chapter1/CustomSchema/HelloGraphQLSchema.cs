using Chapter1.Query;
using GraphQL.Types;

namespace Chapter1.CustomSchema
{
    public class HelloGraphQLSchema:Schema
    {
        #region Constructors.
        public HelloGraphQLSchema(HelloGraphQL helloGraphQLQuery)
        {
            base.Query = helloGraphQLQuery;
        }
        #endregion
    }
}

using Newtonsoft.Json.Linq;

namespace Chapter1.Models
{
    public class GraphQLRequest
    {
        #region Members.
        public string Query { get; set; }
        public JObject Variables { get; set; }
        #endregion
    }
}

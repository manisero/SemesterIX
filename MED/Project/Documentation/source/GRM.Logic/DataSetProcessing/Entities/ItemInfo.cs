using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public class ItemInfo
    {
        public int AttributeID { get; set; }

        public int ValueID { get; set; }

        public IList<int> TransactionIDs { get; set; }

        public bool IsDecisive { get; set; }

        public int DecisionID { get; set; }
    }
}
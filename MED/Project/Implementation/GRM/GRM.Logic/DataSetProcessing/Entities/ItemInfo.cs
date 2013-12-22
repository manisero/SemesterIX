using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public struct ItemInfo
    {
        public IList<int> TransactionIDs { get; set; }

        public bool IsDecisive { get; set; }

        public string Decision { get; set; }
    }
}
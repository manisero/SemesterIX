using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public class DataSet
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}

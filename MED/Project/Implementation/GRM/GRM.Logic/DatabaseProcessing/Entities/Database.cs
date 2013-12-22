using System.Collections.Generic;

namespace GRM.Logic.DatabaseProcessing.Entities
{
    public class DataSet
    {
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}

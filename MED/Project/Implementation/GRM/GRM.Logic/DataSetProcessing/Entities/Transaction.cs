using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public class Transaction
    {
        public int TransactionID { get; set; }

        public IEnumerable<string[]> Items { get; set; }
    }
}
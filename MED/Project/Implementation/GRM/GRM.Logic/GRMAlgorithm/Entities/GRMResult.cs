using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class GRMResult
    {
        public IEnumerable<Rule> Rules { get; set; }
    }

    public class Rule
    {
        public string Decision { get; set; }

        public IList<IEnumerable<Item>> Generators { get; set; }
    }
}
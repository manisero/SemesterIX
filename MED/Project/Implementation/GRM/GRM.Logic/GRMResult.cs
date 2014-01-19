using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic
{
    public class GRMResult
    {
        public int AttributesCount { get; set; }

        public IDictionary<int, string> AttributeNames { get; set; }

        public int DecisionAttributeIndex { get; set; }

        public IEnumerable<Rule> Rules { get; set; }
    }

    public class Rule
    {
        public string Decision { get; set; }

        public IList<IEnumerable<Item>> Generators { get; set; }
    }
}
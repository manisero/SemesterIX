using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic
{
    public class GRMResult
    {
        public IEnumerable<Rule> Rules { get; set; }
    }

    public class Rule
    {
        public IEnumerable<Item> Items { get; set; }

        public string Decision { get; set; }
    }
}
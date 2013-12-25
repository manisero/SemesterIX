using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Generator : List<ItemID>
    {
        public Generator()
        {

        }

        public Generator(ItemID itemID)
        {
            Add(itemID);
        }

        public Generator(Generator otherGenerator)
            : base(otherGenerator)
        {

        }
    }
}

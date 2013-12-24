using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class Generator : List<ItemID>
    {
        public Generator()
        {

        }

        public Generator(Generator otherGenerator)
            : base(otherGenerator)
        {

        }
    }
}

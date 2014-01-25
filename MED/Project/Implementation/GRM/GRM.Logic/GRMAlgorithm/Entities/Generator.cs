using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Generator : List<ItemID>
    {
        private bool _identifierComputed;
        private int _identifier;

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

        public int GetIdentifier()
        {
            if (!_identifierComputed)
            {
                foreach (var item in this)
                {
                    _identifier += item.ValueID;
                }

                _identifierComputed = true;
            }

            return _identifier;
        }
    }
}

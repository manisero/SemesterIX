using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Generator : List<ItemID>
    {
        private bool _identifierComputed;
        private long _identifier;

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

        public long GetIdentifier()
        {
            if (!_identifierComputed)
            {
                long hash = this.Count;

                foreach (var item in this)
                {
                    hash = unchecked(hash * 314159L + item.ValueID);
                }

                _identifier = hash;
                _identifierComputed = true;
            }

            return _identifier;
        }
    }
}

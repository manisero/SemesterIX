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
                this.Sort((item1, item2) => item1.ValueID - item2.ValueID);
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

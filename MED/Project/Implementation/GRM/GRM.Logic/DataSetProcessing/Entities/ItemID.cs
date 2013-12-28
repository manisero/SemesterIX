namespace GRM.Logic.DataSetProcessing.Entities
{
    public struct ItemID
    {
        public int AttributeID { get; set; }

        public int ValueID { get; set; }

        public override string ToString()
        {
            return string.Format("({0},{1})", AttributeID, ValueID);
        }
    }
}
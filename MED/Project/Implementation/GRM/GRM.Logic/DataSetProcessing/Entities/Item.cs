namespace GRM.Logic.DataSetProcessing.Entities
{
    public struct Item
    {
        public int AttributeID { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return string.Format("({0},{1})", AttributeID, Value);
        }
    }
}

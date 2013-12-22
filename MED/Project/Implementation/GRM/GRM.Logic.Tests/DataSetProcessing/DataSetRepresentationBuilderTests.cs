using System.IO;
using System.Text;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing.Entities;
using Xunit;

namespace GRM.Logic.Tests.DataSetProcessing
{
    public class DataSetRepresentationBuilderTests : TestsBase
    {
        private DataSetRepresentation Execute(string dataSet, ITransactionProcessor transactionProcessor)
        {
            var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(dataSet));

            var reader = new StreamReader(dataSetStream);
            var result = new DataSetRepresentation();

            for (int i = 1; !reader.EndOfStream; i++)
            {
                var transaction = reader.ReadLine();
            }

            reader.Dispose();
            dataSetStream.Dispose();

            return result;
        }

        [Fact]
        public void represents_single_transaction()
        {
            var result = Execute("value1,value2,decision\n", null);
        }
    }
}

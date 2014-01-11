using System.IO;
using System.Text;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.DataSetProcessing._Impl;
using Moq;
using Xunit;

namespace GRM.Logic.UnitTests.DataSetProcessing
{
    public class DataSetRepresentationBuilderTests : TestsBase
    {
        private DataSetRepresentation Execute(string dataSet)
        {
            using (var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(dataSet)))
            {
                return AutoMoqer.Resolve<DataSetRepresentationBuilder>().Build(dataSetStream, false, null);
            }
        }

        [Fact]
        public void processes_single_transaction()
        {
            // Arrange
            var dataSet = "value1,value2,decision";

            AutoMoqer.GetMock<ITransactionProcessor>().Setup(x => x.AppendTransaction(1, dataSet, 2, It.IsAny<DataSetRepresentationBuildState>()));

            // Act
            var result = Execute(dataSet);

            // Assert
            Assert.Equal(3, result.AttributesCount);
            Assert.Equal(2, result.DecisiveAttributeIndex);
            Assert.Equal(null, result.AttributeNames);

            AutoMoqer.GetMock<ITransactionProcessor>().VerifyAll();
        }
    }
}

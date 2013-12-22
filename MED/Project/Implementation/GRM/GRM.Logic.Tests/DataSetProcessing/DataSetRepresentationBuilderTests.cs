using System.IO;
using System.Text;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.DataSetProcessing._Impl;
using Moq;
using Xunit;

namespace GRM.Logic.Tests.DataSetProcessing
{
    public class DataSetRepresentationBuilderTests : TestsBase
    {
        private DataSetRepresentation Execute(string dataSet)
        {
            using (var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(dataSet)))
            {
                return AutoMoqer.Resolve<DataSetRepresentationBuilder>().Build(dataSetStream);
            }
        }

        [Fact]
        public void processes_single_transaction()
        {
            // Arrange
            var dataSet = "value1,value2,decision";

            AutoMoqer.GetMock<ITransactionProcessor>().Setup(x => x.AppendTransaction(1, dataSet, It.IsAny<DataSetRepresentationBuildState>()));

            // Act
            Execute(dataSet);

            // Assert
            AutoMoqer.GetMock<ITransactionProcessor>().VerifyAll();
        }
    }
}

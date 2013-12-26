using System.IO;
using System.Text;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests
{
    public class GRMFacadeTests
    {
        private GRMResult Execute(string dataSet, int minimumSupport, SortingStrategyType sortingStrategy)
        {
            using (var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(dataSet)))
            {
                return new GRMFacade().ExecuteGRM(dataSetStream, minimumSupport, sortingStrategy, new ProgressInfo());
            }
        }

        [Fact]
        public void processes_car_data_set()
        {
            // Act
            var result = Execute(Resources.CarDataSet, 10, SortingStrategyType.DescendingSupport);

            // Assert
            Assert.Equal(2, result.Rules.Count());
        }
    }
}

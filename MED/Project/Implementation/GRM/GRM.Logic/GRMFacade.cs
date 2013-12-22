using System.IO;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing._Impl;

namespace GRM.Logic
{
    public class GRMFacade
    {
        public void ExecuteGRM(string dataFilePath, int minimumSupport, ProgressInfo progressInfo)
        {
            progressInfo.BeginTask();

            var stream = new FileStream(dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            progressInfo.BeginStep("Creating data set representation");
            var representation = new DataSetRepresentationBuilder(new TransactionProcessor()).Build(stream);
            progressInfo.EndStep();

            progressInfo.BeginStep("Selecting frequent items");
            var frequentItems = new FrequentItemsSelector().SelectFrequentItems(representation.ItemInfos.Values, minimumSupport);
            progressInfo.EndStep();

            progressInfo.EndTask();
        }
    }
}

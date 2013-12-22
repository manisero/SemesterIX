using System.IO;
using GRM.Logic.DataSetProcessing._Impl;

namespace GRM.Logic
{
    public class GRMFacade
    {
        public void ExecuteGRM(string dataFilePath, int minimumSupport)
        {
            var stream = new FileStream(dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            var representation = new DataSetRepresentationBuilder(new TransactionProcessor()).Build(stream);

            var frequentItems = new FrequentItemsSelector().SelectFrequentItems(representation.ItemInfos.Values, minimumSupport);
        }
    }
}

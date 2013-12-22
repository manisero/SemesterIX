using System.IO;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing._Impl
{
    public class DataSetRepresentationBuilder : IDataSetRepresentationBuilder
    {
        private readonly ITransactionProcessor _transactionProcessor;

        public DataSetRepresentationBuilder(ITransactionProcessor transactionProcessor)
        {
            _transactionProcessor = transactionProcessor;
        }

        public DataSetRepresentation Build(Stream dataSetStream)
        {
            var buildState = new DataSetRepresentationBuildState();

            using (var reader = new StreamReader(dataSetStream))
            {
                for (int i = 1; !reader.EndOfStream; i++)
                {
                    var transaction = reader.ReadLine();
                    _transactionProcessor.AppendTransaction(i, transaction, buildState);
                }
            }

            return new DataSetRepresentation
                {
                    ItemIDs = buildState.ItemIDs,
                    ItemTransactions = buildState.ItemTransactions
                };
        }
    }
}
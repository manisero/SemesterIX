using System.Collections.Generic;
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

        public DataSetRepresentation Build(Stream dataSetStream, bool dataContainsHeaders, int? decisiveAttributeIndex)
        {
            var buildState = new DataSetRepresentationBuildState();
            int attributesCount;
            int decisionIndex;
            IDictionary<int, string> attributeNames = null;

            using (var reader = new StreamReader(dataSetStream))
            {
                if (dataContainsHeaders)
                {
                    var headers = reader.ReadLine().Split(',');

                    for (int i = 0; i < headers.Length; i++)
                    {
                        attributeNames.Add(i, headers[i]);
                    }
                }

                var firstTransaction = reader.ReadLine();
                attributesCount = firstTransaction.Split(',').Length;
                decisionIndex = decisiveAttributeIndex ?? attributesCount - 1;
                _transactionProcessor.AppendTransaction(1, firstTransaction, decisionIndex, buildState);

                for (int i = 2; !reader.EndOfStream; i++)
                {
                    var transaction = reader.ReadLine();
                    _transactionProcessor.AppendTransaction(i, transaction, decisionIndex, buildState);
                }
            }

            return new DataSetRepresentation
                {
                    AttributesCount = attributesCount,
                    DecisiveAttributeIndex = decisionIndex,
                    AttributeNames = attributeNames,
                    DecisionIDs = buildState.DecisionIDs,
                    TransactionDecisions = buildState.TransactionDecisions,
                    ItemIDs = buildState.ItemIDs,
                    ItemInfos = buildState.ItemInfos
                };
        }
    }
}
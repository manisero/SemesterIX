using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing
{
    public interface ITransactionProcessor
    {
        DataSetRepresentation Build(IEnumerable<ConcreteItem> dataSet);
    }
}
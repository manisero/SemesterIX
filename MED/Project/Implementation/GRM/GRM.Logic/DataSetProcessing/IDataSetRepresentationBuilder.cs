using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing
{
    public interface IDataSetRepresentationBuilder
    {
        DataSetRepresentation Build(IEnumerable<ConcreteItem> dataSet);
    }
}
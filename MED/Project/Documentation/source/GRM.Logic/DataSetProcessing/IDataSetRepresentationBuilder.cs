using System.IO;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing
{
    public interface IDataSetRepresentationBuilder
    {
        DataSetRepresentation Build(Stream dataSetStream, bool dataContainsHeaders, int? decisionAttributeIndex);
    }
}

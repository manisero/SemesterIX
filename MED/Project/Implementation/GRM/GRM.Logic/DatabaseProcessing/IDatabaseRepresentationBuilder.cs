using System.Collections.Generic;
using GRM.Logic.DatabaseProcessing.Entities;

namespace GRM.Logic.DatabaseProcessing
{
    public interface IDatabaseRepresentationBuilder
    {
        DatabaseRepresentation Build(IEnumerable<ConcreteItem> database);
    }
}
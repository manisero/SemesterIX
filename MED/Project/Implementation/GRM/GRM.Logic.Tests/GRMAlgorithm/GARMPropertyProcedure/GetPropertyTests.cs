using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.Tests.GRMAlgorithm.GARMPropertyProcedure
{
    public class GetPropertyTests
    {
        private GARMPropertyType Execute(IEnumerable<int> leftChildTransactionIds, IEnumerable<int> rightChildTransactionIds)
        {
            return new Logic.GRMAlgorithm._Impl.GARMPropertyProcedure().GetProperty(leftChildTransactionIds, rightChildTransactionIds);
        }


    }
}

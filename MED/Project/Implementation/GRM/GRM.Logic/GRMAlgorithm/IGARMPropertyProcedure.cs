using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IGARMPropertyProcedure
    {
        void Execute(Node parent, Node leftChild, Node rightChild);
    }
}
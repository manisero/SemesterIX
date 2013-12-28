using AutoMoq;

namespace GRM.Logic.UnitTests
{
    public abstract class TestsBase
    {
        protected AutoMoqer AutoMoqer { get; set; }

        protected TestsBase()
        {
            AutoMoqer = new AutoMoqer();
        }
    }
}

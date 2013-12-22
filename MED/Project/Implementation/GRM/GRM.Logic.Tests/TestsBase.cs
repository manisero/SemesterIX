using AutoMoq;

namespace GRM.Logic.Tests
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

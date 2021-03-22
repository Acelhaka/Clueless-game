using Microsoft.VisualStudio.TestTools.UnitTesting;
using CluelessBackend.Core.Board;

namespace CluelessBackend.CluelessTests.BackendTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Board board = new Board();
            Assert.AreEqual("1", "1", "Not equal");
        }
    }
}
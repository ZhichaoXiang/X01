using BrunieBCL.Models.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


123
namespace BrunieBCL.UnitTest {
    [TestClass]
    public class SortTest {
        [TestMethod]
        public void Test() {
            int[] test = { 1, 2 };
            test = test.OrderBy(x => x, new RelayComparer<int>((x1, x2) => {
                return (x2 - x1);
                })).ToArray();
            int a = 0;
        }
    }
}

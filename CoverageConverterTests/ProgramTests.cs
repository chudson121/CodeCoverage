using CoverageConverter;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoverageConverterTests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void MainTest_NullArgsReturn1()
        {
            var retval = Program.Main(null);
            Assert.AreEqual(1, retval);
        }

        [TestMethod()]
        public void MainTest_LessThan2ArgsReturn1()
        {
            var args = new string[0];

            var retval = Program.Main(args);
            Assert.AreEqual(1, retval);

        }

        [TestMethod()]
        public void MainTest_IfSourcePathDoesntExistReturn1()
        {
            string[] args = { "C:\\DoesNotExist", "C:\\DoesNotExist" };

            var retval = Program.Main(args);
            Assert.AreEqual(1, retval);

        }

        [TestMethod()]
        public void MainTest_IfSourceFileDoesntExistReturn1()
        {
            string[] args = { "C:\\TestResults", "C:\\DoesNotExist" };

            var retval = Program.Main(args);
            Assert.AreEqual(1, retval);

        }

    }
}
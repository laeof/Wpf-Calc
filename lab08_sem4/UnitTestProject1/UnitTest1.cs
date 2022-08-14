using Microsoft.VisualStudio.TestTools.UnitTesting;
using lab08_sem4;
namespace UnitTest_Calc
{
    [TestClass]
    public class UnitTest1
    {
        Calc calc = new Calc();

        [TestMethod]
        public void Test_Summ()
        {
            decimal x = 10, y = 20, rez = 30;
            decimal actual = calc.Add(x, y);
            Assert.AreEqual(rez, actual);
        }

        [TestMethod]
        public void Test_Minus()
        {
            decimal x = 40, y = 20, rez = 20;
            decimal actual = calc.Subtract(x, y);
            Assert.AreEqual(rez, actual);
        }


        [TestMethod]
        public void Test_Multiply()
        {
            decimal x = 5, y = 3, rez = 15;
            decimal actual = calc.Multiply(x, y);
            Assert.AreEqual(rez, actual);
        }

        [TestMethod]
        public void Test_Divide()
        {
            decimal x = 10, y = 20, rez = 0.5m;
            decimal actual = calc.Divide(x, y);
            Assert.AreEqual(rez, actual);
        }

        [TestMethod]
        public void Test_Cos()
        {
            decimal x = 60, rez = 0.5m;
            decimal actual = calc.cos(x);
            Assert.AreEqual(rez, actual);
        }

        [TestMethod]
        public void Test_Root()
        {
            decimal x = 16, rez = 4;
            decimal actual = calc.squareroot(x);
            Assert.AreEqual(rez, actual);
        }

        [TestMethod]
        public void Test_onedivx()
        {
            decimal x = 2, rez = 0.5m;
            decimal actual = calc.onedivx(x);
            Assert.AreEqual(rez, actual);
        }
    }
}


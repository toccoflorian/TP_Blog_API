using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogAPI.Helpers.AgeCalculHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Helpers.AgeCalculHelper.Tests
{
    [TestClass()]
    public class AgeCalculHelperTests
    {
        [TestMethod()]
        public void IsUserAdultTest()
        {
            DateTime dateTime = DateTime.Today;

            Assert.IsFalse(AgeCalculHelper.IsUserAdult(dateTime));
        }

        [TestMethod()]
        public void IsUserAdultTest2()
        {
            DateTime dateTime = DateTime.Today.AddYears(-18);

            Assert.IsTrue(AgeCalculHelper.IsUserAdult(dateTime));
        }
    }
}
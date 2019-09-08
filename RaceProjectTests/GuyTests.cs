using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaceProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaceProject.Tests
{
    [TestClass()]
    public class GuyTests
    {
        [TestMethod()]
        public void ClearBetTest()
        {
            Guy frm = new Guy();
            frm.ClearBet();
            Assert.Fail();
        }
    }
}
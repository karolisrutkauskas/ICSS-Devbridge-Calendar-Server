using DevBridgeAPI.UseCases.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBridgeAPI.Tests.UseCasesTest.UtilTest
{
    [TestClass]
    public class HashingUtilTest
    {
        [TestMethod]
        [DataRow("normal string 123")]
        [DataRow("Lietuviškas tekstas")]
        [DataRow("\u4E00\u4E9C interesting characters \u0000\u0002")]
        public void HashAndVerifyPassowrd_ShouldVerify(string password)
        {
            //

            var hash = HashingUtil.HashPasswordWithSalt(password);
            var verifyIsTrue = HashingUtil.VerifyPassword(password, hash);

            Assert.IsTrue(verifyIsTrue);
        }
    }
}

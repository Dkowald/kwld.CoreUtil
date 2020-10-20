using kwd.CoreUtil.Strings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace kwd.CoreUtil.Tests.String
{
    [TestClass]
    public class StringMatchTest
    {
        [TestMethod]
        public void SamePhrase_()
        {
            var lhs = "My name is   Inigo Montoya";
            var rhs = "MY name IS Inigo MONTOYA ";

            var isSame = lhs.SamePhrase(rhs);
            Assert.IsTrue(isSame);

            rhs += "!";
            lhs += "    \n";
            isSame = lhs.SamePhrase(rhs);
            Assert.IsFalse(isSame);
        }
    }
}

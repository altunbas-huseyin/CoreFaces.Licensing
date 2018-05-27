using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CoreFaces.Licensing.UnitTest
{
    [TestClass]
    public class LicenceTest
    {
        string domain = "domain.com";
        string type = "Status";
        [TestMethod]
        public void TestMethod1()
        {
            IHttpContextAccessor contextAccessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };

            string hashPass = this.GenerateDomainPass();
            Licence _licence = new Licence(type, contextAccessor, domain, hashPass);
            bool result = _licence.Verify(type, contextAccessor, domain, hashPass);
            Assert.AreEqual(result, true);
        }

        public string GenerateDomainPass()
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(domain + type);
            return passwordHash;
        }
    }
}

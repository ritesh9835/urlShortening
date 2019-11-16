using System.Net.Http;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using urlShortAPI.Controllers;

namespace urlShortAPI.Tests
{
    [TestClass]
    public class UrlDataTest
    {
        [TestMethod]
        public void SaveUrl()
        {
            // Set up Prerequisites   
        
            var controller = new ShortUrlController();
            controller.Request = new HttpRequestMessage();

            controller.Configuration = new HttpConfiguration();
            // Act on Test  
            var response = controller.SaveUrl("http://raitechintro.com");
            Assert.AreNotEqual(string.Empty, response);
        }

    }
}

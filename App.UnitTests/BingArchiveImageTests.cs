using System.IO;
using BingDesktop.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BingDesktop.UnitTests
{
    [TestClass]
    public class BingArchiveImageTests
    {
        private const string TestStartDate = "20200113";
        private const string TestImageFileName = "20200113_OHR.MtDiablo_EN-US7458508287_1920x1080.jpg";
        private const string TestImageUrl = "/th?id=OHR.MtDiablo_EN-US7458508287_1920x1080.jpg&rf=LaDigue_1920x1080.jpg&pid=hp";

        [TestMethod]
        public void Response_ParsedSuccessfully()
        {
            var json = File.ReadAllText(@"Collateral\ImageArchiveResponse.json");
            var response = JsonConvert.DeserializeObject<BingArchiveImageResponse>(json);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Images);
            Assert.AreEqual(1, response.Images.Count);

            var image = response.Images[0];
            Assert.IsNotNull(image);
            Assert.AreEqual(TestStartDate, image.StartDate);
            Assert.AreEqual(TestImageUrl, image.Url);
            Assert.AreEqual("401bd7ed6e3d19d4f83572af6db81bbe", image.Hash);
        }

        [TestMethod]
        public void ParseFileName_Successful()
        {
            var image = new BingArchiveImage
            {
                StartDate = TestStartDate,
                Url = TestImageUrl,
            };

            Assert.AreEqual(TestImageFileName, image.ParseFileName());
        }
    }
}

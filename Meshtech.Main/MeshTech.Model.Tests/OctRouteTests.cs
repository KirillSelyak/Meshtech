using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MeshTech.Model.Tests
{
    [TestClass]
    public class OctRouteTests
    {
        [DataTestMethod]
        [DataRow("29CBB8FAC688", 0, 1)]
        [DataRow("29CBB8FAC688", 4, 5)]
        [DataRow("29CBB8FAC688", 6, 7)]
        [DataRow("29CBB8FAC688", 7, 0)]
        [DataRow("29CBB8FAC688", 11, 4)]
        [DataRow("29CBB8FAC688", 15, 0)]
        public void GivenParse_TheGetValue_ReturnsRelevantValue(string hexRoute, int index, int expectedResult)
        {
            var route = CreateRoute(hexRoute);

            var actualResult = route.GetValue(index);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [DataTestMethod]
        [DataRow("29CBB8FAC688", "1234567076543210")]
        [DataRow("FFFFFFFFFFF8", "7777777777777770")]
        public void ToStringTest(string hexRoute, string expectedOctRoute)
        {
            var route = CreateRoute(hexRoute);

            var actualOctRoute = route.ToString();

            Assert.AreEqual(expectedOctRoute, actualOctRoute);
        }

        private static OctRoute CreateRoute(string hexRoute)
        {
            var result = OctRoute.Parse(hexRoute);
            return result;
        }
    }
}

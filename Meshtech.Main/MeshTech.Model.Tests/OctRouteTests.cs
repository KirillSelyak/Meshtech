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
        public void ParseTest(string hexRoute, int index, int expectedResult)
        {
            var route = CreateRoute(hexRoute);

            var actualResult = route[index];

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

        [DataTestMethod]
        [DataRow("FFCB18FAC688", "FFCB18FAC688", true)]
        [DataRow("2FFFFFFFFFF8", "2FFFFFFFFFF8", true)]
        [DataRow("FFFFFFFFFFF8", "1FFFFFFFFFF8", false)]
        [DataRow("447FFFFFFFF8", "3FFFFFFFFFF8", false)]
        public void EqualsTest(string firstHexRoute, string secondHexRoute, bool expectedResult)
        {
            var oneRoute = CreateRoute(firstHexRoute);
            var secondRoute = CreateRoute(secondHexRoute);

            var actualResult = oneRoute.Equals(secondRoute);

            Assert.AreEqual(expectedResult, actualResult);
        }

        private static OctRoute CreateRoute(string hexRoute)
        {
            var result = OctRoute.Parse(hexRoute);
            return result;
        }
    }
}

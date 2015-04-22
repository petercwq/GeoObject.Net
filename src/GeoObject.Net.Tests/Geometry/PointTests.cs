using GeoObject.Net.Geometry;
using NUnit.Framework;
using ServiceStack.Text;

namespace GeoObject.Net.Tests.Geometry
{
    [TestFixture]
    public class PointTests : TestBase
    {
        [Test]
        public void Can_Serialize_With_Lat_Lon()
        {
            var point = new GeoPoint(new GeoEntity(90.65464646, 53.2455662));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var actualJson = JsonSerializer.SerializeToString(point);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_With_Lat_Lon_Alt()
        {
            var point = new GeoPoint(new GeoEntity(90.65464646, 53.2455662, 200.4567));

            var expectedJson = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var actualJson = JsonSerializer.SerializeToString(point);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon_Alt()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662,200.4567],\"type\":\"Point\"}";

            var expectedPoint = new GeoPoint(new GeoEntity(90.65464646, 53.2455662, 200.4567));

            var actualPoint = JsonSerializer.DeserializeFromString<GeoPoint>(json);

            Assert.AreEqual(expectedPoint, actualPoint);
        }

        [Test]
        public void Can_Deserialize_With_Lat_Lon()
        {
            var json = "{\"coordinates\":[90.65464646,53.2455662],\"type\":\"Point\"}";

            var expectedPoint = new GeoPoint(new GeoEntity(90.65464646, 53.2455662));

            var actualPoint = JsonSerializer.DeserializeFromString<GeoPoint>(json);

            Assert.AreEqual(expectedPoint, actualPoint);
        }
    }
}
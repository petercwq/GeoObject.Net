using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiPointTests : TestBase
    {
        [Test]
        public void Can_Serialize()
        {
            var points = new List<GeoPoint>
            {
                new GeoPoint() { Entity = new GeoEntity(4.889259338378906, 52.370725881211314)},
                new GeoPoint() { Entity = new GeoEntity(4.895267486572266, 52.3711451105601)},
                new GeoPoint() { Entity = new GeoEntity(4.892091751098633, 52.36931095278263)},
                new GeoPoint() { Entity = new GeoEntity(4.889259338378906, 52.370725881211314)}
            };

            var multiPoint = new GeoMultiPoint(points);

            var actualJson = JsonConvert.SerializeObject(multiPoint);

            JsonAssert.AreEqual(GetExpectedJson(), actualJson);
        }

        [Test]
        public void Can_Deserialize()
        {
            var points = new List<GeoPoint>
            {
                new GeoPoint() { Entity = new GeoEntity(-105.01621, 39.57422)},
                new GeoPoint() { Entity = new GeoEntity(-80.6665134, 35.0539943)}
            };

            var expectedMultiPoint = new GeoMultiPoint(points);

            var json = GetExpectedJson();
            var actualMultiPoint = JsonConvert.DeserializeObject<GeoMultiPoint>(json);

            Assert.AreEqual(expectedMultiPoint, actualMultiPoint);
        }
    }
}
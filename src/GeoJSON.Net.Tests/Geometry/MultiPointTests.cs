using System.Collections.Generic;
using GeoObject.Net.Geometry;
using ServiceStack.Text;
using NUnit.Framework;

namespace GeoObject.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiPointTests : TestBase
    {
        [Test]
        public void Can_Serialize()
        {
            var points = new List<GeoPoint>
            {
                new GeoPoint(new GeoEntity(4.88925933837890, 52.3707258812113)),
                new GeoPoint(new GeoEntity(4.89526748657226, 52.3711451105601)),
                new GeoPoint(new GeoEntity(4.89209175109863, 52.3693109527826)),
                new GeoPoint(new GeoEntity(4.88925933837890, 52.3707258812113))
            };

            var multiPoint = new GeoMultiPoint(points);

            var actualJson = JsonSerializer.SerializeToString(multiPoint);

            JsonAssert.AreEqual(GetExpectedJson(), actualJson);
        }

        [Test]
        public void Can_Deserialize()
        {
            var points = new List<GeoPoint>
            {
                new GeoPoint(new GeoEntity(-105.01621, 39.57422)),
                new GeoPoint(new GeoEntity(-80.6665134, 35.0539943))
            };

            var expectedMultiPoint = new GeoMultiPoint(points);

            var json = GetExpectedJson();
            var actualMultiPoint = JsonSerializer.DeserializeFromString<GeoMultiPoint>(json);

            Assert.AreEqual(expectedMultiPoint, actualMultiPoint);
        }
    }
}
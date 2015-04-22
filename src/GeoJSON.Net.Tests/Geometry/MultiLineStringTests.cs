using System.Collections.Generic;
using GeoObject.Net.Geometry;
using ServiceStack.Text;
using NUnit.Framework;

namespace GeoObject.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiLineStringTests : TestBase
    {
        [Test]
        public void Can_Deserialize()
        {
            var json = GetExpectedJson();

            var expectedMultiLineString = new GeoMultiLineString(new List<GeoLineString>
            {
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(5.3173828125, 52.379790828551016),
                    new GeoEntity(5.456085205078125, 52.36721467920585),
                    new GeoEntity(5.386047363281249, 52.303440474272755, 4.23)
                }),
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(5.3273828125, 52.379790828551016),
                    new GeoEntity(5.486085205078125, 52.36721467920585),
                    new GeoEntity(5.426047363281249, 52.303440474272755, 4.23)
                })
            });

            var multiLineString = JsonSerializer.DeserializeFromString<GeoMultiLineString>(json);

            Assert.IsNotNull(multiLineString);
            Assert.AreEqual(expectedMultiLineString, multiLineString);
        }

        [Test]
        public void Can_Serialize()
        {
            var expectedMultiLineString = new GeoMultiLineString(new List<GeoLineString>
            {
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(5.3173828125, 52.379790828551),
                    new GeoEntity(5.456085205078, 52.367214679205),
                    new GeoEntity(5.386047363281, 52.3034404742727, 4.23)
                }),
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(5.3273828125, 52.379790828551),
                    new GeoEntity(5.486085205078, 52.367214679205),
                    new GeoEntity(5.426047363281, 52.3034404742727, 4.23)
                })
            });

            var expectedJson = GetExpectedJson();
            var actualJson = JsonSerializer.SerializeToString(expectedMultiLineString);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }
    }
}
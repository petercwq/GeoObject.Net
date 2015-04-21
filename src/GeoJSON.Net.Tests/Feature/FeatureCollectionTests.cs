using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using ServiceStack.Text;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Feature
{
    [TestFixture]
    public class FeatureCollectionTests : TestBase
    {
        [Test]
        public void Ctor_Throws_ArgumentNullException_When_Features_Is_Null()
        {
            Assert.Throws<ArgumentNullException>(() => { var collection = new FeatureCollection(null); });
        }

        [Test]
        public void Can_Deserialize()
        {
            string json = GetExpectedJson();

            var featureCollection = JsonSerializer.DeserializeFromString<FeatureCollection>(json);

            Assert.IsNotNull(featureCollection.Features);
            Assert.AreEqual(featureCollection.Features.Count, 3);
            Assert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoObjectType.Point), 1);
            Assert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoObjectType.MultiPolygon), 1);
            Assert.AreEqual(featureCollection.Features.Count(x => x.Geometry.Type == GeoObjectType.Polygon), 1);
        }

        [Test]
        public void FeatureCollectionSerialization()
        {
            var model = new FeatureCollection();
            for (var i = 10; i-- > 0;)
            {
                var geom = new GeoLineString(new[]
                {
                    new GeoEntity(-1.034, 51.010), 
                    new GeoEntity(-0.034, 51.010)
                });

                var props = new Dictionary<string, object>
                {
                    { "test1", "1" }, 
                    { "test2", 2 }
                };

                var feature = new Net.Feature.Feature(geom, props);
                model.Features.Add(feature);
            }

            var actualJson = JsonSerializer.SerializeToString(model);

            Assert.IsNotNull(actualJson);

            Assert.IsFalse(string.IsNullOrEmpty(actualJson));
        }
    }
}
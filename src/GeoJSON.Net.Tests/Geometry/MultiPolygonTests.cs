using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class MultiPolygonTests : TestBase
    {
        [Test]
        public void Can_Deserialize()
        {
            var json = GetExpectedJson();

            var expectMultiPolygon = new GeoMultiPolygon(new List<GeoPolygon>
            {
                new GeoPolygon(new List<GeoLineString>
                {
                    new GeoLineString(new List<IGeoEntity>
                    {
                        new GeoEntity(52.959676831105995, -2.6797102391514338), 
                        new GeoEntity(52.9608756693609, -2.6769029474483279), 
                        new GeoEntity(52.908449372833715, -2.6079763270327119), 
                        new GeoEntity(52.891287242948195, -2.5815104708998668), 
                        new GeoEntity(52.875476700983896, -2.5851645010668989), 
                        new GeoEntity(52.882954723868622, -2.6050779098387191), 
                        new GeoEntity(52.875255907042678, -2.6373482332006359), 
                        new GeoEntity(52.878791122091066, -2.6932445076063951), 
                        new GeoEntity(52.89564268523565, -2.6931334629377890), 
                        new GeoEntity(52.930592009390175, -2.6548779332193022), 
                        new GeoEntity(52.959676831105995, -2.6797102391514338)
                    })
                }), 
                new GeoPolygon(new List<GeoLineString>
                {
                    new GeoLineString(new List<IGeoEntity>
                    {
                        new GeoEntity(52.89610842810761, -2.69628632041613), 
                        new GeoEntity(52.8894641454077, -2.75901233808515), 
                        new GeoEntity(52.89938894657412, -2.7663172788742449), 
                        new GeoEntity(52.90253773227807, -2.804554822840895), 
                        new GeoEntity(52.929801009654575, -2.83848602260174), 
                        new GeoEntity(52.94013913205788, -2.838979264607087), 
                        new GeoEntity(52.937353122653533, -2.7978187468478741), 
                        new GeoEntity(52.920394929466184, -2.772273870352612), 
                        new GeoEntity(52.926572918779222, -2.6996509024137052), 
                        new GeoEntity(52.89610842810761, -2.69628632041613)
                    })
                })
            });

            var actualMultiPolygon = JsonConvert.DeserializeObject<GeoMultiPolygon>(json);

            Assert.AreEqual(expectMultiPolygon, actualMultiPolygon);
        }

        [Test]
        public void Can_Serialize()
        {
            // Arrang
            var polygon1 = new GeoPolygon(new List<GeoLineString>
            {
                new GeoLineString(new List<GeoEntity>
                {
                    new GeoEntity(0, 0), 
                    new GeoEntity(0, 1), 
                    new GeoEntity(1, 1), 
                    new GeoEntity(1, 0), 
                    new GeoEntity(0, 0)
                })
            });

            var polygon2 = new GeoPolygon(new List<GeoLineString>
            {
                new GeoLineString(new List<GeoEntity>
                {
                    new GeoEntity(100, 100), 
                    new GeoEntity(100, 101), 
                    new GeoEntity(101, 101), 
                    new GeoEntity(101, 100), 
                    new GeoEntity(100, 100)
                }), 
                new GeoLineString(new List<GeoEntity>
                {
                    new GeoEntity(200, 200), 
                    new GeoEntity(200, 201), 
                    new GeoEntity(201, 201), 
                    new GeoEntity(201, 200), 
                    new GeoEntity(200, 200)
                })
            });

            var multiPolygon = new GeoMultiPolygon(new List<GeoPolygon> { polygon1, polygon2 });
            var expectedJson = GetExpectedJson();

            // Act
            var actualJson = JsonConvert.SerializeObject(multiPolygon);

            // Assert
            JsonAssert.AreEqual(expectedJson, actualJson);
        }
    }
}
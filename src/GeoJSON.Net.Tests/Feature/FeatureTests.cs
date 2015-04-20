using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Feature
{
    [TestFixture]
    public class FeatureTests : TestBase
    {
        [Test]
        public void Can_Deserialize_Point_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonConvert.DeserializeObject<Net.Feature.Feature>(json);

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Any());

            Assert.IsTrue(feature.Properties.ContainsKey("name"));
            Assert.AreEqual(feature.Properties["name"], "Dinagat Islands");

            Assert.AreEqual(feature.Id, "test-id");

            Assert.AreEqual(feature.Geometry.Type, GeoJSONObjectType.Point);
        }

        [Test]
        public void Can_Serialize_LineString_Feature()
        {
            var coordinates = new[]
            {
                new List<IGeoEntity>
                {
                    new GeoEntity(52.370725881211314, 4.889259338378906),
                    new GeoEntity(52.3711451105601, 4.895267486572266),
                    new GeoEntity(52.36931095278263, 4.892091751098633),
                    new GeoEntity(52.370725881211314, 4.889259338378906)
                },
                new List<IGeoEntity>
                {
                    new GeoEntity(52.370725881211314, 4.989259338378906),
                    new GeoEntity(52.3711451105601, 4.995267486572266),
                    new GeoEntity(52.36931095278263, 4.992091751098633),
                    new GeoEntity(52.370725881211314, 4.989259338378906)
                }
            };

            var geometry = new GeoLineString(coordinates[0]);

            var actualJson = JsonConvert.SerializeObject(new Net.Feature.Feature(geometry));

            Console.WriteLine(actualJson);

            var expectedJson = GetExpectedJson();

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_MultiLineString_Feature()
        {
            var geometry = new GeoMultiLineString(new List<GeoLineString>
            {
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(52.370725881211314, 4.889259338378906),
                    new GeoEntity(52.3711451105601, 4.895267486572266),
                    new GeoEntity(52.36931095278263, 4.892091751098633),
                    new GeoEntity(52.370725881211314, 4.889259338378906)
                }),
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(52.370725881211314, 4.989259338378906),
                    new GeoEntity(52.3711451105601, 4.995267486572266),
                    new GeoEntity(52.36931095278263, 4.992091751098633),
                    new GeoEntity(52.370725881211314, 4.989259338378906)
                })
            });

            var expectedJson = GetExpectedJson();

            var actualJson = JsonConvert.SerializeObject(new Net.Feature.Feature(geometry));
            
            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_Point_Feature()
        {
            var geometry = new GeoPoint(new GeoEntity(1, 2));
            var expectedJson = GetExpectedJson();

            var actualJson = JsonConvert.SerializeObject(new Net.Feature.Feature(geometry));

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_Polygon_Feature()
        {
            var coordinates = new List<GeoEntity>
            {
                new GeoEntity(52.370725881211314, 4.889259338378906),
                new GeoEntity(52.3711451105601, 4.895267486572266),
                new GeoEntity(52.36931095278263, 4.892091751098633),
                new GeoEntity(52.370725881211314, 4.889259338378906)
            };

            var polygon = new GeoPolygon(new List<GeoLineString> { new GeoLineString(coordinates) });
            var properties = new Dictionary<string, object> { { "Name", "Foo" } };
            var feature = new Net.Feature.Feature(polygon, properties);

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(feature);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_MultiPolygon_Feature()
        {
            var multiPolygon = new GeoMultiPolygon(new List<GeoPolygon>
            {
                new GeoPolygon(new List<GeoLineString>
                {
                    new GeoLineString(new List<GeoEntity>
                    {
                        new GeoEntity(0, 0),
                        new GeoEntity(0, 1),
                        new GeoEntity(1, 1),
                        new GeoEntity(1, 0),
                        new GeoEntity(0, 0)
                    })
                }),
                new GeoPolygon(new List<GeoLineString>
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
                })
            });

            var feature = new Net.Feature.Feature(multiPolygon);

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(feature);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Ctor_Can_Add_Properties_Using_Object()
        {
            var properties = new MyTestClass
            {
                BooleanProperty = true,
                DateTimeProperty = DateTime.Now,
                DoubleProperty = 1.2345d,
                EnumProperty = MyTestEnum.Value1,
                IntProperty = -1,
                StringProperty = "Hello, GeoJSON !"
            };

            Net.Feature.Feature feature = new Net.Feature.Feature(new GeoPoint(new GeoEntity(10, 10)), properties);

            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Count > 1);
            Assert.AreEqual(feature.Properties.Count, 6);
        }

        [Test]
        public void Ctor_Creates_Properties_Collection_When_Passed_Null_Proper_Object()
        {
            Net.Feature.Feature feature = new Net.Feature.Feature(new GeoPoint(new GeoEntity(10, 10)), (object)null);

            Assert.IsNotNull(feature.Properties);
            CollectionAssert.IsEmpty(feature.Properties);
        }

        private enum MyTestEnum
        {
            Undefined,
            Value1,
            Value2
        }

        private class MyTestClass
        {
            public bool BooleanProperty { get; set; }

            public DateTime DateTimeProperty { get; set; }

            public double DoubleProperty { get; set; }

            public MyTestEnum EnumProperty { get; set; }

            public int IntProperty { get; set; }

            public string StringProperty { get; set; }
        }
    }
}
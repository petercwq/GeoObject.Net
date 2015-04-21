using System;
using System.Collections.Generic;
using System.Linq;
using GeoJSON.Net.Geometry;
using NUnit.Framework;
using ServiceStack.Text;

namespace GeoJSON.Net.Tests.Feature
{
    [TestFixture]
    public class FeatureTests : TestBase
    {
        public FeatureTests()
        {
            JsConfig<IGeoObject>.RawDeserializeFn = json =>
            {
                var geometry = JsonObject.Parse(json);
                var geometryType = geometry.Get<string>("type");
                switch (geometryType)
                {
                    case null:
                        return null;
                    case "Point":
                        return JsonSerializer.DeserializeFromString<GeoPoint>(json);
                    case "LineString":
                        return JsonSerializer.DeserializeFromString<GeoLineString>(json);
                    case "Polygon":
                        return JsonSerializer.DeserializeFromString<GeoPolygon>(json);
                    case "MultiPoint":
                        return JsonSerializer.DeserializeFromString<GeoMultiPoint>(json);
                    case "MultiLineString":
                        return JsonSerializer.DeserializeFromString<GeoMultiLineString>(json);
                    case "MultiPolygon":
                        return JsonSerializer.DeserializeFromString<GeoMultiPolygon>(json);
                    case "GeometryCollection":
                        return JsonSerializer.DeserializeFromString<GeoCollection>(json);
                    default:
                        throw new InvalidCastException(string.Format("Not a valid GeoSon geometry type: {0}", geometryType));
                }
            };
        }

        [Test]
        public void Can_Deserialize_Point_Feature()
        {
            var json = GetExpectedJson();

            var feature = JsonSerializer.DeserializeFromString<Net.Feature.Feature>(json);

            Assert.IsNotNull(feature);
            Assert.IsNotNull(feature.Properties);
            Assert.IsTrue(feature.Properties.Any());

            Assert.IsTrue(feature.Properties.ContainsKey("name"));
            Assert.AreEqual(feature.Properties["name"], "Dinagat Islands");

            Assert.AreEqual(feature.Id, "test-id");

            Assert.AreEqual(feature.Geometry.Type, GeoObjectType.Point);
        }

        [Test]
        public void Can_Serialize_LineString_Feature()
        {
            var coordinates = new[]
            {
                new List<IGeoEntity>
                {
                    new GeoEntity(4.88925933837, 52.37072588121),
                    new GeoEntity(4.89526748657, 52.371145110),
                    new GeoEntity(4.89209175109, 52.3693109527),
                    new GeoEntity(4.88925933837, 52.37072588121)
                },
                new List<IGeoEntity>
                {
                    new GeoEntity(4.98925933837, 52.37072588121),
                    new GeoEntity(4.99526748657, 52.371145110),
                    new GeoEntity(4.99209175109, 52.3693109527),
                    new GeoEntity(4.98925933837, 52.37072588121)
                }
            };

            var geometry = new GeoLineString(coordinates[0]);

            var actualJson = JsonSerializer.SerializeToString(new Net.Feature.Feature(geometry));

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
                    new GeoEntity(4.88925933837, 52.3707258812),
                    new GeoEntity(4.89526748657, 52.3711451105),
                    new GeoEntity(4.89209175109, 52.3693109527),
                    new GeoEntity(4.88925933837, 52.3707258812)
                }),
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(4.98925933837, 52.3707258812),
                    new GeoEntity(4.99526748657, 52.3711451105),
                    new GeoEntity(4.99209175109, 52.3693109527),
                    new GeoEntity(4.98925933837, 52.3707258812)
                })
            });

            var expectedJson = GetExpectedJson();

            var actualJson = JsonSerializer.SerializeToString(new Net.Feature.Feature(geometry));

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_Point_Feature()
        {
            var geometry = new GeoPoint(new GeoEntity(2, 1));
            var expectedJson = GetExpectedJson();

            var actualJson = JsonSerializer.SerializeToString(new Net.Feature.Feature(geometry));

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Serialize_Polygon_Feature()
        {
            var coordinates = new List<GeoEntity>
            {
                new GeoEntity(4.889259338378, 52.37072588121),
                new GeoEntity(4.895267486572, 52.37114511056),
                new GeoEntity(4.892091751098, 52.36931095278),
                new GeoEntity(4.889259338378, 52.37072588121)
            };

            var polygon = new GeoPolygon(new List<GeoLineString> { new GeoLineString(coordinates) });
            var properties = new Dictionary<string, object> { { "Name", "Foo" } };
            var feature = new Net.Feature.Feature(polygon, properties);

            var expectedJson = GetExpectedJson();
            var actualJson = JsonSerializer.SerializeToString(feature);

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
                        new GeoEntity(1, 0),
                        new GeoEntity(1, 1),
                        new GeoEntity(0, 1),
                        new GeoEntity(0, 0)
                    })
                }),
                new GeoPolygon(new List<GeoLineString>
                {
                    new GeoLineString(new List<GeoEntity>
                    {
                        new GeoEntity(100, 100),
                        new GeoEntity(101, 100),
                        new GeoEntity(101, 101),
                        new GeoEntity(100, 101),
                        new GeoEntity(100, 100)
                    }),
                    new GeoLineString(new List<GeoEntity>
                    {
                        new GeoEntity(200, 200),
                        new GeoEntity(201, 200),
                        new GeoEntity(201, 201),
                        new GeoEntity(200, 201),
                        new GeoEntity(200, 200)
                    })
                })
            });

            var feature = new Net.Feature.Feature(multiPolygon);

            var expectedJson = GetExpectedJson();
            var actualJson = JsonSerializer.SerializeToString(feature);

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
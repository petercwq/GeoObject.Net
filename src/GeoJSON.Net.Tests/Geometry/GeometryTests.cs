using System.Collections.Generic;
using GeoJSON.Net.Converters;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class GeometryTests : TestBase
    {
        public IEnumerable<IGeoObject> Geometries
        {
            get
            {
                var point = new GeoPoint(new GeoEntity(1, 2, 3));

                var multiPoint = new GeoMultiPoint(new List<GeoPoint>
                {
                    new GeoPoint(new GeoEntity(52.379790828551016, 5.3173828125)),
                    new GeoPoint(new GeoEntity(52.36721467920585, 5.456085205078125)),
                    new GeoPoint(new GeoEntity(52.303440474272755, 5.386047363281249, 4.23))
                });

                var lineString = new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(52.379790828551016, 5.3173828125),
                    new GeoEntity(52.36721467920585, 5.456085205078125),
                    new GeoEntity(52.303440474272755, 5.386047363281249, 4.23)
                });

                var multiLineString = new GeoMultiLineString(new List<GeoLineString>
                {
                    new GeoLineString(new List<IGeoEntity>
                    {
                        new GeoEntity(52.379790828551016, 5.3173828125),
                        new GeoEntity(52.36721467920585, 5.456085205078125),
                        new GeoEntity(52.303440474272755, 5.386047363281249, 4.23)
                    }),
                    new GeoLineString(new List<IGeoEntity>
                    {
                        new GeoEntity(52.379790828551016, 5.3273828125),
                        new GeoEntity(52.36721467920585, 5.486085205078125),
                        new GeoEntity(52.303440474272755, 5.426047363281249, 4.23)
                    })
                });

                var polygon = new GeoPolygon(new List<GeoLineString>
                {
                    new GeoLineString(new List<GeoEntity>
                    {
                        new GeoEntity(52.379790828551016, 5.3173828125),
                        new GeoEntity(52.36721467920585, 5.456085205078125),
                        new GeoEntity(52.303440474272755, 5.386047363281249, 4.23),
                        new GeoEntity(52.379790828551016, 5.3173828125)
                    })
                });

                var multiPolygon = new GeoMultiPolygon(new List<GeoPolygon>
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

                yield return point;
                yield return multiPoint;
                yield return lineString;
                yield return multiLineString;
                yield return polygon;
                yield return multiPolygon;
                yield return new GeoCollection(new List<IGeoObject>
                {
                    point,
                    multiPoint,
                    lineString,
                    multiLineString,
                    polygon,
                    multiPolygon
                });
            }
        }

        [Test]
        [TestCaseSource("Geometries")]
        public void Can_Serialize_And_Deserialize_Geometry(IGeoObject geometry)
        {
            var json = JsonConvert.SerializeObject(geometry);

            var deserializedGeometry = JsonConvert.DeserializeObject<IGeoObject>(json, new GeometryConverter());

            Assert.AreEqual(geometry, deserializedGeometry);
        }

        [Test]
        [TestCaseSource("Geometries")]
        public void Serialization_Observes_Indenting_Setting_Of_Serializer(IGeoObject geometry)
        {
            var json = JsonConvert.SerializeObject(geometry, Formatting.Indented);
            Assert.IsTrue(json.Contains("\r\n"));
        }

        [Test]
        [TestCaseSource("Geometries")]
        public void Serialization_Observes_No_Indenting_Setting_Of_Serializer(IGeoObject geometry)
        {
            var json = JsonConvert.SerializeObject(geometry, Formatting.None);
            Assert.IsFalse(json.Contains("\r\n"));
            Assert.IsFalse(json.Contains(" "));
        }

        [Test]
        [TestCaseSource("Geometries")]
        public void Can_Serialize_And_Deserialize_Geometry_As_Object_Property(IGeoObject geometry)
        {
            var classWithGeometry = new ClassWithGeometryProperty(geometry);

            var json = JsonConvert.SerializeObject(classWithGeometry);

            var deserializedClassWithGeometry = JsonConvert.DeserializeObject<ClassWithGeometryProperty>(json);

            Assert.AreEqual(classWithGeometry, deserializedClassWithGeometry);
        }

        private class ClassWithGeometryProperty
        {
            public ClassWithGeometryProperty(IGeoObject geometry)
            {
                Geometry = geometry;
            }

            [JsonConverter(typeof(GeometryConverter))]
            public IGeoObject Geometry { get; private set; }

            /// <summary>
            /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current
            /// <see cref="T:System.Object" />.
            /// </summary>
            /// <returns>
            /// true if the specified object  is equal to the current object; otherwise, false.
            /// </returns>
            /// <param name="obj">The object to compare with the current object. </param>
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }

                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (obj.GetType() != GetType())
                {
                    return false;
                }

                return Equals((ClassWithGeometryProperty)obj);
            }

            /// <summary>
            /// Serves as a hash function for a particular type.
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            public override int GetHashCode()
            {
                return Geometry.GetHashCode();
            }

            public static bool operator ==(ClassWithGeometryProperty left, ClassWithGeometryProperty right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(ClassWithGeometryProperty left, ClassWithGeometryProperty right)
            {
                return !Equals(left, right);
            }

            protected bool Equals(ClassWithGeometryProperty other)
            {
                return Geometry.Equals(other.Geometry);
            }
        }
    }
}
using NUnit.Framework;
using ServiceStack.Text;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class GeometryTests : TestBase
    {
        [Test]
        [TestCaseSource("Geometries")]
        public void Can_Serialize_And_Deserialize_Geometry(IGeoObject geometry)
        {
            var json = JsonSerializer.SerializeToString(geometry);

            var deserializedGeometry = JsonSerializer.DeserializeFromString<IGeoObject>(json
                //, new GeometryConverter()
                );

            Assert.AreEqual(geometry, deserializedGeometry);
        }

        //[Test]
        //[TestCaseSource("Geometries")]
        //public void Serialization_Observes_Indenting_Setting_Of_Serializer(IGeoObject geometry)
        //{
        //    var json = JsonSerializer.SerializeToString(geometry, Formatting.Indented);
        //    Assert.IsTrue(json.Contains("\r\n"));
        //}

        //[Test]
        //[TestCaseSource("Geometries")]
        //public void Serialization_Observes_No_Indenting_Setting_Of_Serializer(IGeoObject geometry)
        //{
        //    var json = JsonSerializer.SerializeToString(geometry, Formatting.None);
        //    Assert.IsFalse(json.Contains("\r\n"));
        //    Assert.IsFalse(json.Contains(" "));
        //}

        [Test]
        [TestCaseSource("Geometries")]
        public void Can_Serialize_And_Deserialize_Geometry_As_Object_Property(IGeoObject geometry)
        {
            var classWithGeometry = new ClassWithGeometryProperty(geometry);

            var json = JsonSerializer.SerializeToString(classWithGeometry);

            var deserializedClassWithGeometry = JsonSerializer.DeserializeFromString<ClassWithGeometryProperty>(json);

            Assert.AreEqual(classWithGeometry, deserializedClassWithGeometry);
        }

        private class ClassWithGeometryProperty
        {
            public ClassWithGeometryProperty(IGeoObject geometry)
            {
                Geometry = geometry;
            }

            // [JsonConverter(typeof(GeometryConverter))]
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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using GeoJSON.Net.Geometry;

namespace GeoJSON.Net.Tests
{
    public abstract class TestBase
    {
        protected IEnumerable<GeoObject> Geometries
        {
            get
            {
                var point = new GeoPoint(new GeoEntity(2, 1, 3));

                var multiPoint = new GeoMultiPoint(new List<GeoPoint>
                {
                    new GeoPoint(new GeoEntity(5.3173828125, 52.37979082855)),
                    new GeoPoint(new GeoEntity(5.45608520507, 52.3672146792)),
                    new GeoPoint(new GeoEntity(5.38604736328, 52.30344047427, 4.23))
                });

                var lineString = new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(5.3173828125, 52.37979082855),
                    new GeoEntity(5.45608520507, 52.3672146792),
                    new GeoEntity(5.38604736328, 52.30344047427, 4.23)
                });

                var multiLineString = new GeoMultiLineString(new List<GeoLineString>
                {
                    new GeoLineString(new List<IGeoEntity>
                    {
                        new GeoEntity(5.3173828125, 52.37979082855),
                        new GeoEntity(5.45608520507, 52.3672146792),
                        new GeoEntity(5.38604736328, 52.30344047427, 4.23)
                    }),
                    new GeoLineString(new List<IGeoEntity>
                    {
                        new GeoEntity(5.3273828125, 52.37979082855),
                        new GeoEntity(5.48608520507, 52.3672146792),
                        new GeoEntity(5.42604736328, 52.30344047427, 4.23)
                    })
                });

                var polygon = new GeoPolygon(new List<GeoLineString>
                {
                    new GeoLineString(new List<GeoEntity>
                    {
                        new GeoEntity(5.3173828125, 52.37979082855),
                        new GeoEntity(5.45608520507, 52.3672146792),
                        new GeoEntity(5.38604736328, 52.30344047427, 4.23),
                        new GeoEntity(5.3173828125, 52.37979082855)
                    })
                });

                var multiPolygon = new GeoMultiPolygon(new List<GeoPolygon>
                {
                    new GeoPolygon(new List<GeoLineString>
                    {
                        new GeoLineString(new List<IGeoEntity>
                        {
                            new GeoEntity(-2.679710239151, 52.95967683110),
                            new GeoEntity(-2.676902947448, 52.960875669),
                            new GeoEntity(-2.607976327032, 52.90844937283),
                            new GeoEntity(-2.581510470899, 52.89128724294),
                            new GeoEntity(-2.585164501066, 52.87547670098),
                            new GeoEntity(-2.605077909838, 52.88295472386),
                            new GeoEntity(-2.637348233200, 52.87525590704),
                            new GeoEntity(-2.693244507606, 52.87879112209),
                            new GeoEntity(-2.693133462937, 52.8956426852),
                            new GeoEntity(-2.654877933219, 52.93059200939),
                            new GeoEntity(-2.679710239151, 52.95967683110)
                        })
                    }),
                    new GeoPolygon(new List<GeoLineString>
                    {
                        new GeoLineString(new List<IGeoEntity>
                        {
                            new GeoEntity(-2.6962863204, 52.8961084281),
                            new GeoEntity(-2.7590123380, 52.889464145),
                            new GeoEntity(-2.766317278874, 52.8993889465),
                            new GeoEntity(-2.80455482284, 52.9025377322),
                            new GeoEntity(-2.8384860226, 52.92980100965),
                            new GeoEntity(-2.83897926460, 52.9401391320),
                            new GeoEntity(-2.797818746847, 52.93735312265),
                            new GeoEntity(-2.77227387035, 52.92039492946),
                            new GeoEntity(-2.699650902413, 52.92657291877),
                            new GeoEntity(-2.6962863204, 52.8961084281)
                        })
                    })
                });

                yield return point;
                yield return multiPoint;
                yield return lineString;
                yield return multiLineString;
                yield return polygon;
                yield return multiPolygon;
                yield return new GeoCollection(new List<GeoObject>
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

        private static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        protected string GetExpectedJson([CallerMemberName] string name = null)
        {
            var type = GetType().Name;
            var projectFolder = GetType().Namespace.Substring(AssemblyName.Length + 1);
            var path = Path.Combine(@".\", projectFolder, type + "_" + name + ".json");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("file not found at " + path);
            }

            return File.ReadAllText(path);
        }
    }
}
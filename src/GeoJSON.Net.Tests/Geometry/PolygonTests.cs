using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using ServiceStack.Text;
using NUnit.Framework;

namespace GeoJSON.Net.Tests.Geometry
{
    [TestFixture]
    public class PolygonTests : TestBase
    {
        [Test]
        public void Can_Serialize()
        {
            var polygon = new GeoPolygon(new List<GeoLineString>
            {
                new GeoLineString(new List<GeoEntity>
                {
                    new GeoEntity(5.3173828125, 52.379790828551),
                    new GeoEntity(5.456085205078, 52.367214679205),
                    new GeoEntity(5.386047363281, 52.3034404742727, 4.23),
                    new GeoEntity(5.3173828125, 52.379790828551),
                })
            });

            var expectedJson = GetExpectedJson();
            var actualJson = JsonSerializer.SerializeToString(polygon);

            JsonAssert.AreEqual(expectedJson, actualJson);
        }

        [Test]
        public void Can_Deserialize_With_Exterior_And_Inner_Rings()
        {
            var json = GetExpectedJson();
            var expectedPolygon = new GeoPolygon(new List<GeoLineString>
            {
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(-84.32281494140, 34.9895035675),
                    new GeoEntity(-84.29122924804, 35.21981940793),
                    new GeoEntity(-84.24041748046, 35.25459097465),
                    new GeoEntity(-84.22531127929, 35.266925688950),
                    new GeoEntity(-84.20745849609, 35.26580442886),
                    new GeoEntity(-84.19921, 35.24674063355),
                    new GeoEntity(-84.16213989257, 35.24113278166),
                    new GeoEntity(-84.12368774414, 35.24898366572),
                    new GeoEntity(-84.09072875976, 35.24898366572),
                    new GeoEntity(-84.08798217773, 35.264683153268),
                    new GeoEntity(-84.04266357421, 35.27701633139),
                    new GeoEntity(-84.03030395507, 35.291589484566),
                    new GeoEntity(-84.0234, 35.306160014550),
                    new GeoEntity(-84.03305053710, 35.32745068492),
                    new GeoEntity(-84.03579711914, 35.34313496028),
                    new GeoEntity(-84.03579711914, 35.348735749472),
                    new GeoEntity(-84.01657104492, 35.35545618392),
                    new GeoEntity(-84.01107788085, 35.37337460834),
                    new GeoEntity(-84.00970458984, 35.39128905521),
                    new GeoEntity(-84.01931762695, 35.41479572901),
                    new GeoEntity(-84.00283813476, 35.429344044107),
                    new GeoEntity(-83.93692016601, 35.47409160773),
                    new GeoEntity(-83.91220092773, 35.47632833265),
                    new GeoEntity(-83.88885498046, 35.504282143299),
                    new GeoEntity(-83.88473510742, 35.516578738902),
                    new GeoEntity(-83.8751220703, 35.52104976129),
                    new GeoEntity(-83.85314941406, 35.52104976129),
                    new GeoEntity(-83.82843017578, 35.52104976129),
                    new GeoEntity(-83.8092041015, 35.53446133418),
                    new GeoEntity(-83.80233764648, 35.54116627999),
                    new GeoEntity(-83.76800537109, 35.56239491058),
                    new GeoEntity(-83.7432861328, 35.56239491058),
                    new GeoEntity(-83.71994018554, 35.56239491058),
                    new GeoEntity(-83.67050170898, 35.569097520776),
                    new GeoEntity(-83.6334228515, 35.570214567965),
                    new GeoEntity(-83.61007690429, 35.576916524038),
                    new GeoEntity(-83.59634399414, 35.574682600980),
                    new GeoEntity(-83.5894775390, 35.55904339525),
                    new GeoEntity(-83.55239868164, 35.56574628576),
                    new GeoEntity(-83.49746704101, 35.563512051219),
                    new GeoEntity(-83.47000122070, 35.586968406786),
                    new GeoEntity(-83.4466552734, 35.60818490437),
                    new GeoEntity(-83.37936401367, 35.63609277863),
                    new GeoEntity(-83.35739135742, 35.65618041632),
                    new GeoEntity(-83.32305908203, 35.66622234103),
                    new GeoEntity(-83.3148193359, 35.65394870599),
                    new GeoEntity(-83.29971313476, 35.660643649881),
                    new GeoEntity(-83.28598022460, 35.67180064238),
                    new GeoEntity(-83.26126098632, 35.6907639509),
                    new GeoEntity(-83.25714111328, 35.69968630125),
                    new GeoEntity(-83.25576782226, 35.715298012125),
                    new GeoEntity(-83.23516845703, 35.72310272092),
                    new GeoEntity(-83.19808959960, 35.72756221127),
                    new GeoEntity(-83.16238403320, 35.753199435570),
                    new GeoEntity(-83.15826416015, 35.76322914549),
                    new GeoEntity(-83.10333251953, 35.76991491635),
                    new GeoEntity(-83.08685302734, 35.7843988251),
                    new GeoEntity(-83.0511474609, 35.787740890986),
                    new GeoEntity(-83.01681518554, 35.78328477203),
                    new GeoEntity(-83.001708984, 35.77882840327),
                    new GeoEntity(-82.96737670898, 35.793310688351),
                    new GeoEntity(-82.94540405273, 35.820040281),
                    new GeoEntity(-82.9193115234, 35.85121343450),
                    new GeoEntity(-82.9083251953, 35.86902116501),
                    new GeoEntity(-82.90557861328, 35.87792352995),
                    new GeoEntity(-82.91244506835, 35.92353244718),
                    new GeoEntity(-82.88360595703, 35.94688293218),
                    new GeoEntity(-82.85614013671, 35.951329861522),
                    new GeoEntity(-82.8424072265, 35.94243575255),
                    new GeoEntity(-82.825927734, 35.92464453144),
                    new GeoEntity(-82.80670166015, 35.927980690382),
                    new GeoEntity(-82.80532836914, 35.94243575255),
                    new GeoEntity(-82.77923583984, 35.97356075349),
                    new GeoEntity(-82.78060913085, 35.99245209055),
                    new GeoEntity(-82.76138305664, 36.00356252895),
                    new GeoEntity(-82.69546508789, 36.04465753921),
                    new GeoEntity(-82.64465332031, 36.060201412392),
                    new GeoEntity(-82.61306762695, 36.060201412392),
                    new GeoEntity(-82.60620117187, 36.033552893400),
                    new GeoEntity(-82.60620117187, 35.991340960635),
                    new GeoEntity(-82.60620117187, 35.97911749857),
                    new GeoEntity(-82.5787353515, 35.96133453736),
                    new GeoEntity(-82.5677490234, 35.951329861522),
                    new GeoEntity(-82.53067016601, 35.97244935753),
                    new GeoEntity(-82.46475219726, 36.006895355244),
                    new GeoEntity(-82.41668701171, 36.070192281208),
                    new GeoEntity(-82.37960815429, 36.10126686921),
                    new GeoEntity(-82.35488891601, 36.117908916563),
                    new GeoEntity(-82.34115600585, 36.113471382052),
                    new GeoEntity(-82.29583740234, 36.13343831245),
                    new GeoEntity(-82.26287841796, 36.13565654678),
                    new GeoEntity(-82.23403930664, 36.13565654678),
                    new GeoEntity(-82.2216796, 36.154509006),
                    new GeoEntity(-82.20382690429, 36.15561783381),
                    new GeoEntity(-82.19009399414, 36.144528857027),
                    new GeoEntity(-82.15438842773, 36.15007354140),
                    new GeoEntity(-82.14065551757, 36.134547437460),
                    new GeoEntity(-82.1337890, 36.116799556445),
                    new GeoEntity(-82.12142944335, 36.10570509327),
                    new GeoEntity(-82.08984, 36.10792411128),
                    new GeoEntity(-82.05276489257, 36.12678323326),
                    new GeoEntity(-82.03628540039, 36.12900165569),
                    new GeoEntity(-81.91268920898, 36.29409768373),
                    new GeoEntity(-81.89071655273, 36.30959215409),
                    new GeoEntity(-81.86325073242, 36.33504067209),
                    new GeoEntity(-81.83029174804, 36.34499652561),
                    new GeoEntity(-81.80145263671, 36.35605709240),
                    new GeoEntity(-81.77947998046, 36.34610265300),
                    new GeoEntity(-81.76162719726, 36.33835943134),
                    new GeoEntity(-81.73690795898, 36.33835943134),
                    new GeoEntity(-81.71905517578, 36.33835943134),
                    new GeoEntity(-81.70669555664, 36.33504067209),
                    new GeoEntity(-81.70669555664, 36.342784223707),
                    new GeoEntity(-81.72317504882, 36.357163062654),
                    new GeoEntity(-81.73278808593, 36.379279167407),
                    new GeoEntity(-81.73690795898, 36.40028364332),
                    new GeoEntity(-81.73690795898, 36.41354670392),
                    new GeoEntity(-81.72454833984, 36.423492513472),
                    new GeoEntity(-81.71768188476, 36.445589751779),
                    new GeoEntity(-81.69845581054, 36.47541104282),
                    new GeoEntity(-81.69845581054, 36.51073994146),
                    new GeoEntity(-81.705322265, 36.53060536411),
                    new GeoEntity(-81.69158935546, 36.55929085774),
                    new GeoEntity(-81.68060302734, 36.56480607840),
                    new GeoEntity(-81.68197631835, 36.58686302344),
                    new GeoEntity(-81.04202270507, 36.56370306576),
                    new GeoEntity(-80.74264526367, 36.561496993252),
                    new GeoEntity(-79.89120483398, 36.54053616262),
                    new GeoEntity(-78.68408203124, 36.53943280355),
                    new GeoEntity(-77.88345336914, 36.54053616262),
                    new GeoEntity(-76.91665649414, 36.54163950596),
                    new GeoEntity(-76.91665649414, 36.55046568575),
                    new GeoEntity(-76.31103515, 36.551568887),
                    new GeoEntity(-75.79605102539, 36.54936246839),
                    new GeoEntity(-75.6298828, 36.07574221562),
                    new GeoEntity(-75.4925537109, 35.82226734114),
                    new GeoEntity(-75.3936767578, 35.639441068973),
                    new GeoEntity(-75.41015624999, 35.43829554739),
                    new GeoEntity(-75.43212890, 35.263561862152),
                    new GeoEntity(-75.487060546, 35.18727767598),
                    new GeoEntity(-75.5914306640, 35.17380831799),
                    new GeoEntity(-75.9210205078, 35.04798673426),
                    new GeoEntity(-76.17919921, 34.867904962568),
                    new GeoEntity(-76.41540527343, 34.62868797377),
                    new GeoEntity(-76.4593505859, 34.57442951865),
                    new GeoEntity(-76.53076171, 34.53371242139),
                    new GeoEntity(-76.5911865234, 34.551811369170),
                    new GeoEntity(-76.651611328, 34.615126683462),
                    new GeoEntity(-76.761474609, 34.63320791137),
                    new GeoEntity(-77.069091796, 34.59704151614),
                    new GeoEntity(-77.376708984, 34.45674800347),
                    new GeoEntity(-77.5909423828, 34.3207552752),
                    new GeoEntity(-77.8326416015, 33.97980872872),
                    new GeoEntity(-77.9150390, 33.80197351806),
                    new GeoEntity(-77.9754638671, 33.73804486328),
                    new GeoEntity(-78.11279296, 33.8521697014),
                    new GeoEntity(-78.2830810546, 33.8521697014),
                    new GeoEntity(-78.4808349609, 33.815666308702),
                    new GeoEntity(-79.6728515, 34.8047829195),
                    new GeoEntity(-80.782470703, 34.836349990763),
                    new GeoEntity(-80.782470703, 34.91746688928),
                    new GeoEntity(-80.9307861328, 35.092945313732),
                    new GeoEntity(-81.0516357421, 35.02999636902),
                    new GeoEntity(-81.0516357421, 35.05248370662),
                    new GeoEntity(-81.0516357421, 35.137879119634),
                    new GeoEntity(-82.3150634765, 35.19625600786),
                    new GeoEntity(-82.3590087890, 35.19625600786),
                    new GeoEntity(-82.40295410156, 35.22318504970),
                    new GeoEntity(-82.4688720703, 35.16931803601),
                    new GeoEntity(-82.6885986328, 35.1154153142),
                    new GeoEntity(-82.781982421, 35.06147690849),
                    new GeoEntity(-83.1060791015, 35.003003395276),
                    new GeoEntity(-83.616943359, 34.99850370014),
                    new GeoEntity(-84.05639648437, 34.985003130171),
                    new GeoEntity(-84.22119140, 34.985003130171),
                    new GeoEntity(-84.32281494140, 34.9895035675),
                }),
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(-75.69030761718, 35.74205383068),
                    new GeoEntity(-75.5914306640, 35.74205383068),
                    new GeoEntity(-75.5419921, 35.585851593232),
                    new GeoEntity(-75.56396484, 35.32633026307),
                    new GeoEntity(-75.69030761718, 35.285984736065),
                    new GeoEntity(-75.970458984, 35.16482750605),
                    new GeoEntity(-76.2066650390, 34.994003757575),
                    new GeoEntity(-76.300048828, 35.02999636902),
                    new GeoEntity(-76.409912109, 35.07946034047),
                    new GeoEntity(-76.5252685546, 35.10642805736),
                    new GeoEntity(-76.4208984, 35.25907654252),
                    new GeoEntity(-76.3385009765, 35.294952147406),
                    new GeoEntity(-76.0858154296, 35.29943548054),
                    new GeoEntity(-75.948486328, 35.44277092585),
                    new GeoEntity(-75.8660888671, 35.53669637839),
                    new GeoEntity(-75.772705078, 35.567980458012),
                    new GeoEntity(-75.706787109, 35.634976650677),
                    new GeoEntity(-75.706787109, 35.74205383068),
                    new GeoEntity(-75.69030761718, 35.74205383068),
                })
            });

            var actualPolygon = JsonSerializer.DeserializeFromString<GeoPolygon>(json);
            Assert.AreEqual(expectedPolygon, actualPolygon);
        }

        [Test]
        public void Can_Deserialize()
        {
            var json = GetExpectedJson();

            var expectedPolygon = new GeoPolygon(new List<GeoLineString>
            {
                new GeoLineString(new List<GeoEntity>
                {
                    new GeoEntity(5.3173828125, 52.379790828551016),
                    new GeoEntity(5.456085205078125, 52.36721467920585),
                    new GeoEntity(5.386047363281249, 52.303440474272755, 4.23),
                    new GeoEntity(5.3173828125, 52.379790828551016),
                })
            });

            var actualPolygon = JsonSerializer.DeserializeFromString<GeoPolygon>(json);

            Assert.AreEqual(expectedPolygon, actualPolygon);
        }
    }
}
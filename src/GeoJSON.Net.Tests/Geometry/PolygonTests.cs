using System.Collections.Generic;
using GeoJSON.Net.Geometry;
using Newtonsoft.Json;
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
                    new GeoEntity(5.3173828125, 52.379790828551016),
                    new GeoEntity(5.456085205078125, 52.36721467920585),
                    new GeoEntity(5.386047363281249, 52.303440474272755, 4.23),
                    new GeoEntity(5.3173828125, 52.379790828551016),
                })
            });

            var expectedJson = GetExpectedJson();
            var actualJson = JsonConvert.SerializeObject(polygon);

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
                    new GeoEntity(-84.3228149414063, 34.9895035675793),
                    new GeoEntity(-84.2912292480469, 35.2198194079344),
                    new GeoEntity(-84.2404174804688, 35.2545909746502),
                    new GeoEntity(-84.2253112792969, 35.2669256889501),
                    new GeoEntity(-84.2074584960938, 35.2658044288675),
                    new GeoEntity(-84.19921875, 35.24674063356),
                    new GeoEntity(-84.1621398925781, 35.2411327816664),
                    new GeoEntity(-84.1236877441406, 35.2489836657264),
                    new GeoEntity(-84.0907287597656, 35.2489836657264),
                    new GeoEntity(-84.0879821777344, 35.2646831532681),
                    new GeoEntity(-84.0426635742188, 35.2770163313988),
                    new GeoEntity(-84.0303039550781, 35.2915894845661),
                    new GeoEntity(-84.0234375, 35.3061600145508),
                    new GeoEntity(-84.0330505371094, 35.3274506849288),
                    new GeoEntity(-84.0357971191406, 35.3431349602819),
                    new GeoEntity(-84.0357971191406, 35.3487357494725),
                    new GeoEntity(-84.0165710449219, 35.3554561839208),
                    new GeoEntity(-84.0110778808594, 35.3733746083496),
                    new GeoEntity(-84.0097045898437, 35.3912890552176),
                    new GeoEntity(-84.0193176269531, 35.4147957290186),
                    new GeoEntity(-84.0028381347656, 35.4293440441072),
                    new GeoEntity(-83.9369201660156, 35.4740916077303),
                    new GeoEntity(-83.9122009277344, 35.4763283326573),
                    new GeoEntity(-83.8888549804688, 35.5042821432997),
                    new GeoEntity(-83.8847351074219, 35.5165787389029),
                    new GeoEntity(-83.8751220703125, 35.5210497612994),
                    new GeoEntity(-83.8531494140625, 35.5210497612994),
                    new GeoEntity(-83.8284301757813, 35.5210497612994),
                    new GeoEntity(-83.8092041015625, 35.5344613341844),
                    new GeoEntity(-83.8023376464844, 35.5411662799981),
                    new GeoEntity(-83.7680053710937, 35.5623949105885),
                    new GeoEntity(-83.7432861328125, 35.5623949105885),
                    new GeoEntity(-83.7199401855469, 35.5623949105885),
                    new GeoEntity(-83.6705017089844, 35.5690975207761),
                    new GeoEntity(-83.6334228515625, 35.570214567966),
                    new GeoEntity(-83.6100769042969, 35.5769165240386),
                    new GeoEntity(-83.5963439941406, 35.5746826009809),
                    new GeoEntity(-83.5894775390625, 35.559043395259),
                    new GeoEntity(-83.5523986816406, 35.5657462857628),
                    new GeoEntity(-83.4974670410156, 35.5635120512197),
                    new GeoEntity(-83.4700012207031, 35.5869684067865),
                    new GeoEntity(-83.4466552734375, 35.6081849043775),
                    new GeoEntity(-83.3793640136719, 35.6360927786314),
                    new GeoEntity(-83.3573913574219, 35.6561804163202),
                    new GeoEntity(-83.3230590820312, 35.6662223410348),
                    new GeoEntity(-83.3148193359375, 35.6539487059976),
                    new GeoEntity(-83.2997131347656, 35.6606436498816),
                    new GeoEntity(-83.2859802246094, 35.6718006423877),
                    new GeoEntity(-83.2612609863281, 35.6907639509368),
                    new GeoEntity(-83.2571411132813, 35.699686301252),
                    new GeoEntity(-83.2557678222656, 35.7152980121253),
                    new GeoEntity(-83.2351684570313, 35.7231027209226),
                    new GeoEntity(-83.1980895996094, 35.727562211272),
                    new GeoEntity(-83.1623840332031, 35.7531994355703),
                    new GeoEntity(-83.1582641601563, 35.763229145499),
                    new GeoEntity(-83.1033325195313, 35.7699149163548),
                    new GeoEntity(-83.0868530273438, 35.7843988251953),
                    new GeoEntity(-83.0511474609375, 35.7877408909866),
                    new GeoEntity(-83.0168151855469, 35.7832847720374),
                    new GeoEntity(-83.001708984375, 35.7788284032737),
                    new GeoEntity(-82.9673767089844, 35.7933106883517),
                    new GeoEntity(-82.9454040527344, 35.820040281161),
                    new GeoEntity(-82.9193115234375, 35.8512134345006),
                    new GeoEntity(-82.9083251953125, 35.869021165017),
                    new GeoEntity(-82.9055786132813, 35.8779235299512),
                    new GeoEntity(-82.9124450683594, 35.9235324471824),
                    new GeoEntity(-82.8836059570313, 35.9468829321814),
                    new GeoEntity(-82.8561401367188, 35.9513298615227),
                    new GeoEntity(-82.8424072265625, 35.9424357525543),
                    new GeoEntity(-82.825927734375, 35.924644531441),
                    new GeoEntity(-82.8067016601563, 35.9279806903827),
                    new GeoEntity(-82.8053283691406, 35.9424357525543),
                    new GeoEntity(-82.7792358398438, 35.9735607534962),
                    new GeoEntity(-82.7806091308594, 35.9924520905583),
                    new GeoEntity(-82.7613830566406, 36.0035625289507),
                    new GeoEntity(-82.6954650878906, 36.0446575392153),
                    new GeoEntity(-82.6446533203125, 36.0602014123929),
                    new GeoEntity(-82.6130676269531, 36.0602014123929),
                    new GeoEntity(-82.606201171875, 36.0335528934004),
                    new GeoEntity(-82.606201171875, 35.9913409606354),
                    new GeoEntity(-82.606201171875, 35.979117498575),
                    new GeoEntity(-82.5787353515625, 35.9613345373669),
                    new GeoEntity(-82.5677490234375, 35.9513298615227),
                    new GeoEntity(-82.5306701660156, 35.9724493575368),
                    new GeoEntity(-82.4647521972656, 36.0068953552447),
                    new GeoEntity(-82.4166870117188, 36.0701922812085),
                    new GeoEntity(-82.3796081542969, 36.1012668692145),
                    new GeoEntity(-82.3548889160156, 36.1179089165637),
                    new GeoEntity(-82.3411560058594, 36.1134713820522),
                    new GeoEntity(-82.2958374023438, 36.1334383124587),
                    new GeoEntity(-82.2628784179687, 36.1356565467854),
                    new GeoEntity(-82.2340393066406, 36.1356565467854),
                    new GeoEntity(-82.2216796875, 36.154509006695),
                    new GeoEntity(-82.2038269042969, 36.1556178338186),
                    new GeoEntity(-82.1900939941406, 36.1445288570277),
                    new GeoEntity(-82.1543884277344, 36.1500735414076),
                    new GeoEntity(-82.1406555175781, 36.1345474374601),
                    new GeoEntity(-82.1337890625, 36.116799556445),
                    new GeoEntity(-82.1214294433594, 36.1057050932792),
                    new GeoEntity(-82.08984375, 36.1079241112865),
                    new GeoEntity(-82.0527648925781, 36.1267832332643),
                    new GeoEntity(-82.0362854003906, 36.1290016556965),
                    new GeoEntity(-81.9126892089844, 36.2940976837303),
                    new GeoEntity(-81.8907165527344, 36.3095921540914),
                    new GeoEntity(-81.8632507324219, 36.3350406720961),
                    new GeoEntity(-81.8302917480469, 36.344996525619),
                    new GeoEntity(-81.8014526367188, 36.3560570924018),
                    new GeoEntity(-81.7794799804687, 36.3461026530064),
                    new GeoEntity(-81.7616271972656, 36.3383594313405),
                    new GeoEntity(-81.7369079589844, 36.3383594313405),
                    new GeoEntity(-81.7190551757813, 36.3383594313405),
                    new GeoEntity(-81.7066955566406, 36.3350406720961),
                    new GeoEntity(-81.7066955566406, 36.3427842237072),
                    new GeoEntity(-81.7231750488281, 36.3571630626544),
                    new GeoEntity(-81.7327880859375, 36.379279167408),
                    new GeoEntity(-81.7369079589844, 36.4002836433235),
                    new GeoEntity(-81.7369079589844, 36.4135467039288),
                    new GeoEntity(-81.7245483398437, 36.4234925134723),
                    new GeoEntity(-81.7176818847656, 36.4455897517792),
                    new GeoEntity(-81.6984558105469, 36.4754110428296),
                    new GeoEntity(-81.6984558105469, 36.5107399414667),
                    new GeoEntity(-81.705322265625, 36.5306053641136),
                    new GeoEntity(-81.6915893554688, 36.55929085774),
                    new GeoEntity(-81.6806030273438, 36.5648060784035),
                    new GeoEntity(-81.6819763183594, 36.5868630234418),
                    new GeoEntity(-81.0420227050781, 36.5637030657692),
                    new GeoEntity(-80.7426452636719, 36.5614969932526),
                    new GeoEntity(-79.8912048339844, 36.540536162629),
                    new GeoEntity(-78.68408203125, 36.5394328035512),
                    new GeoEntity(-77.8834533691406, 36.540536162629),
                    new GeoEntity(-76.9166564941406, 36.5416395059613),
                    new GeoEntity(-76.9166564941406, 36.5504656857595),
                    new GeoEntity(-76.31103515625, 36.551568887374),
                    new GeoEntity(-75.7960510253906, 36.5493624683978),
                    new GeoEntity(-75.6298828125, 36.075742215627),
                    new GeoEntity(-75.4925537109375, 35.8222673411451),
                    new GeoEntity(-75.3936767578125, 35.6394410689739),
                    new GeoEntity(-75.41015625, 35.4382955473967),
                    new GeoEntity(-75.43212890625, 35.2635618621521),
                    new GeoEntity(-75.487060546875, 35.187277675989),
                    new GeoEntity(-75.5914306640625, 35.1738083179996),
                    new GeoEntity(-75.9210205078125, 35.0479867342673),
                    new GeoEntity(-76.17919921875, 34.8679049625687),
                    new GeoEntity(-76.4154052734375, 34.6286879737706),
                    new GeoEntity(-76.4593505859375, 34.5744295186527),
                    new GeoEntity(-76.53076171875, 34.5337124213957),
                    new GeoEntity(-76.5911865234375, 34.5518113691705),
                    new GeoEntity(-76.651611328125, 34.6151266834622),
                    new GeoEntity(-76.761474609375, 34.6332079113796),
                    new GeoEntity(-77.069091796875, 34.5970415161442),
                    new GeoEntity(-77.376708984375, 34.4567480034781),
                    new GeoEntity(-77.5909423828125, 34.3207552752374),
                    new GeoEntity(-77.8326416015625, 33.9798087287246),
                    new GeoEntity(-77.9150390625, 33.8019735180659),
                    new GeoEntity(-77.9754638671875, 33.7380448632891),
                    new GeoEntity(-78.11279296875, 33.8521697014074),
                    new GeoEntity(-78.2830810546875, 33.8521697014074),
                    new GeoEntity(-78.4808349609375, 33.8156663087028),
                    new GeoEntity(-79.6728515625, 34.8047829195724),
                    new GeoEntity(-80.782470703125, 34.8363499907639),
                    new GeoEntity(-80.782470703125, 34.9174668892825),
                    new GeoEntity(-80.9307861328125, 35.0929453137326),
                    new GeoEntity(-81.0516357421875, 35.0299963690257),
                    new GeoEntity(-81.0516357421875, 35.0524837066247),
                    new GeoEntity(-81.0516357421875, 35.1378791196342),
                    new GeoEntity(-82.3150634765625, 35.1962560078637),
                    new GeoEntity(-82.3590087890625, 35.1962560078637),
                    new GeoEntity(-82.4029541015625, 35.2231850497018),
                    new GeoEntity(-82.4688720703125, 35.1693180360113),
                    new GeoEntity(-82.6885986328125, 35.1154153142536),
                    new GeoEntity(-82.781982421875, 35.0614769084972),
                    new GeoEntity(-83.1060791015625, 35.0030033952767),
                    new GeoEntity(-83.616943359375, 34.9985037001463),
                    new GeoEntity(-84.056396484375, 34.9850031301711),
                    new GeoEntity(-84.22119140625, 34.9850031301711),
                    new GeoEntity(-84.3228149414063, 34.9895035675793),
                }),
                new GeoLineString(new List<IGeoEntity>
                {
                    new GeoEntity(-75.6903076171875, 35.7420538306804),
                    new GeoEntity(-75.5914306640625, 35.7420538306804),
                    new GeoEntity(-75.5419921875, 35.5858515932324),
                    new GeoEntity(-75.56396484375, 35.3263302630748),
                    new GeoEntity(-75.6903076171875, 35.2859847360657),
                    new GeoEntity(-75.970458984375, 35.1648275060503),
                    new GeoEntity(-76.2066650390625, 34.9940037575758),
                    new GeoEntity(-76.300048828125, 35.0299963690257),
                    new GeoEntity(-76.409912109375, 35.0794603404798),
                    new GeoEntity(-76.5252685546875, 35.1064280573642),
                    new GeoEntity(-76.4208984375, 35.2590765425257),
                    new GeoEntity(-76.3385009765625, 35.2949521474066),
                    new GeoEntity(-76.0858154296875, 35.2994354805454),
                    new GeoEntity(-75.948486328125, 35.4427709258577),
                    new GeoEntity(-75.8660888671875, 35.536696378395),
                    new GeoEntity(-75.772705078125, 35.5679804580121),
                    new GeoEntity(-75.706787109375, 35.6349766506773),
                    new GeoEntity(-75.706787109375, 35.7420538306804),
                    new GeoEntity(-75.6903076171875, 35.7420538306804),
                })
            });

            var actualPolygon = JsonConvert.DeserializeObject<GeoPolygon>(json);
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

            var actualPolygon = JsonConvert.DeserializeObject<GeoPolygon>(json);

            Assert.AreEqual(expectedPolygon, actualPolygon);
        }
    }
}
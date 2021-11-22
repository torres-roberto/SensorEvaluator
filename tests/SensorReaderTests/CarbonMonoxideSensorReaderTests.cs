using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SensorEvaluator;

namespace SensorEvaluatorTests.SensorReaderTests
{
    [TestClass]
    public class CarbonMonoxideSensorReaderTests
    {
        [TestMethod]
        public void ShouldBeKept_WhenAllReadingsWithin1ppmOfReferenceReading()
        {
            var log = @"
                reference 70.0 45.0 6
                monoxide mon-1
                2007-04-05T22:04 5
                2007-04-05T22:05 7
                2007-04-05T22:06 9
            ";

            var result = LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual("keep", json["mon-1"]);
        }

        [TestMethod]
        public void ShouldBeDiscarded_WhenOneReadingIsOutsideOf1ppm()
        {
            var log = @"
                reference 70.0 45.0 6
                monoxide mon-1
                2007-04-05T22:04 2
                2007-04-05T22:05 4
                2007-04-05T22:06 10
                2007-04-05T22:07 8
                2007-04-05T22:08 6
            ";

            var result = LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual("discard", json["mon-1"]);
        }
    }
}

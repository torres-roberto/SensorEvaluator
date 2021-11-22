using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace SensorEvaluatorTests.SensorReaderTests
{
    /// <summary>
    // For a humidity sensor, it must be discarded unless it is within 1 humidity percent
    // of the reference value for all readings. (All humidity sensor
    // readings are a decimal value representing percent moisture saturation.)
    /// </summary>
    [TestClass]
    public class HumiditySensorReaderTests
    {
        [TestMethod]
        public void ShouldBeKept_WhenWithin1PercentOfReferenceReading()
        {
            var log = @"
                reference 70.0 45.0 6
                humidity hum-1
                2007-04-05T22:04 45.2
                2007-04-05T22:05 45.3
                2007-04-05T22:06 45.1
            ";

            var result = SensorEvaluator.LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual("keep", json["hum-1"]);
        }

        [TestMethod]
        public void ShouldBeDiscarded_WhenOneReadingIsOutsideOf1Percent()
        {
            var log = @"
                reference 70.0 45.0 6
                humidity hum-1
                2007-04-05T22:04 45.2
                2007-04-05T22:05 45.3
                2007-04-05T22:06 46.1
            ";

            var result = SensorEvaluator.LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual("discard", json["hum-1"]);
        }
    }
}

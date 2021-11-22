using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace SensorEvaluatorTests
{
    /// <summary>
    /// For a thermometer, it is branded “ultra precise” if the mean of the readings is within 0.5 degrees
    /// of the known temperature, and the standard deviation is less than 3.
    /// It is branded “very precise” if the mean is within 0.5 degrees of the room,
    /// and the standard deviation is under 5. Otherwise, it’s sold as “precise”.
    /// </summary>
    [TestClass]
    public class ThermometerReaderTests
    {
        [TestMethod]
        public void ShouldBeUltraPrecise()
        {
            var log = @"
                reference 70.0 45.0 6
                thermometer temp-1
                2007-04-05T22:00 72.4
                2007-04-05T22:04 71.2
                2007-04-05T22:05 71.4
                2007-04-05T22:06 69.2
                2007-04-05T22:11 67.5
                2007-04-05T22:12 69.4
            ";

            var result = SensorEvaluator.LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual("ultra precise", json["temp-1"]);
        }

        [TestMethod]
        public void ShouldBeVeryPrecise()
        {
            var log = @"
                reference 70.0 45.0 6
                thermometer temp-1
                2007-04-05T22:00 72.4
                2007-04-05T22:01 76.0
                2007-04-05T22:03 75.6
                2007-04-05T22:04 71.2
                2007-04-05T22:05 71.4
                2007-04-05T22:06 69.2
                2007-04-05T22:07 65.2
                2007-04-05T22:10 64.0
                2007-04-05T22:11 67.5
                2007-04-05T22:12 69.4
            ";

            var result = SensorEvaluator.LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual("very precise", json["temp-1"]);
        }

        [TestMethod]
        public void ShouldBePrecise()
        {
            var log = @"
                reference 70.0 45.0 6
                thermometer temp-1
                2007-04-05T22:00 72.4
                2007-04-05T22:01 76.0
                2007-04-05T22:02 79.1
                2007-04-05T22:03 75.6
                2007-04-05T22:04 71.2
                2007-04-05T22:05 71.4
                2007-04-05T22:06 69.2
                2007-04-05T22:07 65.2
                2007-04-05T22:08 62.8
                2007-04-05T22:09 61.4
                2007-04-05T22:10 64.0
                2007-04-05T22:11 67.5
                2007-04-05T22:12 69.4
            ";

            var result = SensorEvaluator.LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string,string>>(result);
            
            Assert.AreEqual("precise", json["temp-1"]);
        }
    }
}

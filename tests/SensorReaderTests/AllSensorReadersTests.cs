using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SensorEvaluator;
using SensorEvaluatorTests.resources;

namespace SensorEvaluatorTests.SensorReaderTests
{
    [TestClass]
    public class AllSensorReaderTests
    {
        [TestMethod]
        public void ShouldBeValidClassifications()
        {
            var log = SampleLogs.Value;

            var result = LogEvaluator.EvaluateLogFile(log);

            var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(result);

            Assert.AreEqual("precise", json["temp-1"]);
            Assert.AreEqual("ultra precise", json["temp-2"]);
            Assert.AreEqual("keep", json["hum-1"]);
            Assert.AreEqual("discard", json["hum-2"]);
            Assert.AreEqual("keep", json["mon-1"]);
            Assert.AreEqual("discard", json["mon-2"]);
        }

    }
}

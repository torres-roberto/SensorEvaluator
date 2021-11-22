using System;
using System.Collections.Generic;

namespace SensorEvaluator.SensorReaders
{
    public class HumiditySensorReader : SensorReader, ISensorReader
    {
        public HumiditySensorReader(string type, string name) : base(type, name) { }

        public string Evaluate(List<double> readings, RoomReference reference)
        {
            var result = 
                readings.TrueForAll(reading => Math.Abs(reference.Humidity - reading) <= 1.0)
                ? "keep"
                : "discard";

            return $"\"{Name}\": \"{result}\"";
        }
    }
}

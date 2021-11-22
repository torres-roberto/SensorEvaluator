using System;
using System.Collections.Generic;

namespace SensorEvaluator.SensorReaders
{
    public class CarbonMonoxideSensorReader : SensorReader, ISensorReader
    {
        public CarbonMonoxideSensorReader(string type, string name) : base(type, name) { }

        public string Evaluate(List<double> readings, RoomReference reference)
        {
            var result =
                readings.TrueForAll(reading => Math.Abs(reference.COConcentration - reading) <= 3)
                ? "keep"
                : "discard";

            return $"\"{Name}\": \"{result}\"";
        }
    }
}

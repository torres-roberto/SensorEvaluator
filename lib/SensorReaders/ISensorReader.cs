using System.Collections.Generic;

namespace SensorEvaluator.SensorReaders
{
    public interface ISensorReader
    {
        string Evaluate(List<double> readings, RoomReference reference);
    }
}

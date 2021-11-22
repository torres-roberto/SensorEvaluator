namespace SensorEvaluator.SensorReaders
{
    public class SensorReaderBuilder
    {
        public static ISensorReader For(string sensor)
        {
            var sensorType = sensor.Split(" ")[0];
            var sensorName = sensor.Split(" ")[1];

            switch (sensorType)
            {
                case "thermometer":
                    return new ThermometerReader(sensorType, sensorName);
                case "humidity":
                    return new HumiditySensorReader(sensorType, sensorName);
                case "monoxide":
                    return new CarbonMonoxideSensorReader(sensorType, sensorName);
                default:
                    return null;
            }
        }
    }
}

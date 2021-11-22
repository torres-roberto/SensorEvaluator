using System;
using System.Collections.Generic;
using System.Text;
using SensorEvaluator.SensorReaders;

namespace SensorEvaluator
{
    public class LogEvaluator
    {
        private const int BEGINNING_OF_LOGS = 1;

        public static string EvaluateLogFile(string logContentStr)
        {
            var logs = logContentStr?.Trim()?.Split(Environment.NewLine);

            var roomReference = RoomReference.GetRoomReference(logs?[RoomReference.ROOM_REFERENCE_INDEX]);
            var sensorReadings = GetSensorReadings(logs);

            return BuildOutput(roomReference, sensorReadings);
        }

        private static Dictionary<string, List<double>> GetSensorReadings(string[] logs)
        {
            try
            {
                var sensorReadings = new Dictionary<string, List<double>>();
                var currentSensor = string.Empty;

                for (int i = BEGINNING_OF_LOGS; i < logs.Length; i++)
                {
                    var log = logs?[i]?.Trim();

                    var (newSensor, reading) = GetNewSensorOrReading(log);

                    if (newSensor != null)
                    {
                        // Add new sensor
                        sensorReadings.Add(newSensor, new List<double>());
                        currentSensor = newSensor;
                    }
                    else
                    {
                        // Add reading to current sensor
                        sensorReadings[currentSensor].Add(Convert.ToDouble(reading));
                    }
                }

                return sensorReadings;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Provided log could not be parsed successfully. Found [{logs?.Length}] lines in logs.");
            }
        }

        private static (string, string) GetNewSensorOrReading(string log)
        {
            var sensor = log?.Split(" ")?[0];
            var reading = log?.Split(" ")?[1];

            if (DateTime.TryParse(sensor, out _))
            {
                return (null, reading);
            }

            return ($"{sensor} {reading}", null);
        }

        private static string BuildOutput(RoomReference reference, Dictionary<string, List<double>> sensorReadings)
        {
            var result = new StringBuilder();

            foreach (var (sensor, readings) in sensorReadings)
            {
                result.AppendLine(SensorReaderBuilder.For(sensor).Evaluate(readings, reference));
            }

            return $"{{ {result} }}";
        }
    }
}

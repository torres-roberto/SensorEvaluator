using System;

namespace SensorEvaluator
{
    public class RoomReference
    {
        public const int ROOM_REFERENCE_INDEX = 0;

        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public int COConcentration { get; set; }

        public static RoomReference GetRoomReference(string log)
        {
            try
            {
                var parsedLog = log?.Split(" ");

                return new RoomReference
                {
                    Temperature = Convert.ToDouble(parsedLog[1]),
                    Humidity = Convert.ToDouble(parsedLog[2]),
                    COConcentration = Convert.ToInt32(parsedLog[3]),
                };
            }
            catch (Exception)
            {
                throw new ArgumentException($"First line must be valid room reference e.g \"reference 70.0 45.0 6\". Received invalid reference [{log}] instead.");
            }
        }
    }
}

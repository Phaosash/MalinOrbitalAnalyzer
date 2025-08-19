namespace MalinOrbitalAnalyzer.DataModels;

//  This struct defines two nullable properties, SensorA and SensorB,
//  both of type double?. This means that each property can either
//  hold a double value or be`null. The struct is used to represent
//  data from two sensors, where values can be optionally assigned or
//  left undefined (null).
internal struct SensorData {
    public double? SensorA { get; set; }
    public double? SensorB { get; set; }
}
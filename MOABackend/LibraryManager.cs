using Galileo6;

namespace MOABackend;

public class LibraryManager {
    private readonly LinkedList<double> _sensorA = new();
    private readonly LinkedList<double> _sensorB = new();

    public void LoadData (double average, double deviation){
        const int listSize = 400;
        ReadData readData = new();

        for (int i = 0; i < listSize; i++){
            _sensorA.AddLast(readData.SensorA(average, deviation));
            _sensorB.AddLast(readData.SensorB(average, deviation));
        }
    }

    public LinkedList<double> ReturnSensorA (){
        return _sensorA;
    }

    public LinkedList<double> ReturnSensorB (){
        return _sensorB;
    }

    private int NumberOfNodes (LinkedList<double> nodeList){
        return nodeList.Count;
    }
}
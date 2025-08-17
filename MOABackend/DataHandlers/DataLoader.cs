using Galileo6;

namespace MOABackend.DataHandlers;

internal class DataLoader {
    //  Programming requirements 4.2
    public static LinkedList<double> LoadData (LinkedList<double> theList, double average, double deviation, bool firstSensor){
        const int listSize = 400;
        ReadData readData = new();

        for (int i = 0; i < listSize; i++){
            if (firstSensor){
                theList.AddLast(readData.SensorA(average, deviation));
            } else {
                theList.AddLast(readData.SensorB(average, deviation));
            }
        }

        return theList;
    }
}
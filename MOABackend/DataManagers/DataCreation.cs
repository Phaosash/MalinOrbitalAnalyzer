using Galileo6;

namespace MOABackend.DataManagers;

internal class DataCreation {
    //  Programming requirements 4.2
    public static LinkedList<double> LoadData (double average, double deviation, bool setA){
        const int listSize = 400;
        ReadData readData = new();
        LinkedList<double> data = new();

        for (int i = 0; i < listSize; i++){
            if (setA){            
                data.AddLast(readData.SensorA(average, deviation));
            } else {
                data.AddLast(readData.SensorB(average, deviation));
            }
        }

        return data;
    }
}
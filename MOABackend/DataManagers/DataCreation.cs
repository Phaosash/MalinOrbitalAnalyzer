using ErrorLoggingLibrary;
using Galileo6;

namespace MOABackend.DataManagers;

internal class DataCreation {
    //  Programming requirements 4.2
    //  This method generates a LinkedList<double> of size 400, filled with values based on a normal distribution
    //  defined by the average and deviation parameters. It uses a ReadData object to populate the list, calling
    //  either SensorA or SensorB methods based on the setA parameter. If an exception occurs during the data
    //  generation process, it logs the error and returns an empty LinkedList.
    public static LinkedList<double> LoadData (double average, double deviation, bool setA){
        try {
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
        } catch (Exception ex){
            LoggingHandler.Instance.LogError("Encountered unexpected problem with the LoadData method in the data creation class", ex);
            return new();
        }
    }
}
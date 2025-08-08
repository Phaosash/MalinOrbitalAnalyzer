using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOABackend;

internal class BinarySearches {
    public static int BinarySearchIterative(LinkedList<double> list, double searchValue, int minimum, int maximum) {
        List<double> sortedList = list.ToList();

        while (minimum <= maximum - 1){
            int middle = (minimum + maximum) / 2;
            double middleValue = sortedList[middle];
            double epsilon = 0.00001;

            if (Math.Abs(searchValue - middleValue) < epsilon){
                return middle + 1;
            }
            else if (searchValue < middleValue){
                maximum = middle - 1;
            } else 
            {
                minimum = middle + 1;
            }
        }

        return minimum;
    }
}
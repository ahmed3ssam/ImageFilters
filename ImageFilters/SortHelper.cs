using System;
using System.Collections.Generic;
using System.Text;

namespace ImageFilters
{
    class SortHelper
    {
        private static Random random = new Random();
        private static byte[] tempArray = new byte[byte.MaxValue + 1];
        public static byte Kth_element(Byte[] Array, int T)
        {
            int min = Byte.MaxValue;
            int max = Byte.MinValue;

            foreach (var b in Array)
            {
                if (b < min)
                    min = b;
                if (b > max)
                    max = b;
            }

            int maxT = T;
            int minT = T;
            int sum = 0;
            int count = 0;

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                if (Array[i] == max && maxT > 0)
                {
                    maxT--;
                    continue;
                }

                if (Array[i] == min && minT > 0)
                {
                    minT--;
                    continue;
                }

                count++;
                sum += Array[i];

            }
            if (count == 0)
            {
                return 0;
            }
            return (byte)(sum / count);
        }

        public static Byte[] CountingSort(Byte[] Array)
        {

            System.Array.Clear(tempArray, 0, tempArray.GetLength(0));

            foreach (var b in Array)
                tempArray[b]++;

            int index = 0;
            for (int i = 0; i < tempArray.GetLength(0); i++)
                for (int j = 0; j < tempArray[i]; j++)
                    Array[index++] = (byte)i;
            return Array.Clone() as byte[];
        }

        public static byte[] QuickSort(Byte[] Array)
        {

            QuickSort(Array, 0, Array.Length - 1);
            return Array.Clone() as byte[];
        }
        private static void QuickSort(Byte[] Array, int low, int high)
        {

            if (low < high)
            {

                int pivot = partition(Array, low, high);

                QuickSort(Array, low, pivot - 1);
                QuickSort(Array, pivot + 1, high);
            }

        }



        public static byte[] GetWindow(byte[,] Image, int WindowSize, int x, int y)
        {

            int extra = WindowSize / 2;
            int index = 0;

            byte[] Window = new byte[WindowSize * WindowSize];

            for (int i = y - extra; i <= y + extra; i++)
                for (int j = x - extra; j <= x + extra; j++)
                    Window[index++] = Image[i, j];

            return Window;
        }

        private static void swap(byte[] Window, int i, int j)
        {
            var temp = Window[j];
            Window[j] = Window[i];
            Window[i] = temp;
        }

        private static int partition(byte[] Window, int low, int high)
        {
            int i = low;
            int j = high;
            byte pivot = Window[low];

            while (true)
            {
                while (Window[i] <= pivot && i < high)
                    i++;

                while (Window[j] > pivot && j >= low)
                    j--;

                if (i >= j)
                {
                    swap(Window, j, low);
                    return j;
                }

                swap(Window, i, j);
            }
        }

    }


}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LedProject1._0
{
    public class Frame
    {
        private int[,] colorArray; 
        public Frame(int numPixels)
        {
            colorArray = new int[numPixels,3];
        }

        public int[] getPixel(int position)
        {
            int[] output = new int[3];
            for (int i = 0; i < 3; i++)
                output[i] = colorArray[position,i];
            return output;
        }
        public void setPixel(int position, int[] color)
        {
            for(int i = 0; i < 3; i++)
                colorArray[position, i] = color[i];
        }

        public void setPixel(int[,] colorArray)
        {
            this.colorArray = colorArray;
        }

    }
}

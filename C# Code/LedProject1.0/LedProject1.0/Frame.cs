using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LedProject1._0
{
    public class Frame
    {
        private Color[] colorArray; 
        public Frame(int numPixels)
        {
            colorArray = new Color[numPixels];
        }

        public Color getPixel(int position)
        {
            return colorArray[position];
        }
        public void setPixel(int R, int G, int B, int position)
        {
            Color pixel = Color.FromArgb(R, G, B);
            colorArray[position] = pixel;
        }
        public void setPixel(Color pixel, int position)
        {
            colorArray[position] = pixel;
        }

        public void setPixel(Color[] colorArray)
        {
            this.colorArray = colorArray;
        }

    }
    public class Pixel
    {
        public int X, Y;
        public Pixel(int X, int Y)
        {
            this.X = X;
            this.Y = Y;

        }
    }
}

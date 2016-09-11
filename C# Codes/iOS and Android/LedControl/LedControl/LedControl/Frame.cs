using System;
namespace LedControl
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
}

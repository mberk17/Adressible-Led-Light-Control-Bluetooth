using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;



namespace LedProject1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public SerialPort port1;
        public Graphics g;
        public Bitmap b;
        public Frame frame;
        public int currentPixel = 0;
        public const int leftPixels = 7, rightPixels = 7, topPixels = 12;
        public const int numberOfPixels = leftPixels + rightPixels + topPixels;


        
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(string s in SerialPort.GetPortNames())
                comboBox1.Items.Add(s);
            frame = new Frame(numberOfPixels);
            pictureBox.BackColor = Color.FromArgb(255, 255, 255);
            b = new Bitmap(pictureBox.Width, pictureBox.Height);
            g = Graphics.FromImage(b);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            drawScreen();
        }
        void refr()
        {
            pictureBox.Image = b;
        }

        private void clearScreen()
        {
            g.Clear(Color.FromArgb(255, 255, 255));
        }
        private void drawReferanceFrame()
        {
            int scrWidth = pictureBox.Width, scrHeight = pictureBox.Height;
            Pen linePen = new Pen(Color.Black, 2);
            Point Point1 = new Point((scrWidth / topPixels), 0);
            Size size = new Size(scrWidth / topPixels, scrHeight / (leftPixels + 1));
            for (int i = 1; i <= leftPixels; i++)
            {
                Point1 = new Point(0, i * (scrHeight / (leftPixels + 1)));
                g.DrawRectangle(linePen, new Rectangle(Point1, size));
            }

            for (int i = 1; i <= rightPixels; i++)
            {
                Point1 = new Point((topPixels - 1) * (scrWidth/topPixels), i * (scrHeight / (rightPixels + 1)));
                g.DrawRectangle(linePen, new Rectangle(Point1, size));
            }

            for (int i = 0; i < topPixels; i++)
            {
                Point1 = new Point(i * (scrWidth/topPixels), 0);
                g.DrawRectangle(linePen, new Rectangle(Point1, size));
            }
        }
        private void drawScreen()
        {
            clearScreen();
            drawReferanceFrame();
            refr();
        }

        private void updateScreen()
        {
            refr();
        }
        
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (port1 != null && port1.IsOpen)
                port1.Close();
        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (ConnectButton.Text == "Connect" && !comboBox1.GetItemText(comboBox1.SelectedItem).Equals(""))
            {
                port1 = new SerialPort();
                port1.BaudRate = 9600;
                port1.PortName = comboBox1.GetItemText(comboBox1.SelectedItem);
                port1.Open();
                ConnectButton.Text = "Terminate Connection";
            }
            else
            {
                if (comboBox1.GetItemText(comboBox1.SelectedItem).Equals(""))
                {
                    MessageBox.Show("Select Port!!");
                }
                else
                {
                    ConnectButton.Text = "Connect";
                    port1.Close();
                }
            }
        }

        private Boolean sendData()
        {
            StringBuilder output = new StringBuilder("");
            
            for (int i = 0; i < numberOfPixels; i++)
                output.Append(rgbMessageConverter(frame.getPixel(i)));
            String final = output.ToString();
            port1.Write(final);
            return true;
        }
        private void senderButton_Click(object sender, EventArgs e)
        {
            pictureBox.BackColor = Color.FromArgb(scrollR.Value, scrollG.Value, scrollB.Value);
            //port1.WriteLine(rgbConverter());
        }
        Timer timer;
        
        private void continiousButton_Click(object sender, EventArgs e)
        {
            if (continiousButton.Text == "Continious Led Control")
            {
                continiousButton.Text = "Stop";
                timer = new Timer();
                timer.Tick += new EventHandler(timerTick);
                timer.Interval = 1000;
                timer.Start();
            }
            else
            {
                continiousButton.Text = "Continious Led Control";
                timer.Stop();
            }
        }
        private void timerTick(object Sender, EventArgs e)
        {
            sendData();
            timer.Start();
        }
        private string rgbMessageConverter(Color pixel)
        {
            int R = pixel.R, G = pixel.G, B = pixel.B;
            G = (G * 6) / 8;
            B = B / 4;
            char r1, g1, b1;
            r1 = (char)R;
            g1 = (char)G;
            b1 = (char)B;
            string message = "" + r1 + g1 + b1;
            return message;
        }

        private Color getScrollColor()
        {
            return Color.FromArgb(scrollR.Value, scrollG.Value, scrollB.Value);
        }
        private void allLed_Click(object sender, EventArgs e)
        {
            Color temp = getScrollColor();
            for (int i = 0; i < numberOfPixels; i++)
                frame.setPixel(temp, i);
            updateScreen();
        }
        private void leftButton_Click(object sender, EventArgs e)
        {
            Color temp = getScrollColor();
            for (int i = 0; i < leftPixels; i++)
                frame.setPixel(temp, i);
            updateScreen();
        }
        private void topButton_Click(object sender, EventArgs e)
        {
            Color temp = getScrollColor();
            int limit = leftPixels + topPixels;
            for (int i = leftPixels; i < limit; i++)
                frame.setPixel(temp, i);
            updateScreen();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            Color temp = getScrollColor();
            for (int i = leftPixels + topPixels + 1; i < numberOfPixels; i++)
                frame.setPixel(temp, i);
            updateScreen();
        }
        private Pixel[] pixelPositionOfFrame(int position)
        {
            //First pixel[0] is uppleft pixel, pixel[1] is downright pixel
            int scrWidth = pictureBox.Width, scrHeight = pictureBox.Height;
            Pixel[] pixels = new Pixel[2];
            pixels[0] = new Pixel(0, 0);
            pixels[1] = new Pixel(1, 1);
            if (position <= leftPixels)
            {
                pixels[0].X = 0;
                pixels[0].Y = (leftPixels - position + 1) * (scrHeight / (leftPixels + 1));
                pixels[1].X = scrWidth / topPixels;
                pixels[1].Y = (leftPixels - position + 1) * (scrHeight / (leftPixels + 1)) + (scrHeight / (leftPixels + 1));
            }
            else if (position > leftPixels && position <= leftPixels + topPixels)
            {
                pixels[0].X = (position - leftPixels - 1) * (scrWidth / (topPixels));
                pixels[0].Y = 0;
                pixels[1].X = (position - leftPixels - 1) * (scrWidth / (topPixels)) + scrWidth / topPixels;
                pixels[1].Y = (scrHeight / (leftPixels + 1));
            }
            else if (position > leftPixels + topPixels && position <= leftPixels + topPixels + rightPixels)
            {
                pixels[0].X = (scrWidth / topPixels) * (topPixels - 1);
                pixels[0].Y = (-rightPixels - topPixels + position) * (scrHeight / (rightPixels + 1));
                pixels[1].X = (scrWidth / topPixels) * (topPixels - 1) + scrWidth / topPixels;
                pixels[1].Y = (-rightPixels - topPixels + position) * (scrHeight / (rightPixels + 1)) + (scrHeight / (rightPixels + 1));
            }
            return pixels;
        }
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            //Implement here

            //
            updateScreen();//refresh screen after click
        }
    }
}

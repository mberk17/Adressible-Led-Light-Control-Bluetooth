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
using System.Runtime.InteropServices;



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
        public const int leftPixels = 7, rightPixels = 7, topPixels = 12;
        public const int numberOfPixels = leftPixels + rightPixels + topPixels;
        public const int bytesPerMessage = 42;
        public const int numberOfMessage = (numberOfPixels * 3 - 1) / bytesPerMessage + 1;
        public Boolean messageOnProgress = false;

        
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
        
        private void drawScreen()
        {
            clearScreen();
            for(int i = 0; i < numberOfPixels; i++)
                drawColors(i);
            drawReferanceFrame();
            updateScreen();
        }
        

        private void updateScreen(){refr();}
        
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
                port1.DataReceived += OnDataReceived;
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

        public void OnDataReceived(object source, EventArgs e)
        {
            SerialPort sp = (SerialPort)source;
            int temp = (char)sp.ReadChar();
            if (temp == numberOfMessage)
                messageOnProgress = false;
            else
            {
                StringBuilder output = new StringBuilder("");
                for (int i = temp * bytesPerMessage / 3; i < numberOfPixels && i < (temp+1) * bytesPerMessage / 3; i++)
                    output.Append(rgbMessageConverter(frame.getPixel(i)));
                String final = output.ToString();
                port1.Write(final);
            }
        }
        private Boolean sendData()
        {
            if (!messageOnProgress)
            {
                messageOnProgress = true;
                StringBuilder output = new StringBuilder("");
                for (int i = 0; i < bytesPerMessage / 3; i++)
                    output.Append(rgbMessageConverter(frame.getPixel(i)));
                String final = output.ToString();
                port1.Write(final);
                return true;
            }
            else
                return false;
            
        }
        private void senderButton_Click(object sender, EventArgs e)
        {
            Boolean result = sendData();
            if(!result)
                MessageBox.Show("Failed to send data");
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
            updateColorArrayFromScreen();
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
            drawScreen();
        }
        private void leftButton_Click(object sender, EventArgs e)
        {
            Color temp = getScrollColor();
            for (int i = 0; i < leftPixels; i++)
                frame.setPixel(temp, i);
            drawScreen();
        }
        private void topButton_Click(object sender, EventArgs e)
        {
            Color temp = getScrollColor();
            int limit = leftPixels + topPixels;
            for (int i = leftPixels; i < limit; i++)
                frame.setPixel(temp, i);
            drawScreen();
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            Color temp = getScrollColor();
            for (int i = leftPixels + topPixels; i < numberOfPixels; i++)
                frame.setPixel(temp, i);
            drawScreen();
        }
        private void drawReferanceFrame()
        {
            int scrWidth = pictureBox.Width, scrHeight = pictureBox.Height;
            Pen linePen = new Pen(Color.Black, 2);
            Point Point1 = new Point((scrWidth / topPixels), 0);
            Size size = new Size(scrWidth / topPixels, scrHeight / (leftPixels + 1));
            for (int i = 0; i < numberOfPixels; i++)
            {
                Rectangle rect = pixelPositionOfFrame(i);
                g.DrawRectangle(linePen, rect);
            }
        }
        private void drawColors(int position)
        {
            Rectangle rect = pixelPositionOfFrame(position);
            Color temp = frame.getPixel(position);
            Brush brush = new SolidBrush(temp);
            g.FillRectangle(brush, rect);
        }
        private void scroll_ValueChanged(object sender, EventArgs e)
        {
            int squareDimension = 50;
            int y = pictureBox.Height;
            int x = pictureBox.Width / 2;
            Color temp = getScrollColor();
            Brush brush = new SolidBrush(temp);
            g.FillRectangle(brush, x - squareDimension / 2, y - squareDimension, squareDimension, squareDimension);
            updateScreen();

        }

        private Rectangle pixelPositionOfFrame(int pos)
        {
            int position = pos + 1;
            //First pixel[0] is uppleft pixel, pixel[1] is downright pixel
            int scrWidth = pictureBox.Width, scrHeight = pictureBox.Height;
            Rectangle rect;

            Point[] pixels = new Point[2];
            pixels[0] = new Point();
            pixels[1] = new Point();
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
            
            pixels[1] = Point.Subtract(pixels[1],new Size(pixels[0]));
            
            rect = new Rectangle(pixels[0], new Size(pixels[1]));
            return rect;
        }
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < numberOfPixels; i++)
            {
                Rectangle which = pixelPositionOfFrame(i);
                if (e.X >= which.X && e.X <= which.X + which.Width && e.Y >= which.Y && e.Y <= which.Y + which.Height)
                    frame.setPixel(getScrollColor(), i);
            }
            drawScreen();
        }
        //Windows Dll's Import
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);

        private void button1_Click(object sender, EventArgs e)
        {
            updateColorArrayFromScreen();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private Color getColorAt(Point pixel)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, pixel.X, pixel.Y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }
        private Boolean updateColorArrayFromScreen()
        {
            Rectangle resolution = Screen.PrimaryScreen.Bounds; //taking screen resolution
            int screenHeight = resolution.Height;
            int screenWidth = resolution.Width;
            Color tempColor;
            Point tempPoint;
            for (int i = 1; i <= leftPixels; i++)
            {
                tempPoint = new Point( screenWidth / 200, (leftPixels - i + 1) * (screenHeight / (leftPixels + 1)));
                tempColor = getColorAt(tempPoint);
                frame.setPixel(tempColor, i - 1);
            }

            for (int i = 1; i <= rightPixels; i++)
            {
                tempPoint = new Point(screenWidth - (screenWidth / 200), (rightPixels - i + 1) * (screenHeight / (rightPixels + 1)));
                tempColor = getColorAt(tempPoint);
                frame.setPixel(tempColor, leftPixels + topPixels + i - 1);
            }

            for (int i = 1; i <= topPixels; i++)
            {
                tempPoint = new Point(i * (screenWidth / (topPixels + 1)), screenHeight / 100);
                tempColor = getColorAt(tempPoint);
                frame.setPixel(tempColor, leftPixels + i - 1);
            }
            drawScreen();
            return true;
        }
        
    }
}

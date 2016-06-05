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
            int[] pixel = new int[3];
            pixel[0] = 65;
            pixel[1] = 66;
            pixel[2] = 67;
            frame.setPixel(8, pixel);
            frame.setPixel(13, pixel);
            frame.setPixel(18, pixel);
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
            g.Clear(Color.FromArgb(255, 255, 255));
            Pen linePen = new Pen(Color.Black, 2);
            Point linePoint1 = new Point((scrWidth / topPixels), 0);
            Point linePoint2 = new Point((scrWidth / topPixels), scrHeight);
            g.DrawLine(linePen, linePoint1, linePoint2);
            linePoint1 = new Point(0, (scrHeight / leftPixels));
            linePoint2 = new Point(scrWidth, (scrHeight / leftPixels));
            g.DrawLine(linePen, linePoint1, linePoint2);
            linePoint1 = new Point((scrWidth / topPixels) * (topPixels - 1), 0);
            linePoint2 = new Point((scrWidth / topPixels) * (topPixels - 1), scrHeight);
            g.DrawLine(linePen, linePoint1, linePoint2);

            for (int i = 1; i < leftPixels; i++)
            {
                linePoint1 = new Point(0, i * (scrHeight / leftPixels));
                linePoint2 = new Point((scrWidth / topPixels), i * (scrHeight / leftPixels));
                g.DrawLine(linePen, linePoint1, linePoint2);
            }

            for (int i = 1; i < rightPixels; i++)
            {
                linePoint1 = new Point((scrWidth / topPixels) * (topPixels - 1), i * (scrHeight / leftPixels));
                linePoint2 = new Point(scrWidth, i * (scrHeight / leftPixels));
                g.DrawLine(linePen, linePoint1, linePoint2);
            }

            for (int i = 1; i < topPixels; i++)
            {
                linePoint1 = new Point((scrWidth / topPixels) * i, 0);
                linePoint2 = new Point((scrWidth / topPixels) * i, (scrHeight / rightPixels));
                g.DrawLine(linePen, linePoint1, linePoint2);
            }
        }
        private void drawScreen()
        {
            clearScreen();
            drawReferanceFrame();
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
                for (int j = 0; j < 3; j++)
                {
                    output.Append(rgbMessageConverter(frame.getPixel(i)[0], frame.getPixel(i)[1], frame.getPixel(i)[2]));
                }
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
        private void allLed_Click(object sender, EventArgs e)
        {
            pictureBox.BackColor = Color.FromArgb(scrollR.Value,scrollG.Value,scrollB.Value);
            
        }
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
        private string rgbMessageConverter(int R, int G, int B)
        {
            G = (G * 6) / 8;
            B = B / 4;
            char r1, g1, b1;
            r1 = (char)R;
            g1 = (char)G;
            b1 = (char)B;
            string message = "" + r1 + g1 + b1;
            return message;
        }

        private void topButton_Click(object sender, EventArgs e)
        {
            char output = (char) 65;
            String text = output + "" + output + "" + output;
            port1.Write(text);
        }

        private void leftButton_Click(object sender, EventArgs e)
        {

        }

        private void rightButton_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {

        }
    }
}

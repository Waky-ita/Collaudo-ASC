using System;
using System.Drawing;
using System.IO.Ports;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Serial_Port__Communication
{

    public partial class MainWindow : Form
    {
        private void DrawRectangle()
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.DrawRectangle(myPen, new Rectangle(2, 2, 20, 30));
            myPen.Dispose();
            formGraphics.Dispose();
        }
        string dataIN;

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cBoxCOMPort.Items.AddRange(ports);
            
        }
        
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = cBoxCOMPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);
                serialPort1.WriteTimeout = 500;
                serialPort1.ReadTimeout = 500;
                serialPort1.Open();
                dataIN = serialPort1.ReadExisting();


                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try 
            { 
                if (serialPort1.IsOpen)
                {
                    
                    serialPort1.WriteLine("@COLLAUDO" + DateTime.Now.ToString("yyyy/dd/MM HH.mm.ss"));
                    textBox.Text += "@COLLAUDO" + DateTime.Now.ToString("yyyy/dd/MM HH.mm.ss") + Environment.NewLine;
                    
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void cBoxCOMPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            
            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
            

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataIN = serialPort1.ReadExisting();
            this.Invoke(new EventHandler(ShowData));
        }

        private void ShowData(object sender, EventArgs e)
        {
            textBox.Text += dataIN + Environment.NewLine;
            


        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == (Keys.Alt | Keys.Q))
            {
                MessageBox.Show("Created by Oleksandr Bashtan @Waky\n \n" +
                                "Email: oleksandr.bashtan@gmail.com");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

    }
}

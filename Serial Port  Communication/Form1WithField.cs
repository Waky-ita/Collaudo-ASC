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

namespace Serial_Port__Communication
{
    public partial class Form1 : Form
    {
        string dataOUT;      
        
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string [] ports = SerialPort.GetPortNames(); 
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
                
                serialPort1.Open();
                progressBar1.Value = 100;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                   
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen) 
            {
                serialPort1.Close();
                progressBar1.Value = 0;
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                //dataOUT = tBoxDataSend.Text;               
                //serialPort1.Write(dataOUT+DateTime.Now.ToString("HH/mm/ss"));
                serialPort1.Write("COLLAUDO^" + DateTime.Now.ToString("HH/mm/ss"));
            }
        }

        private void tBoxDataSend_TextChanged(object sender, EventArgs e)
        {

        }

        private void cBoxCOMPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

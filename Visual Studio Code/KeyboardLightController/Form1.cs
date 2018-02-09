using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace KeyboardLightController
{
    public partial class Form1 : Form
    {

        private static SerialPort arduinoComPort = new SerialPort();
        private string comPortToUse = "";

        public Form1()
        {
            InitializeComponent();
            try
            {
                // add list of com ports to drop-down
                this.toolStripComboBox2.Items.AddRange(SerialPort.GetPortNames());

                if (Properties.Settings.Default.COMPortToUse == "")
                {
                    MessageBox.Show("It appears this is the first time this application has been used. Please select a COM port before selecting a color.");
                }
                else
                {
                    // set the com port to the user saved port
                    this.toolStripComboBox2.Text = Properties.Settings.Default.COMPortToUse;
                    this.toolStripComboBox2.SelectedText = Properties.Settings.Default.COMPortToUse;
                    comPortToUse = Properties.Settings.Default.COMPortToUse;
                    SetSerialPortSettings();
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            contextMenuStrip1.Close();
            // connect to serial port of AVR and send appropriate command
            // commands are below
            //"Off",
            //"White",
            //"Red",
            //"Green",
            //"Blue",
            //"Orange",
            //"Yellow",
            //"Purple",
            //"Random"
            string serialMessage = "";

            if(this.toolStripComboBox1.Text == "Off")
                serialMessage = "ALLOFF";
            else if (this.toolStripComboBox1.Text == "White")
                serialMessage = "WHITE#";
            else if (this.toolStripComboBox1.Text == "Red")
                serialMessage = "RED###";
            else if (this.toolStripComboBox1.Text == "Green")
                serialMessage = "GREEN#";
            else if (this.toolStripComboBox1.Text == "Blue")
                serialMessage = "BLUE##";
            else if (this.toolStripComboBox1.Text == "Orange")
                serialMessage = "ORANGE";
            else if (this.toolStripComboBox1.Text == "Yellow")
                serialMessage = "YELLOW";
            else if (this.toolStripComboBox1.Text == "Purple")
                serialMessage = "PURPLE";
            else if (this.toolStripComboBox1.Text == "Random")
                serialMessage = "RANDOM";

            arduinoComPort.Open();

            arduinoComPort.Write(StrToByteArray(serialMessage), 0, serialMessage.Length);

            arduinoComPort.Close();
        }
        private static byte[] StrToByteArray(string str)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            return encoding.GetBytes(str);
        }
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // save the com port the user selects
            Properties.Settings.Default.COMPortToUse = this.toolStripComboBox2.SelectedItem.ToString();
            Properties.Settings.Default.Save();

            comPortToUse = this.toolStripComboBox2.SelectedItem.ToString();
            SetSerialPortSettings();
        }
        private void SetSerialPortSettings()
        {
            arduinoComPort.PortName = comPortToUse;
            arduinoComPort.BaudRate = 115200;
            arduinoComPort.Parity = Parity.None;
            arduinoComPort.StopBits = StopBits.One;
            arduinoComPort.DtrEnable = true;
            arduinoComPort.RtsEnable = false;
        }

    }
}

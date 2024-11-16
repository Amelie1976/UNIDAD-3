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
using System.Media;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private SerialPort SerialPort;
        private bool isBlinking;
        private SoundPlayer SoundPlayer;
        public Form1()
        {
            InitializeComponent();
            SerialPort = new SerialPort("COM6", 9600);
            SerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            botonComenzar.Click += botonComenzar_Click_1;
            timer1.Interval = 500;//Intervalo de parpadeo en milisegundos
            timer1.Tick += timer1_Tick_1;
            SoundPlayer = new SoundPlayer("C:\\Users\\ameli\\Documents\\ARCHIVOS UNIDAD 3\\ARDUINO\\sensor de lluvia\\Emergency Siren Police Wail 02.wav");

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
        }

        void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)

        {
            try
            {
                string data = SerialPort.ReadLine();
                this.Invoke(new Action(() =>
                {
                    label1.Text = "Sensor Status: " + (data.Trim() == "0" ? "Water Detected" : "No Water");

                    if (data.Trim() == "0")
                    {
                        if (!isBlinking)
                        {
                            timer1.Start();
                            SoundPlayer.PlayLooping();
                            pictureBox1.BackColor = System.Drawing.Color.Red;
                            isBlinking = true;
                        }
                    }
                    else
                    {
                        if (isBlinking)
                        {
                            timer1.Stop();
                            SoundPlayer.Stop();
                            pictureBox1.BackColor = System.Drawing.Color.Gray;
                            isBlinking = false;
                        }
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

 
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Acción al hacer clic en la PictureBox (si es necesario)
        }

        private void Salir_Click_1(object sender, EventArgs e)
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            Application.Exit();
        }

        private void botonComenzar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!SerialPort.IsOpen)
                {
                    SerialPort.Open();
                    MessageBox.Show("Puerto abierto correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el puerto: " + ex.Message);
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            pictureBox1.BackColor = pictureBox1.BackColor == System.Drawing.Color.Gray
               ? System.Drawing.Color.Yellow
               : System.Drawing.Color.Gray;
        }
    }
}

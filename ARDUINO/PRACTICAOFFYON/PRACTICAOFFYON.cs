﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Windows.Forms;

namespace practica1
{
    public partial class Form1 : Form
    {
        System.IO.Ports.SerialPort Arduino;
        public Form1()
        {
            InitializeComponent();
            Arduino = new System.IO.Ports.SerialPort();
            Arduino = new SerialPort("COM5", 9600);
            Arduino.Open();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Arduino.Write("F");

        }
        private void Form1_Load(object sender,EventArgs e)
        { 
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Arduino.Write("E");
        }
    }
}

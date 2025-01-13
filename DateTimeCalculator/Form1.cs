using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DateTimeCalculator
{
    public partial class Form1 : Form
    {
        private Timer timer;

        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeTimer()
        {            
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += OnTimerTick; 
            timer.Start(); 
        }

        private void OnTimerTick(object sender, EventArgs e)
        {            
            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();       
            labelTimestamp.Text = $"{unixTimestamp}";
        }
    }
}

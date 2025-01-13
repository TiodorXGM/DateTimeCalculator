using Guna.UI2.WinForms;
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

        void InitializeTimer()
        {            
            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += OnTimerTick; 
            timer.Start(); 
        }

        void OnTimerTick(object sender, EventArgs e)
        {            
            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();       
            labelTimestamp.Text = $"{unixTimestamp}";
        }

        void DateComparer(DateTime date1, DateTime date2)
        {
            TimeSpan difference = date2 - date1;

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            if (MenuTabControl.TabMenuVisible)
            {
                MenuTabControl.TabMenuVisible = false;
            }
            else 
            {
                MenuTabControl.TabMenuVisible = true;
            }
        }

        private void MenuTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {           
            int selectedIndex = MenuTabControl.SelectedIndex;
          
            switch (selectedIndex)
            {
                case 0:
                    CategoryLabel.Text = "2Date Comparer";
                    break;
                case 1:
                    CategoryLabel.Text = "2Date Working Counter";
                    break;
                case 2:
                    CategoryLabel.Text = "Leap Counter";
                    break;
                case 3:
                    CategoryLabel.Text = "Time Converter";
                    break;
                case 4:
                    CategoryLabel.Text = "Birthday Informer";
                    break;
               
            }
        }

        private void guna2Panel4_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void guna2Panel4_Click(object sender, EventArgs e)
        {
            guna2CircleButton1.PerformClick();
            guna2CircleButton1.Focus();
        }
    }
}

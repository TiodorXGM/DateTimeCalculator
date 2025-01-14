﻿using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

            TimePicker1.Value = DateTime.Now;
            TimePicker2.Value = DateTime.Now;


            DateComparer(TimePicker1.Value, TimePicker2.Value);
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
                    DateComparer(TimePicker1.Value, TimePicker2.Value);
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

        private void label6_Click(object sender, EventArgs e)
        {

        }


        void DateComparer(DateTime date1, DateTime date2)
        {
            date1 = date1.AddMilliseconds(-date1.Millisecond);
            date2 = date2.AddMilliseconds(-date2.Millisecond);

            int ms1 = int.Parse(DC_NumMs1.Value.ToString());
            int ms2 = int.Parse(DC_NumMs2.Value.ToString());

            if (date1 > date2 || (date1 == date2 && ms1 > ms2))
            {
                var tempDate = date1;
                date1 = date2;
                date2 = tempDate;

                var tempMs = ms1;
                ms1 = ms2;
                ms2 = tempMs;
            }

            TimeSpan difference = date2 - date1;

          
            long totalMilliseconds1 = (long)(date1 - DateTime.MinValue).TotalMilliseconds + ms1;
            long totalMilliseconds2 = (long)(date2 - DateTime.MinValue).TotalMilliseconds + ms2;

           
            long totalDifference = totalMilliseconds2 - totalMilliseconds1;

       
            int years = (int)(totalDifference / (365L * 24 * 60 * 60 * 1000));
            totalDifference %= (365L * 24 * 60 * 60 * 1000);

            int months = (int)(totalDifference / (30L * 24 * 60 * 60 * 1000)); 
            totalDifference %= (30L * 24 * 60 * 60 * 1000);

            int days = (int)(totalDifference / (24L * 60 * 60 * 1000));
            totalDifference %= (24L * 60 * 60 * 1000);

            int hours = (int)(totalDifference / (60L * 60 * 1000));
            totalDifference %= (60L * 60 * 1000);

            int minutes = (int)(totalDifference / (60L * 1000));
            totalDifference %= (60L * 1000);

            int seconds = (int)(totalDifference / 1000);
            int milliseconds = Math.Abs((int)(totalDifference % 1000));

            DC_Years_Value.Text = years.ToString();
            DC_Months_Value.Text = months.ToString();
            DC_Days_Value.Text = days.ToString();
            DC_Hours_Value.Text = hours.ToString();
            DC_Minutes_Value.Text = minutes.ToString();
            DC_Sec_Value.Text = seconds.ToString();
            DC_Ms_Value.Text = milliseconds.ToString();
            DC_TotalDays_Value.Text = difference.Days.ToString();


        }

        private void TimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }

        private void TimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }

        private void DC_MsBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отклоняем ввод
            }
        }

        private void DC_MsBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Отклоняем ввод
            }
        }

    

        private void DC_MsBox2_TextChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }

        private void DC_MsBox1_TextChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }

        private void DC_NumMs1_ValueChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }

        private void DC_NumMs2_ValueChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }
    }
}


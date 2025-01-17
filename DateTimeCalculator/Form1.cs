using Guna.UI2.WinForms;
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
        private Timer unixTimer;
        public Form1()
        {
            InitializeComponent();
            InitializeTimer();
            TimePicker1.Value = DateTime.Now;
            TimePicker2.Value = DateTime.Now;
            DWC_DatePicker1.Value = DateTime.Now;
            DWC_DatePicker2.Value = DateTime.Now;

            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }          

        void InitializeTimer()
        {
            unixTimer = new Timer();
            unixTimer.Interval = 1000;
            unixTimer.Tick += OnTimerTick;
            unixTimer.Start();
        }

        void OnTimerTick(object sender, EventArgs e)
        {
            long unixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            labelTimestamp.Text = $"{unixTimestamp}";
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

            if (BirthdayTimer != null && BirthdayTimer.Enabled)
            {
                BirthdayTimer.Stop();
            }

            switch (selectedIndex)
            {
                case 0:
                    CategoryLabel.Text = "2Date Comparer";
                    DateComparer(TimePicker1.Value, TimePicker2.Value);
                    break;
                case 1:
                    CategoryLabel.Text = "2Date Working Counter";
                    DateWoringCounter();
                    break;
                case 2:
                    CategoryLabel.Text = "Leap Counter";
                    LeapCounter();
                    break;
                case 3:
                    CategoryLabel.Text = "Time Converter";
                    TimeConverter();
                    break;
                case 4:
                    CategoryLabel.Text = "Birthday Informer";                   
                    BirthdayInformerInit();
                    break;
            }
        }
        private void guna2Panel4_Click(object sender, EventArgs e)
        {
            guna2CircleButton1.PerformClick();
            guna2CircleButton1.Focus();
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

            DC_Years_Value.Text     = years.ToString();
            DC_Months_Value.Text    = months.ToString();
            DC_Days_Value.Text      = days.ToString();
            DC_Hours_Value.Text     = hours.ToString();
            DC_Minutes_Value.Text   = minutes.ToString();
            DC_Sec_Value.Text       = seconds.ToString();
            DC_Ms_Value.Text        = milliseconds.ToString();
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

        private void DC_NumMs1_ValueChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }

        private void DC_NumMs2_ValueChanged(object sender, EventArgs e)
        {
            DateComparer(TimePicker1.Value, TimePicker2.Value);
        }

        void DateWoringCounter()
        {
            var date1 = DWC_DatePicker1.Value;
            var date2 = DWC_DatePicker2.Value;

            int count = 0;

            if (date1 > date2 || (date1 == date2))
            {
                var tempDate = date1;
                date1 = date2;
                date2 = tempDate;
            }

            for (DateTime date = date1; date <= date2; date = date.AddDays(1))
            {
                if (DWC_CB_Monday.Checked)
                {
                    if (date.DayOfWeek == DayOfWeek.Monday) count++;
                }
                if (DWC_CB_Tuesday.Checked)
                {
                    if (date.DayOfWeek == DayOfWeek.Tuesday) count++;
                }
                if (DWC_CB_Wednesday.Checked)
                {
                    if (date.DayOfWeek == DayOfWeek.Wednesday) count++;
                }
                if (DWC_CB_Thursday.Checked)
                {
                    if (date.DayOfWeek == DayOfWeek.Thursday) count++;
                }
                if (DWC_CB_Friday.Checked)
                {
                    if (date.DayOfWeek == DayOfWeek.Friday) count++;
                }
                if (DWC_CB_Saturday.Checked)
                {
                    if (date.DayOfWeek == DayOfWeek.Saturday) count++;
                }
                if (DWC_CB_Sunday.Checked)
                {
                    if (date.DayOfWeek == DayOfWeek.Sunday) count++;
                }
            }

            DWC_Label_WorkingDays.Text = count.ToString();
        }

        private void DWC_DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_DatePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_CB_Monday_CheckedChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_CB_Tuesday_CheckedChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_CB_Wednesday_CheckedChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_CB_Thursday_CheckedChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_CB_Friday_CheckedChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_CB_Saturday_CheckedChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }

        private void DWC_CB_Sunday_CheckedChanged(object sender, EventArgs e)
        {
            DateWoringCounter();
        }


        void LeapCounter()
        {
            var date1 = LC_DatePicker1.Value;
            var date2 = LC_DatePicker2.Value;

            if (date1 > date2 || (date1 == date2))
            {
                var tempDate = date1;
                date1 = date2;
                date2 = tempDate;
            }

            int count = 0;

            for (int year = date1.Year; year <= date2.Year; year++)
            {
                if (DateTime.IsLeapYear(year)) count++;
            }

            LC_LeapYears.Text = count.ToString();
        }

        private void LC_DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            LeapCounter();
        }

        private void LC_DatePicker2_ValueChanged(object sender, EventArgs e)
        {
            LeapCounter();
        }

        public enum TimeType
        {
            Years,
            Months,
            Days,
            Hours,
            Minutes,
            Seconds
        }

        int GetTimeFactor(TimeType type)
        {
            int timeFactor = 1;
            if (type == TimeType.Years)   timeFactor = 60 * 60 * 24 * 365;
            if (type == TimeType.Months)  timeFactor = 60 * 60 * 24 * 30;
            if (type == TimeType.Days)    timeFactor = 60 * 60 * 24;
            if (type == TimeType.Hours)   timeFactor = 60 * 60;
            if (type == TimeType.Minutes) timeFactor = 60;
            if (type == TimeType.Seconds) timeFactor = 1;
            return timeFactor;
        }
        void TimeConverter()
        {
            string valueType = TC_Combo_Right.Items[TC_Combo_Right.StartIndex].ToString();

            TimeType rightType = (TimeType)TC_Combo_Right.SelectedIndex;
            TimeType leftType = (TimeType)TC_Combo_Left.SelectedIndex; 
       
            decimal leftFactor = GetTimeFactor(leftType);
            decimal rightFactor = GetTimeFactor(rightType);

            decimal time = (TC_NumLeft.Value * leftFactor / rightFactor);

            TC_FinalValue.Text = (time % 1 == 0)
                               ? time.ToString("F0")
                               : time.ToString("F8");

            TC_FinalValue_Type.Text = rightType.ToString(); ;
        }

        private void TC_NumLeft_ValueChanged(object sender, EventArgs e)
        {
            TimeConverter();
        }

        private void TC_Combo_Left_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeConverter();
        }

        private void TC_Combo_Right_SelectedIndexChanged(object sender, EventArgs e)
        {
            TimeConverter();
        }

        Timer BirthdayTimer;

        void InitializeBirthdayTimer()
        {
            BirthdayTimer = new Timer();
            BirthdayTimer.Interval = 1000;
            BirthdayTimer.Tick += OnBirthdayTimerTick;
            UpdateIndicator(false);
        }
       

        void OnBirthdayTimerTick(object sender, EventArgs e)
        {
            BirthdayInformer();
        }

        void BirthdayInformerInit()
        {
            BI_DatePicker.MaxDate = DateTime.Now;
            BI_Live_Years.Text = "-";
            BI_Live_Months.Text = "-";
            BI_Live_Days.Text = "-";
            BI_Live_Hours.Text = "-";
            BI_Live_Minutes.Text = "-";
            BI_Live_Seconds.Text = "-";
            BI_Live_MSeconds.Text = "-";

            BI_18_Years.Text = "-";
            BI_18_Months.Text = "-";
            BI_18_Days.Text = "-";
            BI_18_Hours.Text = "-";
            BI_18_Minutes.Text = "-";
            BI_18_Seconds.Text = "-";
            BI_18_MSeconds.Text = "-";

            BI_N_Years.Text = "-";
            BI_N_Months.Text = "-";
            BI_N_Days.Text = "-";
            BI_N_Hours.Text = "-";
            BI_N_Minutes.Text = "-";
            BI_N_Seconds.Text = "-";
            BI_N_MSeconds.Text = "-";

            BI_Label_18Years.Text = "-";
            int NumYear = (int)BI_Num_Year.Value;
            BI_Label_NYears.Text = $"Turned {NumYear} so long ago";
            UpdateIndicator(false);
        }

        void BirthdayInformer()
        {
            DateTime birthDate = BI_DatePicker.Value;
            DateTime now = DateTime.Now;
            DateTime eighteenYearsDate = birthDate.AddYears(18);          

            if (now >= eighteenYearsDate)
            {
                var result = CalculateTimeDifference(eighteenYearsDate, now);
                BI_18_Result(result.years, result.months, result.days, result.hours, result.minutes, result.seconds, result.milliseconds);
                BI_Label_18Years.Text = "Turned 18 so long ago";
            }
            else
            {
                var result = CalculateTimeDifference(now, eighteenYearsDate);
                BI_18_Result(result.years, result.months, result.days, result.hours, result.minutes, result.seconds, result.milliseconds);
                BI_Label_18Years.Text = "Turn 18 in";
            }

            var liveResult = CalculateTimeDifference(birthDate, now);
            BI_Live_Result (liveResult.years, liveResult.months, liveResult.days, liveResult.hours, liveResult.minutes, liveResult.seconds, liveResult.milliseconds);

            DateTime NYearsDate = birthDate.AddYears((int)BI_Num_Year.Value);
        
            var NResult = CalculateTimeDifference(now, NYearsDate);
            BI_N_Result(NResult.years, NResult.months, NResult.days, NResult.hours, NResult.minutes, NResult.seconds, NResult.milliseconds);
        }

        

        void BI_Live_Result(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            BI_Live_Years.Text = years.ToString();
            BI_Live_Months.Text = months.ToString();
            BI_Live_Days.Text = days.ToString();
            BI_Live_Hours.Text = hours.ToString();
            BI_Live_Minutes.Text = minutes.ToString();
            BI_Live_Seconds.Text = seconds.ToString();
            BI_Live_MSeconds.Text = milliseconds.ToString();
        }

        void BI_18_Result(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            BI_18_Years.Text = years.ToString();
            BI_18_Months.Text = months.ToString();
            BI_18_Days.Text = days.ToString();
            BI_18_Hours.Text = hours.ToString();
            BI_18_Minutes.Text = minutes.ToString();
            BI_18_Seconds.Text = seconds.ToString();
            BI_18_MSeconds.Text = milliseconds.ToString();
        }

        void BI_N_Result(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
        {
            BI_N_Years.Text = years.ToString();
            BI_N_Months.Text = months.ToString();
            BI_N_Days.Text = days.ToString();
            BI_N_Hours.Text = hours.ToString();
            BI_N_Minutes.Text = minutes.ToString();
            BI_N_Seconds.Text = seconds.ToString();
            BI_N_MSeconds.Text = milliseconds.ToString();
        }

        Color color = new Color();
        void BI_Button_Click(object sender, EventArgs e)
        {
            if (BirthdayTimer == null)
            {
                InitializeBirthdayTimer();
            }

            if (BirthdayTimer.Enabled)
            {
               
                BirthdayTimer.Stop();
                BirthdayInformerInit();
                UpdateIndicator(false); 
            }
            else
            {
                
                BirthdayTimer.Start();
                UpdateIndicator(true); 
            }
        }

        (int years, int months, int days, int hours, int minutes, int seconds, int milliseconds) CalculateTimeDifference(DateTime fromDate, DateTime toDate)
        {               

            long totalMilliseconds1 = (long)(fromDate - DateTime.MinValue).TotalMilliseconds;
            long totalMilliseconds2 = (long)(toDate - DateTime.MinValue).TotalMilliseconds;

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

            return (years, months, days, hours, minutes, seconds, milliseconds);
        }

        private void BI_DatePicker_ValueChanged(object sender, EventArgs e)
        {
            HandleInputChange();
        }

        private void BI_Num_Year_ValueChanged(object sender, EventArgs e)
        {
            HandleInputChange();
        }

        private void HandleInputChange()
        {
            BirthdayInformerInit();
            if (BirthdayTimer != null && BirthdayTimer.Enabled)
            {
                BirthdayTimer.Stop();
                UpdateIndicator(false);                
            }
        }

        void UpdateIndicator(bool isRunning)
        {
            if (isRunning)
            {
                TimerIndicator.FillColor = Color.LawnGreen;
                BI_Button.FillColor = Color.Crimson;
                BI_Button.Text = "STOP";

            }
            else
            {
                TimerIndicator.FillColor = Color.Crimson;
                BI_Button.FillColor = Color.FromArgb(43, 188, 105);
                BI_Button.Text = "OK";
            }
        }

        private void BI_Num_Year_ValueChanged_1(object sender, EventArgs e)
        {
            HandleInputChange();
            BI_Num_Year.Focus();
        }
    }
}


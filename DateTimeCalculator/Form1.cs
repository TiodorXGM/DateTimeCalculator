using DateTimeCalculator.Logic;
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
            TimePicker1.MaxDate = DateTime.Now;
            TimePicker2.MaxDate = DateTime.Now;
            UpdateDateCompareValues(TimePicker1.Value, TimePicker2.Value);

            DWC_DatePicker1.Value = DateTime.Now;
            DWC_DatePicker2.Value = DateTime.Now;            
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
                    UpdateDateCompareValues(TimePicker1.Value, TimePicker2.Value);
                    break;
                case 1:
                    CategoryLabel.Text = "2Date Working Counter";
                    UpdateDateWoringCounterValues();
                    break;
                case 2:
                    CategoryLabel.Text = "Leap Counter";
                    UpdateLeapCounterValue();
                    break;
                case 3:
                    CategoryLabel.Text = "Time Converter";
                    UpdateTimeConverterValue();
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
        void UpdateDateCompareValues(DateTime date1, DateTime date2)
        {           
            int ms1 = int.Parse(DC_NumMs1.Value.ToString());
            int ms2 = int.Parse(DC_NumMs2.Value.ToString());

            var result = DateComparer.CompareDates(date1, ms1, date2, ms2);
            int totalDays = DateComparer.GetTotalDays(date1, ms1, date2, ms2);

            DC_Years_Value.Text = result.Years.ToString();
            DC_Months_Value.Text = result.Months.ToString();
            DC_Days_Value.Text = result.Days.ToString();
            DC_Hours_Value.Text = result.Hours.ToString();
            DC_Minutes_Value.Text = result.Minutes.ToString();
            DC_Sec_Value.Text = result.Seconds.ToString();
            DC_Ms_Value.Text = result.Milliseconds.ToString();
            DC_TotalDays_Value.Text = totalDays.ToString();
        }

        private void TimePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateCompareValues(TimePicker1.Value, TimePicker2.Value);
        }

        private void TimePicker2_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateCompareValues(TimePicker1.Value, TimePicker2.Value);
        }

        private void DC_NumMs1_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateCompareValues(TimePicker1.Value, TimePicker2.Value);
        }

        private void DC_NumMs2_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateCompareValues(TimePicker1.Value, TimePicker2.Value);
        }

        void UpdateDateWoringCounterValues()
        {
            var date1 = DWC_DatePicker1.Value;
            var date2 = DWC_DatePicker2.Value;

            var includedDays = new List<DayOfWeek>();
            if (DWC_CB_Monday.Checked) includedDays.Add(DayOfWeek.Monday);
            if (DWC_CB_Tuesday.Checked) includedDays.Add(DayOfWeek.Tuesday);
            if (DWC_CB_Wednesday.Checked) includedDays.Add(DayOfWeek.Wednesday);
            if (DWC_CB_Thursday.Checked) includedDays.Add(DayOfWeek.Thursday);
            if (DWC_CB_Friday.Checked) includedDays.Add(DayOfWeek.Friday);
            if (DWC_CB_Saturday.Checked) includedDays.Add(DayOfWeek.Saturday);
            if (DWC_CB_Sunday.Checked) includedDays.Add(DayOfWeek.Sunday);
        
            int count = WorkingDaysCounter.CountWorkingDays(date1, date2, includedDays);

            DWC_Label_WorkingDays.Text = count.ToString();
        }

        private void DWC_DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_DatePicker2_ValueChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_CB_Monday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_CB_Tuesday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_CB_Wednesday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_CB_Thursday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_CB_Friday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_CB_Saturday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }

        private void DWC_CB_Sunday_CheckedChanged(object sender, EventArgs e)
        {
            UpdateDateWoringCounterValues();
        }


        void UpdateLeapCounterValue()
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

            LC_LeapYears.Text = LeapCounter.CountLeap(date1,date2).ToString();
        }

        private void LC_DatePicker1_ValueChanged(object sender, EventArgs e)
        {
            UpdateLeapCounterValue();
        }

        private void LC_DatePicker2_ValueChanged(object sender, EventArgs e)
        {
            UpdateLeapCounterValue();
        }

       
        void UpdateTimeConverterValue()
        {
            TimeType leftType = (TimeType)TC_Combo_Left.SelectedIndex;
            TimeType rightType = (TimeType)TC_Combo_Right.SelectedIndex;

            decimal convertedValue = TimeConverter.ConvertTime(TC_NumLeft.Value, leftType, rightType);

      
            TC_FinalValue.Text = (convertedValue % 1 == 0)
                               ? convertedValue.ToString("F0") // Целое число
                               : convertedValue.ToString("F8"); // Дробное число
            TC_FinalValue_Type.Text = rightType.ToString();
        }

        private void TC_NumLeft_ValueChanged(object sender, EventArgs e)
        {
            UpdateTimeConverterValue();
        }

        private void TC_Combo_Left_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTimeConverterValue();
        }

        private void TC_Combo_Right_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTimeConverterValue();
        }

        Timer BirthdayTimer;

        void InitializeBirthdayTimer()
        {
            BirthdayTimer = new Timer();
            BirthdayTimer.Interval = 1000;
            BirthdayTimer.Tick += OnBirthdayTimerTick;
            BI_UpdateIndicator(false);
        }
       
        void OnBirthdayTimerTick(object sender, EventArgs e)
        {
            UpdateBirthdayInformerValues();
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

            BI_NextBirthday_Years.Text = "-";
            BI_NextBirthday_Months.Text = "-";
            BI_NextBirthday_Days.Text = "-";
            BI_NextBirthday_Hours.Text = "-";
            BI_NextBirthday_Minutes.Text = "-";
            BI_NextBirthday_Seconds.Text = "-";
            BI_NextBirthday_MSeconds.Text = "-";

            BI_Label_18Years.Text = "-";
            int NumYear = (int)BI_Num_Year.Value;
            BI_Label_NYears.Text = $"Turned {NumYear} so long ago";
            BI_UpdateIndicator(false);
        }

        
        void UpdateBirthdayInformerValues()
        {
            DateTime birthDate = BI_DatePicker.Value;
            DateTime now = DateTime.Now;
            DateTime eighteenYearsDate = birthDate.AddYears(18);

            bool isEighteenPassed =  BirthdayInformer.CalculateEighteenYearsDifference(birthDate, now, out TimeDifferenceResult eighteenResult);

            if (isEighteenPassed)
            {
                BI_18_Result(eighteenResult);
                BI_Label_18Years.Text = "Turned 18 so long ago";
            }
            else
            {
                BI_18_Result(eighteenResult);
                BI_Label_18Years.Text = "Turn 18 in";
            }

            TimeDifferenceResult liveResult = BirthdayInformer.CalculateLiveDuration(birthDate, now);
            BI_Live_Result(liveResult);

            DateTime NYearsDate = birthDate.AddYears((int)BI_Num_Year.Value);

            int years = (int)BI_Num_Year.Value;
            TimeDifferenceResult nResult = BirthdayInformer.CalculateDifferenceForNYears(birthDate, now, years);
            BI_N_Result(nResult);

            TimeDifferenceResult nextBirthdayResult = BirthdayInformer.CalculateNextBirthday(birthDate, now);
            BI_NextBirthday_Result(nextBirthdayResult);
        }



        void BI_Live_Result(TimeDifferenceResult result)
        {
            BI_Live_Years.Text = result.Years.ToString();
            BI_Live_Months.Text = result.Months.ToString();
            BI_Live_Days.Text = result.Days.ToString();
            BI_Live_Hours.Text = result.Hours.ToString();
            BI_Live_Minutes.Text = result.Minutes.ToString();
            BI_Live_Seconds.Text = result.Seconds.ToString();
            BI_Live_MSeconds.Text = result.Milliseconds.ToString();
        }

        void BI_18_Result(TimeDifferenceResult result)
        {
            BI_18_Years.Text = result.Years.ToString();
            BI_18_Months.Text = result.Months.ToString();
            BI_18_Days.Text = result.Days.ToString();
            BI_18_Hours.Text = result.Hours.ToString();
            BI_18_Minutes.Text = result.Minutes.ToString();
            BI_18_Seconds.Text = result.Seconds.ToString();
            BI_18_MSeconds.Text = result.Milliseconds.ToString();
        }

        void BI_N_Result(TimeDifferenceResult result)
        {
            BI_N_Years.Text = result.Years.ToString();
            BI_N_Months.Text = result.Months.ToString();
            BI_N_Days.Text = result.Days.ToString();
            BI_N_Hours.Text = result.Hours.ToString();
            BI_N_Minutes.Text = result.Minutes.ToString();
            BI_N_Seconds.Text = result.Seconds.ToString();
            BI_N_MSeconds.Text = result.Milliseconds.ToString();
        }

        void BI_NextBirthday_Result(TimeDifferenceResult result)
        {
            BI_NextBirthday_Years.Text = result.Years.ToString();
            BI_NextBirthday_Months.Text = result.Months.ToString();
            BI_NextBirthday_Days.Text = result.Days.ToString();
            BI_NextBirthday_Hours.Text = result.Hours.ToString();
            BI_NextBirthday_Minutes.Text = result.Minutes.ToString();
            BI_NextBirthday_Seconds.Text = result.Seconds.ToString();
            BI_NextBirthday_MSeconds.Text = result.Milliseconds.ToString();
        }
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
                BI_UpdateIndicator(false); 
            }
            else
            {
                
                BirthdayTimer.Start();
                BI_UpdateIndicator(true); 
            }
        }     

        private void BI_DatePicker_ValueChanged(object sender, EventArgs e)
        {
            BI_HandleInputChange();
        }

        private void BI_Num_Year_ValueChanged(object sender, EventArgs e)
        {
            BI_HandleInputChange();
        }

        private void BI_HandleInputChange()
        {
            BirthdayInformerInit();
            if (BirthdayTimer != null && BirthdayTimer.Enabled)
            {
                BirthdayTimer.Stop();
                BI_UpdateIndicator(false);                
            }
        }

        void BI_UpdateIndicator(bool isRunning)
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
            BI_HandleInputChange();
            BI_Num_Year.Focus();
        }

        private void Btn_CloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Btn_Minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

    }
}


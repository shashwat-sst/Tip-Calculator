using System;
using System.Windows.Forms;

namespace Tip_Calculator
{
    public partial class TipCalculator : Form
    {
        public TipCalculator()
        {
            InitializeComponent();
        }

        
        #region Events
        
        
        private void BillTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            decimalinput(sender, e);
        }

        private void TipTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            numberinput(sender, e);
        }

        private void PeopleTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            numberinput(sender, e);

            // only allow natural numbers
            if (PeopleTextBox.Text == "" && e.KeyChar == '0')
                e.Handled = true;
        }

        private void TipMinusButton_Click(object sender, EventArgs e)
        {
            // do nothing if Tip = null
            if (TipTextBox.Text == "" || TipTextBox.Text == "%")
                return;

            // show a message box for minimum tip % reached
            if (TipTextBox.Text == "0" || TipTextBox.Text == "0%")
            {
                MessageBox.Show("Tips lower than 0% of the bill amount are not appreaciated!");
            }
            else
            {
                try
                {
                    // convert tip % input to integer and decreament by 1
                    int tip = Convert.ToInt16(TipTextBox.Text);
                    TipTextBox.Text = (tip - 1).ToString();
                }

                // show a  message box in case of an exception of outof range input
                catch (OverflowException)
                {
                    MessageBox.Show("We don't support this much proportion of tip ");
                }
            }
        }

        private void TipPlusButton_Click(object sender, EventArgs e)
        {
            try
            {
                int tip = 0;

                // if tip % is null assume it is minimum
                if (TipTextBox.Text == "")
                    tip = 0;
                else
                {
                    // convert tip % input to integer and increament by 1
                    tip = Convert.ToInt16(TipTextBox.Text);
                    TipTextBox.Text = (tip + 1).ToString();
                }
            }

            // show a  message box in case of an exception of outof range input
            catch (OverflowException)
            {
                MessageBox.Show("We don't support this much proportion of tip ");
            }
        }

        private void PeopleMinusButton_Click(object sender, EventArgs e)
        {
            // do nothing if Number  of people = null
            if (PeopleTextBox.Text == "")
                return;

            // show a message box for minimum number of people required
            if (PeopleTextBox.Text == "1")
            {
                MessageBox.Show("We need atleast one person to generate a bill.");
            }
            else
            {
                try
                {
                    // convert number of people input to integer and decreament by 1
                    int people = Convert.ToInt16(PeopleTextBox.Text);
                    PeopleTextBox.Text = (people - 1).ToString();
                }

                // show a  message box in case of an exception of outof range input
                catch (OverflowException)
                {
                    MessageBox.Show("Sorry!! We are unable to entertain these many customers at the same time.");
                }
            }
        }

        private void PeoplePlusButton_Click(object sender, EventArgs e)
        {
            try
            {
                int people = 0;

                // if number of people is null assume it is minimum
                if (PeopleTextBox.Text == "")
                    people = 1;
                else
                {
                    // convert tip % input to integer and increament by 1
                    people = Convert.ToInt16(PeopleTextBox.Text);
                    PeopleTextBox.Text = (people + 1).ToString();
                }
            }

            // show a  message box in case of an exception of outof range input
            catch (OverflowException)
            {
                MessageBox.Show("Sorry!! We are unable to entertain this many customers at the same time.");
            }
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            // if any of the details are not filled in show a message box
            if(BillTextBox.Text == "" || TipTextBox.Text == "" || PeopleTextBox.Text == "")
            {
                MessageBox.Show("Please fill all the details!");
            }
            else
            {
                try
                {
                    double bill = Convert.ToDouble(BillTextBox.Text);
                    int tip = Convert.ToInt16(TipTextBox.Text);
                    int people = Convert.ToInt16(PeopleTextBox.Text);
                    var TipPerPerson = (bill * tip) / (people * 100);
                    var total = (bill / people) + TipPerPerson;
                    TipPerPersonTextBox.Text = ("$" + TipPerPerson.ToString("0.00"));
                    TotalTextBox.Text = ("$" + total.ToString("0.00"));
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Please enter valid values for all the fields!");
                }
            }
        }


        #endregion


        #region Constrains

        // do not retain "0" in first place except for decimal
        private void nonzerostart(object sender, KeyPressEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            if (textbox.Text == "0" && e.KeyChar != '.')
            {
                textbox.Text = textbox.Text.Remove(0, 1);
            }
            
        }

        // only allow decimal value
        private void decimalinput(object sender, KeyPressEventArgs e)
        {
            nonzerostart(sender, e);

            // allow nothing accept for controls, digits and "."
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            TextBox textbox = sender as TextBox;
            if ((e.KeyChar == '.') && (textbox.Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }

            // only allow 2 places after decimal
            if (!char.IsControl(e.KeyChar) && textbox.Text.IndexOf('.') < textbox.SelectionStart && textbox.Text.Split('.').Length > 1 && textbox.Text.Split('.')[1].Length == 2)
            {
                e.Handled = true;
            }

            // add zero in first place if "." input in first place
            while (textbox.Text.StartsWith("."))
            {
                textbox.Text = textbox.Text.Insert(0, "0");
            }
            textbox.Select(textbox.Text.Length, 0);
        }

        // only allow positive numbers
        private void numberinput(object sender, KeyPressEventArgs e)
        {
            nonzerostart(sender, e);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        #endregion
    
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerMaintenance
{
    public static class Validator
    {
        public static bool IsPresent(TextBox textBox)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(textBox.Tag+" is require field!","Entry Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                textBox.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsPresent(ComboBox comboBox)
        {
            if(comboBox.SelectedIndex == -1)
            {
                return false;
            }
            return true;
        }
        public static bool IsInt32(TextBox textBox)
        {
            try
            {
                Convert.ToInt32(textBox.Text);
                return true;
            }
            catch(FormatException e)
            {
                MessageBox.Show(
                    textBox.Tag + " must be an Integer number",
                    "Entry Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                textBox.Focus();
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomerMaintenance
{
    public partial class frmModifyCustomer : Form
    {
        public bool addCustomer;

        public Customer customer;

        public frmModifyCustomer()
        {
            InitializeComponent();
        }

        private void frmModifyCustomer_Load(object sender, EventArgs e)
        {
            // Method from this FORM
            this.LoadStateComboBox();

            if (addCustomer)
            {
                this.Text = "Add Customer";
                cboState.SelectedIndex = -1;
            }
            else
            {
                this.Text = "Modify Customer";
                this.DisplayCustomer();
            }
        }

        private void LoadStateComboBox()
        {
            // Declare collection list of State
            List<State> states = new List<State>();

            try
            {
                // retreive data from StateDB class via GetStates() method to store in state object of collection list
                states = StateDB.GetStates();

                // retreive data from stats object collection list into combobox
                cboState.DataSource = states;

                cboState.DisplayMember = "StateName";

                cboState.ValueMember = "StateCode";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message, ex.GetType().ToString()); 
            }
        }
        // Show data of customer into textbox
        private void DisplayCustomer()
        {
            txtName.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtCity.Text = customer.City;
            cboState.SelectedValue = customer.State;
            txtZipcode.Text = customer.ZipCode;
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidData())
            {
                if (addCustomer)
                {
                    // declare object 
                    customer = new Customer();

                    //put data from textbox into Customer Class
                    this.PutCustomerData(customer);

                    try
                    {
                        customer.CustomerID =
                            CustomerDB.AddCustomer(customer);

                        this.DialogResult = DialogResult.OK;
                    }
                    catch( Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            ex.GetType().ToString());
                    }
                }
                else
                {
                    //Create object of Customer class
                    Customer newCustomer = new Customer();

                    // New Customer retreive CustomerID from Old Customer
                    newCustomer.CustomerID = customer.CustomerID;

                    // new data of customer into class Customer from text box
                    this.PutCustomerData(newCustomer);

                    try
                    {
                        if (! CustomerDB.UpdateCustomer(customer, newCustomer))
                        {
                            MessageBox.Show(
                                "Another user has updated or " +
                                "deleted that customer.",
                                "Database Error");

                            this.DialogResult = DialogResult.Retry;
                        }
                        else
                        {
                            // change data
                            customer = newCustomer;

                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            ex.GetType().ToString());
                    }
                }
            }
        }

        public bool IsValidData()
        {
            return
                 Validator.IsPresent(txtName) &&
                 Validator.IsPresent(txtAddress) &&
                 Validator.IsPresent(txtCity) &&
                 Validator.IsPresent(cboState) &&
                 Validator.IsPresent(txtZipcode);
        }

        public void PutCustomerData(Customer customer)
        {
            customer.Name = txtName.Text;
            customer.Address = txtAddress.Text;
            customer.City = txtCity.Text;
            customer.State = cboState.SelectedValue.ToString();
            customer.ZipCode = txtZipcode.Text;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCustomerMaintenance frm = new frmCustomerMaintenance();
            frm.ShowDialog();
        }
    }
}

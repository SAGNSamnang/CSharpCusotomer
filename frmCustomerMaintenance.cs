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
    public partial class frmCustomerMaintenance : Form
    {

        //declare a private field of customer object 
        private Customer customer;

        public frmCustomerMaintenance()
        {
            InitializeComponent();
        }

        private void btnGetCustomer_Click(object sender, EventArgs e)
        {
            if (Validator.IsPresent(txtCustomerID) && Validator.IsInt32(txtCustomerID))
            {
                // declare variable for holding data from textbox that convert already
                int customerID = Convert.ToInt32(txtCustomerID.Text);

                // call method GetCustomer(parameter) from this FORM
                this.GetCustomer(customerID);

                // data of customer in class [Customer]
                if (customer == null)
                {
                    MessageBox.Show(
                        "No customer with this ID. " +
                        "Please try again.",
                        "Customer Not Found");
                    // Clear all info in the controls
                    this.ClearControls();
                }
                else
                {
                    // if customer data not null display data in this current FORM
                    this.DisplayCustomer();
                    txtCustomerID.Focus();
                }
            }
        }
        private void GetCustomer(int customerID)
        {
            try
            {
                // Get customer data into [Customer] from CustomerDB[ GetCustomer(par) ] and via customerID parameter
                customer = CustomerDB.GetCustomer(customerID);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    ex.Message, ex.GetType().ToString());
            }
        }
        private void DisplayCustomer()
        {
            // retreive data from Customer class via object into another text box
            txtName.Text = customer.Name;
            txtAddress.Text = customer.Address;
            txtCity.Text = customer.City;
            txtState.Text = customer.State;
            txtZipcode.Text = customer.ZipCode;
            // disabled two buttons
            btnModify.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void ClearControls()
        {
            txtCustomerID.Text = "";
            txtName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtZipcode.Text = "";
            // enabled two buttons
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            txtCustomerID.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // object of form Modify Customer
            frmModifyCustomer addCustomerForm =
                new frmModifyCustomer();

            // field [ addCustomer ] from frmModifyCustomer to add information of customer from another form to this form
            addCustomerForm.addCustomer = true;

            // Show new form as DilogResult
            DialogResult result = addCustomerForm.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                // customer object that declare get data from another form  via customer field
                customer = addCustomerForm.customer;

                txtCustomerID.Text = customer.CustomerID.ToString();

                this.DisplayCustomer();
            }


        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            frmModifyCustomer modifyCustomerForm = new frmModifyCustomer();

            modifyCustomerForm.addCustomer = false;

            modifyCustomerForm.customer = customer;

            DialogResult result = modifyCustomerForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                customer = modifyCustomerForm.customer;

                //this.DisplayCustomer();

            }else if (result == DialogResult.Retry) 
            {
                this.GetCustomer (customer.CustomerID);

                if (customer != null)
                {
                    this.DisplayCustomer();
                }
                else
                {
                    this.ClearControls();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete " +
                customer.Name + "?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!CustomerDB.DeleteCustomer(customer))
                    {
                        MessageBox.Show("Another user has " +
                            "updated or deleted" +
                            "that customer.", "Database Error");

                        this.GetCustomer(customer.CustomerID);

                        if (customer != null)
                        {
                            this.DisplayCustomer();
                        }
                        else
                        {
                            this.ClearControls();
                        }
                    }
                    else
                    {
                        this.ClearControls();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(
                        ex.Message, ex.GetType().ToString());
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtCustomerID_TextChanged(object sender, EventArgs e)
        {
            if (txtCustomerID.Text == string.Empty)
                this.ClearControls();
        }
    }
}

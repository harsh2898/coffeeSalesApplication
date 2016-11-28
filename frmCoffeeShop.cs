/*
 * Name : Harsh Patel
 * Date: October 4, 2016
 * Purpose:This application allows the user to select multiple coffee type
 * in various quantities.
 * The amount due is displayed.
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Coffee_Sales
{
    public partial class frmCoffeeShop : Form
    {
        // module level variables
        // objects are initialised to null and numbers to 0
        private decimal subTotalAmount, totalAmount;
        private RadioButton selectedRadioButton;

        //constants
        const decimal TaxRate = 0.13m;
        const decimal CappuccinoPrice = 2m;
        const decimal EspressoPrice = 2.25m;
        const decimal LattePrice = 1.75m;
        const decimal IcedPrice = 2.50m;

        public frmCoffeeShop()
        {
            InitializeComponent();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            //assigning the coffee selected radio button to the selectedRadioButton
            selectedRadioButton = (RadioButton) sender;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            //clear the current coffee selection for the same customer
            ClearAllInput();

        }

        private void ClearAllInput()
        {
            txtQuantity.Clear();
            txtItemAmount.Clear();
            if (selectedRadioButton != null)
            {
                selectedRadioButton.Checked = false;
                selectedRadioButton = null;
            }
            txtQuantity.Focus();
        }

        private void btnNewOrder_Click(object sender, EventArgs e)
        {
            DialogResult confirm;

            confirm = MessageBox.Show("Do you want to start a new order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            // if the user confirms the new order
            if (confirm == DialogResult.Yes)
            {
                btnClear.Enabled = false;
                btnNewOrder.Enabled = false;
                // clear new order
                ClearAllInput();

                txtSubtotal.Clear();
                txtTax.Clear();
                txtTotalDue.Clear();

                if (chkTakeout.Checked)
                    chkTakeout.Checked = false;
                chkTakeout.Enabled = true;
                subTotalAmount = 0;
                totalAmount = 0;

                txtQuantity.Focus();
            }// user confirms
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // terminate the application
            Application.Exit();
        }

        private void frmCoffeeShop_Load(object sender, EventArgs e)
        {
            //when form loads
            btnClear.Enabled = false;
            btnNewOrder.Enabled = false;
        }

        private void grpCoffee_Enter(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            // calculates the amount for the selected coffee/quantity
            // accumulates subtotal for an order-a customer can order
            // multiple coffee types in various quantities

            // local variables - do not have a default initial value
            int quantity = 0;
            decimal price = 0, tax, itemAmount;

            //enable the disabled btns and disable the check out
            btnClear.Enabled = true;
            btnNewOrder.Enabled = true;
            chkTakeout.Enabled = false;

            //quantity provided
            if (txtQuantity.Text != String.Empty)
            {
                try {
                    //convert text to integer
                    quantity = int.Parse(txtQuantity.Text); //could use Convert.toInt32(...)
                                                            // bool result = int.TryParse(txtQuantity.Text, out quantity);

                    if (quantity > 0)
                    {
                        //coffee type is selected
                        if (selectedRadioButton != null)
                        {
                            //calculate the item ammount due

                            switch (selectedRadioButton.Name)
                            {
                                case "rdoCappuccino":
                                    price = CappuccinoPrice;
                                    break;
                                case "rdoEspresso":
                                    price = EspressoPrice;
                                    break;
                                case "rdoLatte":
                                    price = LattePrice;
                                    break;
                                case "rdoIcedCappuccino":
                                    price = IcedPrice;
                                    break;
                                case "rdoIcedLatte":
                                    price = IcedPrice;
                                    break;
                                    // default:
                                    //      price = 0;
                                    //      break;
                            }

                            //calculations
                            itemAmount = price * quantity;
                            subTotalAmount += itemAmount; //subTotalAmount = subTotalAmount + itemAmount
                            if (chkTakeout.Checked)
                            {
                                tax = TaxRate * subTotalAmount;
                            }
                            else // avoid it by initialising local tax = 0
                            {
                                tax = 0;
                            }
                            totalAmount = subTotalAmount + tax;

                            // display values
                            txtItemAmount.Text = itemAmount.ToString("c");
                            txtSubtotal.Text = subTotalAmount.ToString("c");
                            txtTax.Text = tax.ToString("f2");
                            txtTotalDue.Text = totalAmount.ToString("c");

                        }//coffee selected
                        else
                        {
                            MessageBox.Show("The coffee type needs to be selected", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }//quantity > 0
                    else
                    {
                        MessageBox.Show("The number of coffees to be ordered should be greater than 0", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtQuantity.SelectAll();
                        //txtQuantity.Clear();
                        txtQuantity.Focus();
                    }
                } //try
                catch (FormatException quantityFE)
                {
                    // if (txtQuantity.Text == String.Empty)
                    // show message here
                    // else
                    MessageBox.Show("Quantity of coffees needed MUST be a whole number", "Quantity Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.SelectAll(); // txtQuantity.Clear();
                    txtQuantity.Focus();
                }
                catch(OverflowException quantityOE)
                {
                    MessageBox.Show("Quantity of coffees is out of range", "Quantity Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.SelectAll();
                    txtQuantity.Focus();
                }
                catch(Exception quantityException)
                {
                    MessageBox.Show(quantityException.Message, "Quantity Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtQuantity.SelectAll();
                    txtQuantity.Focus();
                }

                }//quantity provided
                
            else
            {
                MessageBox.Show("Please provide the number of coffee to be ordered", "Input Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
            }

        }//calculate click
    }
}

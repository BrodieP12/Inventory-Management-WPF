using DataDomain;
using LogicLayer;
using PresentationLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for AddEditVendor.xaml
    /// </summary>
    public partial class AddEditVendor : Window
    {
        
        UserManager userManager = new UserManager();
        VendorManager vendorManager = new VendorManager();
        ConfirmationDialog confirmationDialog = new ConfirmationDialog();
        public bool _isNew;
        public Vendor _selectedVendor;

        public AddEditVendor(Vendor selectedVendor, bool isNew)
        {
            InitializeComponent();
            _isNew = isNew;
            cbxUsers.ItemsSource = userManager.GetAllUserFirstAndLastName(userManager.GetAllUsers());
            if (isNew == false)
            {
                _selectedVendor = selectedVendor;
                setFields();
            } 
            else
            {
                _selectedVendor = new Vendor();
                
            }
                
        }
        private void setFields()
        {
            tbxVendorName.Text = _selectedVendor.VendorName;
            tbxContactPerson.Text = _selectedVendor.ContactPerson;
            mtbPhoneNumber.Text = _selectedVendor.PhoneNumber;
            tbxContactEmail.Text = _selectedVendor.Email;
            intUpDownLeadTime.Value = _selectedVendor.LeadTimeDays;
            cbxUsers.SelectedIndex = cbxUsers.Items.IndexOf(userManager.GetUserFirstNameLastName(_selectedVendor.AccountOwner));
        }
        private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog confirmationDialog = new ConfirmationDialog();
            bool userClickedYes = confirmationDialog.ShowConfirmationDialog("Are you sure you want to cancel?", "Confirmation");
            if (userClickedYes)
            {
                this.Close();
            }
        }
        private User getUserObjectByFullNameString(string fullName)
        {
            string firstName = fullName.Split(' ')[0];
            string lastName = fullName.Split(' ')[1];
            User user = userManager.GetUserByFirstAndLast(firstName, lastName);
            return user;
        }

        private void btnEditUser_Click(object sender, RoutedEventArgs e)
        {
            AddEditUser editUser = new AddEditUser((UserVM)getUserObjectByFullNameString(cbxUsers.SelectedItem.ToString()), false);
            editUser.Show();
        }
        private Vendor getVendorFields()
        {
            int leadTimeDays;
            if (intUpDownLeadTime.Value == null)
            {
                leadTimeDays = 7;
            }
            else 
            {
                leadTimeDays = (int)intUpDownLeadTime.Value;
            }
            Vendor _vendor = new Vendor
            {
                VendorID = _selectedVendor.VendorID,
                VendorName = tbxVendorName.Text,
                AccountOwner = getUserObjectByFullNameString(cbxUsers.SelectedItem.ToString()).UserID,
                ContactPerson = tbxContactPerson.Text,
                PhoneNumber = mtbPhoneNumber.Text.Replace("(", "").Replace(")", "").Replace("+", "").Replace("-", ""),
                Email = tbxContactEmail.Text,
                LeadTimeDays = leadTimeDays

            };
            return _vendor;
        }
        private bool validateInputs()
        {
            if (tbxVendorName.Text == "")
            {
                ModernWpf.MessageBox.Show("You must enter a name for the vendor.", "Missing Input!", MessageBoxButton.OK);
                tbxVendorName.Focus();
                return false;
            }
            if (cbxUsers.SelectedIndex == -1)
            {
                ModernWpf.MessageBox.Show("You must select an Account Owner for the vendor.", "Missing Input!", MessageBoxButton.OK);
                cbxUsers.Focus();
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog confirmationDialog = new ConfirmationDialog();
            if (_isNew == false)
            {
                if (validateInputs())
                {
                    Vendor updatedVendor = getVendorFields();
                    vendorManager.UpdateVendor(_selectedVendor.VendorID, updatedVendor);
                    ModernWpf.MessageBox.Show("Vendor Updated!", "Vendor Updated.", MessageBoxButton.OK);
                }
            }
            else
            {
                if (validateInputs())
                {
                    Vendor newVendor = getVendorFields();
                    vendorManager.AddVendor(newVendor);
                    ModernWpf.MessageBox.Show("Vendor Added!", "New Vendor Added.", MessageBoxButton.OK);
                }
            }
            this.Close();
        }
    }
}

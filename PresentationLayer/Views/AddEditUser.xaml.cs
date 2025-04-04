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
    /// Interaction logic for AddEditUser.xaml
    /// </summary>
    public partial class AddEditUser : Window
    {
        public UserManager userManager = new UserManager();
        public bool _isNew;
        public UserVM _selectedUser;
        public AddEditUser(UserVM userSelected, bool isNew)
        {
            InitializeComponent();
            _isNew = isNew;
            setRoleSelection();
            if(isNew == false)
            {
                _selectedUser = userSelected;
                setFields();
            } 
            else
            {
                _selectedUser = new UserVM();
            }
        }
        private void setRoleSelection()
        {
            List<string> roleNames = new List<string>();
            List<Role> roles = new List<Role>();
            roles = userManager.GetAllRoles();
            foreach (Role role in roles)
            {
                roleNames.Add(role.RoleName);
            }
            cbxRoles.ItemsSource = roleNames;
        }
        private void setFields()
        {
            tbxFirstName.Text = _selectedUser.GivenName;
            tbxLastName.Text = _selectedUser.FamilyName;
            mtbPhoneNumber.Text = _selectedUser.PhoneNumber;
            tbxEmail.Text = _selectedUser.Email;
            cbxRoles.SelectedIndex = cbxRoles.Items.IndexOf(userManager.GetRolesForUser(_selectedUser.UserID)[0]);
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
        private UserVM getUserFields()
        {
            List<string> roles = new List<string>();
            roles.Add(cbxRoles.SelectedValue.ToString());
            UserVM _user = new UserVM
            {
                UserID = _selectedUser.UserID,
                GivenName = tbxFirstName.Text,
                FamilyName = tbxLastName.Text,
                Email = tbxEmail.Text,
                PhoneNumber = mtbPhoneNumber.Text.Replace("(","").Replace(")","").Replace("+","").Replace("-",""),
                Roles = roles
            };
            return _user;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog confirmationDialog = new ConfirmationDialog();
            if (_isNew == false)
            {
                if (validateInputs())
                {
                    UserVM updatedUser = getUserFields();
                    userManager.UpdateUserByUserID(_selectedUser.UserID, updatedUser);
                    ModernWpf.MessageBox.Show("User Updated!", "User Updated.", MessageBoxButton.OK);
                }
            }
            else
            {
                if (validateInputs())
                {
                    UserVM newUser = getUserFields();
                    userManager.AddUser(newUser);
                    ModernWpf.MessageBox.Show("User Added!", "New User Added.", MessageBoxButton.OK);
                }
            }
            this.Close();
        }
        private bool validateInputs()
        {
            if (tbxFirstName.Text == "")
            {
                ModernWpf.MessageBox.Show("You must enter the user's first name.", "Missing Input!", MessageBoxButton.OK);
                tbxFirstName.Focus();
                return false;
            }
            if (tbxLastName.Text == "")
            {
                ModernWpf.MessageBox.Show("You must enter the user's last name.", "Missing Input!", MessageBoxButton.OK);
                tbxLastName.Focus();
                return false;
            }
            if(mtbPhoneNumber.Text == "")
            {
                ModernWpf.MessageBox.Show("You must enter the user's phone number.", "Missing Input!", MessageBoxButton.OK);
                mtbPhoneNumber.Focus();
                return false;
            }
            if (tbxEmail.Text == "")
            {
                ModernWpf.MessageBox.Show("You must enter the user's email address.", "Missing Input!", MessageBoxButton.OK);
                tbxEmail.Focus();
                return false;
            }
            if (cbxRoles.SelectedIndex == -1)
            {
                ModernWpf.MessageBox.Show("You must select the a role for the user.", "Missing Input!", MessageBoxButton.OK);
                cbxRoles.Focus();
                return false;
            }
            return true;
        }
    }
}

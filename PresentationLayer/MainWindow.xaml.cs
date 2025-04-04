using DataDomain;
using LogicLayer;
using System.Windows;
using System.Windows.Controls;
using ModernWpf.Controls;
using ModernWpf;
using PresentationLayer.Views;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<Audit> AuditDisplay { get; set; }
        
        List<PurchaseOrder> PurchaseDisplay { get; set; }
        List<Alert> NotificationDisplay { get; set; }
        
        public UserVM _accessToken = null;
        public bool loggedIn = false;
        
        
        public Vendor selectedVendor;
        
        public MainWindow()
        {
            InitializeComponent();

        }
        private void mnuPreferences_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new UserSettings();
            bool? result = settingsWindow.ShowDialog();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtEmail.Focus();
            btnLoginLogout.IsDefault = true;
            hideAllTabs();

        }
        private void mnuExit1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLoginLogout_Click(object sender, RoutedEventArgs e)
        {
            var userManager = new UserManager();
            string email = txtEmail.Text;
            string password = pwdPassword.Password;
            User user = null;

            if (btnLoginLogout.Content.ToString() == "Log Out")
            {
                logoutUser();
                return;
            }
            if (email.Length < 7)
            {
                System.Windows.MessageBox.Show("Invalid");
                txtEmail.Focus();
                txtEmail.SelectAll();
                return;
            }
            if (password.Length < 7)
            {
                System.Windows.MessageBox.Show("Invalid Password");
                pwdPassword.Focus();
                pwdPassword.SelectAll();
                return;
            }
            // try to log in
            try
            {
                _accessToken = userManager.LoginUser(email, password);
                string message = "Welcome, " + _accessToken.GivenName;
                lblGreeting.Content = message;
                lblRoleDisplay.Content = "You are logged in as: " + userManager.GetRolesForUser(_accessToken.UserID)[0];
                lblRoleDisplay.Visibility = Visibility.Visible;

                // reset the login area and button
                // reset the user interface
                showUserTabs();
                statMessage.Content = "Logged in. Please log out before you leave.";

                // reset the log in
                btnLoginLogout.Content = "Log Out";
                txtEmail.Text = "";
                pwdPassword.Password = "";
                txtEmail.IsEnabled = false;
                pwdPassword.IsEnabled = false;
                txtEmail.Visibility = Visibility.Collapsed;
                pwdPassword.Visibility = Visibility.Collapsed;
                lblGreeting.Margin = new Thickness(0, 0, 0, 10);
                btnLoginLogout.IsDefault = false;

                if (password == "newuser")
                {
                    //var updatePassword = new frmUpdatePassword(_accessToken, userManager, isNew: true);
                    //if (updatePassword.ShowDialog() == false)
                    //{
                    //    logoutUser();
                    //}
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                System.Windows.MessageBox.Show(message, "Login Failed");
            }
        }

        private void logoutUser()
        {
            // remove the access token
            _accessToken = null;
            // reset the user interface
            statMessage.Content = "You are not logged in. Please log in to continue.";


            // reset the login
            btnLoginLogout.Content = "Log In";
            txtEmail.Text = "";
            pwdPassword.Password = "";
            txtEmail.IsEnabled = true;
            pwdPassword.IsEnabled = true;
            txtEmail.Visibility = Visibility.Visible;
            pwdPassword.Visibility = Visibility.Visible;
            lblGreeting.Margin = new Thickness(0, 0, 0, 5);
            //hideAllUserTabs();

            txtEmail.Focus();
            btnLoginLogout.IsDefault = true;
            lblGreeting.Content = "You are not logged in.";
            lblRoleDisplay.Visibility = Visibility.Collapsed;
            return;
        }

        private void showUserTabs()
        {
            /*
                Admin
                Checkin
                Checkout
                Maintenance
                Manager
                No Access
                Prep
                Rental
            */
            foreach (string r in _accessToken.Roles)
            {
                switch (r)
                {
                    case "Admin":
                        navHeaderInventory.Visibility = Visibility.Visible;
                        navItemOverview.Visibility = Visibility.Visible;
                        navItemOverview.IsSelected = true;
                        navItemAudits.Visibility = Visibility.Visible;
                        navItemAudits.IsSelected = true;
                        break;
                    case "User":
                        navHeaderInventory.Visibility = Visibility.Visible;
                        navItemOverview.Visibility = Visibility.Visible;
                        navItemOverview.IsSelected = true;
                        break;
                    default:
                        break;
                }
            }
        }

        private void hideAllTabs()
        {
            navHeaderInventory.Visibility = Visibility.Collapsed;
            navItemOverview.Visibility = Visibility.Collapsed;
            navItemAudits.Visibility = Visibility.Collapsed;
        }
        private void mnuUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            //var updateWindow = new frmUpdatePassword(_accessToken, new EmployeeManager());
            //bool? result = updateWindow.ShowDialog();
            /*
            if (result == true)
            {
                MessageBox.Show("Password Updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Password was not updated!", "Update Failed", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            */
        }
       

        private void btnSearchButton_Click(object sender, RoutedEventArgs e)
        {
            //var UpdateStockWindow = new UpdateStockLevel();
            //bool? result = UpdateStockWindow.ShowDialog();
        }



        
        
        
        private void dgVendors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //selectedVendor = dgVendors.SelectedItem as Vendor;
        }
        

        private void btnEditInvItem_Click(object sender, RoutedEventArgs e)
        {
            //if (selectedItem != null) {
            //    var itemEditWindow = new ItemEditor(selectedItem, inventoryManager, false);
            //    bool? result = itemEditWindow.ShowDialog();
            //}
            //else
           // {
           //     System.Windows.MessageBox.Show("You must make a selection.");
            //}
        }
        
        /*===================================
         *           Audits Tab
         ===================================*/
        
        private void dgAudits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //selectedAudit = dgAudits.SelectedItem as Audit;
        }

        private void btnDeleteAudit_Click(object sender, RoutedEventArgs e)
        {

        }

        

        private void navView_BackRequested(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewBackRequestedEventArgs args)
        {

        }

        private void navView_ItemInvoked(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewItemInvokedEventArgs args)
        {
            if (args.InvokedItemContainer is NavigationViewItem item)
            {
                string uri = NavigationProperties.GetNavigationUri(item);
                if (uri == "Views/InventoryView.xaml")
                {
                    //NavigationService.Navigate(inventoryView, _accessToken);
                    contentFrame.NavigationService.Navigate(new InventoryView(_accessToken));
                } else if(uri == "Views/AuditView.xaml")
                {
                    contentFrame.NavigationService.Navigate(new AuditView(_accessToken));
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No navigation URI was found on this item.");
                }
            }
        }

        private void navView_PaneOpening(ModernWpf.Controls.NavigationView sender, object args)
        {

        }

        private void navView_PaneClosing(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewPaneClosingEventArgs args)
        {

        }

        private void navView_DisplayModeChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewDisplayModeChangedEventArgs args)
        {

        }

        private void btnToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            if (ThemeManager.Current.ApplicationTheme == ApplicationTheme.Dark)
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
            }
            else
            {
                ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            }
        }
    }
}
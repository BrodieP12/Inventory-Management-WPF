using Azure.Core;
using DataDomain;
using LogicLayer;
using PresentationLayer.Helpers;
using PresentationLayer.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : System.Windows.Controls.Page
    {
        List<InventoryItem> InventoryItemDisplay { get; set; }
        public InventoryManager inventoryManager = new InventoryManager();
        public InventoryItem selectedItem;
        //public ContentDialogs contentDialogs = new ContentDialogs();
        public List<InventoryItem> Items { get; set; }
        public UserVM AccessToken { get; set; }

        public InventoryView(UserVM userVM)
        {
            AccessToken = userVM;
            InitializeComponent();
            DataContext = this;
            if (AccessToken.Roles.Contains("User"))
            {
                setUserControls();
            } else if (AccessToken.Roles.Contains("Admin"))
            {
                setAdminControls();
            }
        }
        private void setUserControls()
        {
            btnAddInvItem.Visibility = Visibility.Collapsed;
            btnEditInvItem.Visibility = Visibility.Collapsed;
            btnDeleteInvItem.Visibility = Visibility.Collapsed;
        }
        private void setAdminControls()
        {
            btnAddInvItem.Visibility = Visibility.Visible;
            btnEditInvItem.Visibility = Visibility.Visible;
            btnDeleteInvItem.Visibility = Visibility.Visible;
        }

        private void cbxSearchCategory_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var column in dgInventory.Columns)
            {
                cbxSearchCategory.Items.Add(column.Header.ToString());
            }
        }
        private void dgInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedItem = dgInventory.SelectedItem as InventoryItem;
        }
        private void InventoryColumnHeader_RightClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGridColumnHeader header)
            {
                // Create the Popup
                Popup popup = new Popup
                {
                    StaysOpen = false, // Allow manual control of closing
                    PlacementTarget = header, // Anchor it to the header
                    Placement = PlacementMode.Bottom, // Show below the header
                };

                // Create a ContextMenu-like UI
                StackPanel panel = new StackPanel
                {
                    Background = System.Windows.Media.Brushes.Black
                };

                foreach (var column in dgInventory.Columns)
                {
                    var checkBox = new System.Windows.Controls.CheckBox
                    {
                        Content = column.Header.ToString(),
                        IsChecked = column.Visibility == Visibility.Visible,
                        Tag = column
                    };

                    checkBox.Checked += (s, ev) =>
                    {
                        var gridColumn = (DataGridColumn)((System.Windows.Controls.CheckBox)s).Tag;
                        gridColumn.Visibility = Visibility.Visible;

                        if (!cbxSearchCategory.Items.Contains(column.Header.ToString()))
                        {
                            cbxSearchCategory.Items.Add(column.Header.ToString());
                        }
                    };
                    checkBox.Unchecked += (s, ev) =>
                    {
                        var gridColumn = (DataGridColumn)((System.Windows.Controls.CheckBox)s).Tag;
                        gridColumn.Visibility = Visibility.Collapsed;

                        if (cbxSearchCategory.Items.Contains(column.Header.ToString()))
                        {
                            cbxSearchCategory.Items.Remove(column.Header.ToString());
                        }
                    };
                    panel.Children.Add(checkBox);

                }

                // Add the panel to the Popup
                popup.Child = panel;

                // Show the Popup
                popup.IsOpen = true;

                // Close the popup when clicking outside
                System.Windows.Application.Current.MainWindow.PreviewMouseDown += ClosePopupOnOutsideClick;

                void ClosePopupOnOutsideClick(object sender, MouseButtonEventArgs args)
                {
                    if (!popup.IsMouseOver)
                    {
                        popup.IsOpen = false;
                        System.Windows.Application.Current.MainWindow.PreviewMouseDown -= ClosePopupOnOutsideClick;
                    }
                }

                e.Handled = true;
            }
        }
        private void txtSearch_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                // dgInventory.ItemsSource = In
                string searchString = txtSearch.Text;
                dgInventory.ItemsSource = Items.Where(i => i.ItemName.ToLower().Contains(searchString.ToLower())).Select(c => c);
            }
            else
            {
                dgInventory.ItemsSource = Items;
            }
        }
        private void reloadInventoryGrid()
        {
            try
            {
                Items = inventoryManager.GetInventoryItems();

                dgInventory.ItemsSource = Items;

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                System.Windows.MessageBox.Show(message, "Item Retrieval Failed");
            }
        }

        private void dgInventory_Loaded(object sender, RoutedEventArgs e)
        {
            reloadInventoryGrid();
        }
       
        

        

        private async void btnEditInvItem_Click(object sender, RoutedEventArgs e)
        {
            if (selectedItem == null)
            {
                System.Windows.MessageBox.Show("You must select an item to edit it.");
                return;
            }
            
            AddEditItem editItem = new AddEditItem(AccessToken, selectedItem, false);
            bool? result = editItem.ShowDialog();
            reloadInventoryGrid();
            //contentDialogs.AddEditItem(selectedItem, false);
        }

        private void btnAddInvItem_Click(object sender, RoutedEventArgs e)
        {
            AddEditItem addItem = new AddEditItem(AccessToken, selectedItem, true);
            bool? result = addItem.ShowDialog();
            reloadInventoryGrid();
        }

        private void btnDeleteInvItem_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog confirmationDialog = new ConfirmationDialog();
            if(confirmationDialog.ShowConfirmationDialog("Are you sure you want to delete " + selectedItem.ItemName + "?", "Confirmation"))
            {
                if (!inventoryManager.RemoveInventoryItem(selectedItem.ItemID))
                {
                    confirmationDialog.ShowConfirmationDialog(selectedItem.ItemName + " deleted!", "Confirmation");
                    reloadInventoryGrid();
                }
                else
                {
                    confirmationDialog.ShowConfirmationDialog(selectedItem.ItemName + " was unable to be deleted!", "Confirmation");
                    reloadInventoryGrid();
                }
            }
            
        }
    }
}

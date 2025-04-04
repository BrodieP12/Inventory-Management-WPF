using Azure.Core;
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
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;

namespace PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddEditItem : Window
    {
        public VendorManager vendorManager = new VendorManager();
        public InventoryManager inventoryManager = new InventoryManager();
        public UserManager userManager = new UserManager();
        public InventoryItem item = new InventoryItem
        {
            ItemID = -1,
            ItemName = "",
            QuantityOnHand = 0,
            Description = "",
            UnitPrice = 0,
            SerialNumber = "",
            WarrentyPeriodMonths = 0,
            ReorderLevel = 0,
            ItemWeight = 0,
            Active = true,
            VendorID = -1,
            Vendor = new Vendor(),
            Location = new ItemLocation
            {
                LocationName = "",
                LocationGUID = "" + Guid.NewGuid()
            }
        };
        public InventoryItem _selectedItem;
        public int vendorSelectedIndex = -1;
        public int comboBoxSelected = -1;
        public string[] StatusOptions = {"In Stock", "Low Stock", "Out of Stock"};
        public List<string> VendorNames = new List<string>();
        public bool _isNew;
        public User accessToken;
        public AddEditItem(User accessToken, InventoryItem selectedItem, bool isNew)
        {
            InitializeComponent();
            _isNew = isNew;
            _selectedItem = selectedItem;
            setTitle(isNew);

            VendorNames = vendorManager.GetAllVendorNames();
            setUIActions();
            if (isNew == false)
            {
                setItemValues(selectedItem);
                item = selectedItem;
                setUIValues();
            } else
            {
                setNewUIValues();
                updateComboBoxStatus(Int32.Parse(intUpDownQuantityOnHand.Text), Int32.Parse(intUpDownReorderLevel.Text), cbxStatus);
            }
            

        }
        public void setTitle(bool isNew)
        {
            if (isNew == false)
            {
                this.Title = "Editing Item...";
            }
            else
            {
                this.Title = "Adding Item...";
            }
        }
        public void setItemValues(InventoryItem Item)
        {
            if (item.WarrentyPeriodMonths != null)
            {
                item.WarrentyPeriodMonths = (int)Item.WarrentyPeriodMonths;
            }
            if (item.ItemWeight != null)
            {
                item.ItemWeight = Item.ItemWeight;
            }
            if (Item.Status.Equals("In Stock"))
            {
                comboBoxSelected = 0;
            }
            else if (Item.Status.Equals("Low Stock"))
            {
                comboBoxSelected = 1;
            }
            else if (Item.Status.Equals("Out of Stock"))
            {
                comboBoxSelected = 2;
            }
            item.ItemWeight = Item.ReorderLevel;
            item.Active = Item.Active;

            item.VendorID = Item.VendorID;
            item.Vendor = Item.Vendor;
            item.Location = Item.Location;
            vendorSelectedIndex = Item.VendorID - 1;
        }
        public void setUIValues()
        {
            tbxItemName.Text = item.ItemName;
            intUpDownQuantityOnHand.Value = item.QuantityOnHand;
            intUpDownWarrentyMonths.Value = item.WarrentyPeriodMonths;
            intUpDownReorderLevel.Value = item.ReorderLevel;
            tbxSerialNumber.Text = item.SerialNumber;
            cbxStatus.ItemsSource = StatusOptions;
            cbxStatus.SelectedIndex = comboBoxSelected;
            tbxItemDescription.Text = item.Description;
            doubleUpDownUnitPrice.Value = (double)item.UnitPrice;
            tbxLocationName.Text = item.Location.LocationName;
            tbxLocationGUID.Text = item.Location.LocationGUID;
            cbxVendor.ItemsSource = vendorManager.GetAllVendorNames();
            cbxVendor.SelectedIndex = item.VendorID - 1;
        }
        public void setNewUIValues()
        {
            cbxStatus.ItemsSource = StatusOptions;
            comboBoxSelected = 0;
            cbxStatus.SelectedIndex = 0;
            intUpDownQuantityOnHand.Value = 0;
            intUpDownReorderLevel.Value = 0;
            cbxVendor.ItemsSource = vendorManager.GetAllVendorNames();
            tbxLocationGUID.Text = Guid.NewGuid().ToString();
        }
        public void setUIActions()
        {
            intUpDownQuantityOnHand.PreviewTextInput += IntegerUpDown_PreviewTextInput;
            intUpDownReorderLevel.PreviewTextInput += IntegerUpDown_PreviewTextInput;
            intUpDownWarrentyMonths.PreviewTextInput += IntegerUpDown_PreviewTextInput;
            intUpDownQuantityOnHand.ValueChanged += (sender, e) => integerUpDownQuantity_TextInput(sender, e, intUpDownReorderLevel, cbxStatus);
            intUpDownReorderLevel.ValueChanged += (sender, e) => integerUpDownReorderLevel_TextInput(sender, e, intUpDownQuantityOnHand, cbxStatus);
            doubleUpDownUnitPrice.PreviewTextInput += (sender, e) => DoubleUpDown_PreviewTextInput(sender, e, doubleUpDownUnitPrice);
            doubleUpDownItemWeight.PreviewTextInput += (sender, e) => DoubleUpDown_PreviewTextInput(sender, e, doubleUpDownItemWeight);
        }
        private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        private void updateComboBoxStatus(int q, int rl, System.Windows.Controls.ComboBox cbx)
        {
            //System.Windows.Forms.MessageBox.Show($"Quantity: {q} \n Reorder Level: {rl}");
            if (q == 0)
            {
                cbx.SelectedIndex = 2;
            }
            else if (q < rl || q == rl)
            {
                cbx.SelectedIndex = 1;
            }
            else if (q > rl)
            {
                cbx.SelectedIndex = 0;
            }
        }
        private void DoubleUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e, DoubleUpDown doubleUpDown)
        {
            // Flag to track if a decimal point has already been entered
            bool decimalPointEntered = doubleUpDown.Text.Contains(".");

            if (!char.IsDigit(e.Text, 0))  // Check if the first character is not a digit
            {
                if (e.Text == "." && !decimalPointEntered)  // Allow only one decimal point
                {
                    e.Handled = false;  // Let the decimal point through
                }
                else
                {
                    e.Handled = true;  // Block non-digit characters (except the decimal point)
                }
            }
            else if (e.Text == "." && decimalPointEntered)  // Block multiple decimal points
            {
                e.Handled = true;
            }

        }
        private void integerUpDownReorderLevel_TextInput(object sender, RoutedPropertyChangedEventArgs<object> e, IntegerUpDown integerUpDownQuantity, System.Windows.Controls.ComboBox comboBox)
        {
            var reorderLevelInput = sender as IntegerUpDown;
            if (Int32.TryParse(reorderLevelInput.Text, out int reorderLevel))
            {
                updateComboBoxStatus(Int32.Parse(integerUpDownQuantity.Text), reorderLevel, comboBox);
            }
        }
        private void integerUpDownQuantity_TextInput(object sender, RoutedPropertyChangedEventArgs<object> e, IntegerUpDown integerUpDownReorderLevel, System.Windows.Controls.ComboBox comboBox)
        {
            var quantityInput = sender as IntegerUpDown;
            if (Int32.TryParse(quantityInput.Text, out int quantity))
            {
                updateComboBoxStatus(quantity, Int32.Parse(integerUpDownReorderLevel.Text), comboBox);
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

        private void btnAddVendor_Click(object sender, RoutedEventArgs e)
        {
            AddEditVendor addVendor = new AddEditVendor(new Vendor(), true);
            bool? result = addVendor.ShowDialog();
            cbxVendor.ItemsSource = vendorManager.GetAllVendorNames();
        }
        
        private void btnEditVendor_Click(object sender, RoutedEventArgs e)
        {
            AddEditVendor editVendor = new AddEditVendor(vendorManager.GetVendorByName(cbxVendor.SelectedItem.ToString()), false);
            bool? result = editVendor.ShowDialog();
            cbxVendor.ItemsSource = vendorManager.GetAllVendorNames();
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ConfirmationDialog confirmationDialog = new ConfirmationDialog();
            if (_isNew == false)
            {
                InventoryItem updatedItem = getItemFields();
                inventoryManager.UpdateInventoryItem(_selectedItem.ItemID, updatedItem);
                if (
                    tbxLocationName.Text != inventoryManager.GetLocationByItemID(item.ItemID).LocationName ||
                    tbxLocationGUID.Text != inventoryManager.GetLocationByItemID(item.ItemID).LocationGUID
                    )
                {
                    ItemLocation location = new ItemLocation();
                    location.LocationName = tbxLocationName.Text;
                    location.LocationGUID = tbxLocationGUID.Text;
                    inventoryManager.UpdateInventoryLocation(item.ItemID, location);
                }
            } 
            else
            {
                if (validateInputs())
                {
                    InventoryItem newItem = getItemFields();
                    ItemLocation newLocation = getLocationFields();
                    inventoryManager.AddInventoryItem(newItem, newLocation);
                    ModernWpf.MessageBox.Show("Item Added!", "New Item Added.", MessageBoxButton.OK);
                }
            }
            this.Close();
        }
        private bool validateInputs()
        {
            if(tbxItemName.Text == "")
            {
                System.Windows.MessageBox.Show("You must enter a name for the item.", "Missing Input!", MessageBoxButton.OK);
                tbxItemName.Focus();
                return false;
            }
            if(cbxVendor.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("You must select a vendor for the item.", "Missing Input!", MessageBoxButton.OK);
                cbxVendor.Focus();
                return false;
            }
            if(intUpDownQuantityOnHand.Value == null)
            {
                System.Windows.MessageBox.Show("You must enter a quantity for the item.", "Missing Input!", MessageBoxButton.OK);
                intUpDownQuantityOnHand.Focus();
                return false;
            }
            if(doubleUpDownUnitPrice.Value == null)
            {
                System.Windows.MessageBox.Show("You must enter a price for the item.", "Missing Input!", MessageBoxButton.OK);
                intUpDownQuantityOnHand.Focus();
                return false;
            }
            if (intUpDownReorderLevel.Value == null)
            {
                System.Windows.MessageBox.Show("You must enter a price for the item.", "Missing Input!", MessageBoxButton.OK);
                intUpDownQuantityOnHand.Focus();
                return false;
            }
            if(cbxStatus.SelectedIndex == -1)
            {
                System.Windows.MessageBox.Show("You must select a stock status for the item.", "Missing Input!", MessageBoxButton.OK);
                intUpDownQuantityOnHand.Focus();
                return false;
            }
            return true;
        }
        private InventoryItem getItemFields()
        {
            InventoryItem inventoryItem = new InventoryItem
            {
                ItemID = -1,
                ItemName = tbxItemName.Text,
                VendorID = vendorManager.GetVendorByName(cbxVendor.SelectedItem.ToString()).VendorID,
                QuantityOnHand = (int)intUpDownQuantityOnHand.Value,
                Description = tbxItemDescription.Text,
                UnitPrice = (decimal)doubleUpDownUnitPrice.Value,
                ReorderLevel = (int)intUpDownReorderLevel.Value,
                WarrentyPeriodMonths = intUpDownWarrentyMonths.Value,
                SerialNumber = tbxSerialNumber.Text,
                Status = cbxStatus.Text,
                ItemWeight = (decimal)doubleUpDownItemWeight.Value,
                DateAdded = DateTime.Now,
                LastUpdated = DateTime.Now,
                Active = (bool)ckbItemActive.IsChecked
            };
            return inventoryItem;
        }
        private ItemLocation getLocationFields()
        {
            ItemLocation location = new ItemLocation
            {
                LocationGUID = tbxLocationGUID.Text,
                LocationName = tbxLocationName.Text,
            };
            return location;
        }
    }
}

using DataDomain;
using LogicLayer;
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
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using System.ComponentModel.DataAnnotations;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for ItemEditor.xaml
    /// </summary>
    public partial class ItemEditor : Window
    {
        public InventoryItem _item;
        private IInventoryManager _itemManager;
        private readonly bool _isNew;

        
        public ItemEditor(InventoryItem item,
            InventoryManager itemManager, bool isNew = false)
        {
            InitializeComponent();
            _item = item;
            _itemManager = (IInventoryManager?)itemManager;
            _isNew = isNew;

            ShowItemProperties();

            //var combinedProperties = new CombinedProperties(_item);
            //propertyGrid.SelectedObject = 


            var itemProperties = new ItemProperties(_item); // Binding the data to the existing item
            propertyGrid.SelectedObject = itemProperties;
        }
        private void ShowItemProperties() {
            var itemProperties = new ItemProperties(_item);
            propertyGrid.SelectedObject = itemProperties;
        }
        private void ShowVendorProperties()
        {
            var vendorProperties = new VendorProperties(_item);
            propertyGrid.SelectedObject = vendorProperties;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (propertyGrid.SelectedObject is ItemProperties itemProps)
            {
                _item.ItemName = itemProps.Name;
                _item.QuantityOnHand = itemProps.TotalStock;
                _item.Description = itemProps.Description;
                _item.UnitPrice = (decimal)itemProps.UnitPrice;
                _item.ReorderLevel = itemProps.ReorderLevel;
                _item.WarrentyPeriodMonths = itemProps.WarrentyPeriodMonths;
                _item.SerialNumber = itemProps.SerialNumber;
                _item.ItemWeight = (decimal)itemProps.ItemWeight;
                _item.Status = itemProps.Status;
                _item.Active = itemProps.Active;

            }
            else if (propertyGrid.SelectedObject is VendorProperties vendorProps)
            {
                _item.Vendor.VendorName = vendorProps.VendorName;
                _item.Vendor.AccountOwner = vendorProps.AccountOwnerID;
                _item.Vendor.ContactPerson = vendorProps.ContactPerson;
                _item.Vendor.PhoneNumber = vendorProps.PhoneNumber;
                _item.Vendor.Email = vendorProps.Email;
                _item.Vendor.LeadTimeDays = vendorProps.LeadTimeDays;
            }
        }
        public class VendorProperties : INotifyPropertyChanged
        {
            private string _vendorName;
            private int _accountOwnerID;
            private string _contactPerson;
            private string _phoneNumber;
            private string _email;
            private int _leadTimeDays;
            public VendorProperties(InventoryItem item)
            {
                VendorName = item.Vendor.VendorName;
                AccountOwnerID = item.Vendor.AccountOwner;
                ContactPerson = item.Vendor.PhoneNumber;
                Email = item.Vendor.Email;
                LeadTimeDays = item.Vendor.LeadTimeDays;
            }


            [Category("Vendor Details")]
            [DisplayName("Vendor Name")]
            [Description("Name of the vendor.")]
            [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
            public string VendorName
            {
                get => _vendorName;
                set { _vendorName = value; OnPropertyChanged(nameof(VendorName)); }
            }
            [Category("Vendor Details")]
            [DisplayName("Account Owner")]
            [Description("Person who owns the account of the vendor.")]
            [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
            [ItemsSource(typeof(AccountOwnerrItemsSource))]
            public int AccountOwnerID
            {
                get => _accountOwnerID;
                set
                {
                    _accountOwnerID = value;
                    OnPropertyChanged(nameof(AccountOwnerID));
                }
            }
            public List<string> AccountOwnerNameList { get; }
            

            [Category("Vendor Details")]
            [DisplayName("Contact Person")]
            [Description("Vendor Contact Person.")]
            [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
            public string ContactPerson
            {
                get => _contactPerson;
                set { _contactPerson = value; OnPropertyChanged(nameof(ContactPerson));  }
            }
            [Category("Vendor Details")]
            [DisplayName("Contact Number")]
            [Description("Contact Person Phone Number.")]
            [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
            public string PhoneNumber
            {
                get => _phoneNumber;
                set { _phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
            }

            [Category("Vendor Details")]
            [DisplayName("Email Address")]
            [Description("Vendor's email address.")]
            [Editor(typeof(TextBoxEditor), typeof(TextBoxEditor))]
            public string Email
            {
                get => _email;
                set {  _email = value; OnPropertyChanged(nameof(Email));}
            }

            [Category("Vendor Details")]
            [DisplayName("Lead Time Days")]
            [Description("Vendor's days to order ahead")]
            [Editor(typeof(IntegerUpDownEditor), typeof(IntegerUpDownEditor))]
            public int LeadTimeDays
            {
                get => _leadTimeDays;
                set { _leadTimeDays = value; OnPropertyChanged(nameof(LeadTimeDays)); }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
           
            
        }
        public class AccountOwnerrItemsSource : IItemsSource
        {
            public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
            {
                UserManager userManager = new UserManager();
                List<User> users = userManager.GetAllUsers();
                Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection itemCollection = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();

                foreach(var user in users)
                {
                    itemCollection.Add(new KeyValuePair<int, string>(user.UserID, userManager.GetUserFirstNameLastName(user.UserID)));
                }
                return itemCollection;
            }
        }
        public class ItemProperties : INotifyPropertyChanged
        {
            private string stockStatus;
            public ItemProperties(InventoryItem item)
            {
                Name = item.ItemName;
                Description = item.Description;
                UnitPrice = (double)item.UnitPrice;
                TotalStock = item.QuantityOnHand;
                ReorderLevel = item.ReorderLevel;
                WarrentyPeriodMonths = item.WarrentyPeriodMonths;
                SerialNumber = item.SerialNumber;
                Status = item.Status;

                Active = item.Active;
            }

            [Category("Item Details")]
            [DisplayName("Item Weight")]
            [Description("The weight per individual item.")]
            [Editor(typeof(DoubleUpDownEditor), typeof(DoubleUpDownEditor))]
            public double ItemWeight { get; set; }


            [Category("Item Details")]
            [DisplayName("Serial Number")]
            [Description("The serial number of item.")]
            [Editor(typeof(TextBoxEditor), typeof(IntegerUpDownEditor))]
            public string SerialNumber { get; set; }

            [Category("Item Details")]
            [DisplayName("Warrenty Period (Months)")]
            [Description("Number of months that the item is under warrenty.")]
            [Editor(typeof(IntegerUpDownEditor), typeof(IntegerUpDownEditor))]
            public int? WarrentyPeriodMonths { get; set; }


            [Category("Item Details")]
            [DisplayName("Reorder Level")]
            [Description("Total Number of items before a notifcation for new items appear.")]
            [Editor(typeof(IntegerUpDownEditor), typeof(IntegerUpDownEditor))]
            public int ReorderLevel {  get; set; }

            [Category("Stock Info")]
            [DisplayName("Stock Level")]
            [Description("Total number of items on hand.")]
            [Editor(typeof(IntegerUpDownEditor), typeof(IntegerUpDownEditor))]
            public int TotalStock { get; set; }

            [Category("Stock Info")]
            [DisplayName("Status")]
            [Description("Stock status of the item.")]
            [ItemsSource(typeof(StockStatusItemsSource))]
            public string Status
            {
                get => stockStatus;
                set
                {
                    stockStatus = value;
                    OnPropertyChanged(nameof(stockStatus));
                }
            }

            [Category("Item Details")]
            [DisplayName("Is Available")]
            [Description("Indicates if the item is available.")]
            [Editor(typeof(CheckBoxEditor), typeof(CheckBoxEditor))]
            public bool Active { get; set; }
            
            [Category("Item Details")]
            [DisplayName("Unit Price")]
            [Description("The price per item")]
            [Editor(typeof(DoubleUpDownEditor), typeof(DoubleUpDownEditor))]
            public double UnitPrice { get; set; }

            [Category("Item Details")]
            [DisplayName("Item Description")]
            [Description("A short description about the item")]
            public string Description { get; set; }

            [Category("Item Details")]
            [DisplayName("Item Name")]
            [Description("The display name of the item")]
            public string Name { get; set; }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            public class StockStatusItemsSource : IItemsSource
            {
                public Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection GetValues()
                {
                    var itemCollection = new Xceed.Wpf.Toolkit.PropertyGrid.Attributes.ItemCollection();

                    itemCollection.Add(new String("In Stock"));
                    itemCollection.Add(new String("Out of Stock"));
                    itemCollection.Add(new String("Low Stock"));

                    return itemCollection;
                }
            }
        }
    }
}

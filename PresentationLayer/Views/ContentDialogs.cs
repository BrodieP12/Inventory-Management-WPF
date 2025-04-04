using DataDomain;
using ModernWpf.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using Xceed.Wpf.Toolkit;
using Color = System.Windows.Media.Color;
using System.Windows.Media.Animation;
using Xceed.Wpf.Toolkit.Primitives;
using LogicLayer;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace PresentationLayer.Views
{
    /*
    public class ContentDialogs
    {

        public async void AddEditItem(InventoryItem selectedItem, bool isNew)
        {








            /*===========================
             *       COMBO BOXES
             * =========================

            var comboBoxStatus = new System.Windows.Controls.ComboBox
            {
                Name = "cbxStatus",
                ItemsSource = new string[] { "In Stock", "Low Stock", "Out of Stock" },
                SelectedIndex = comboBoxSelected,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 10)
            };
            var comboBoxVendor = new System.Windows.Controls.ComboBox
            {
                Name = "cbxVendor",
                ItemsSource = vendorManager.GetAllVendorNames(),
                SelectedIndex = vendorSelectedIndex,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 10)
            };


            /*===========================
             *     INTEGER SPINNERS
             * =========================
            var integerUpDownQuantity = new IntegerUpDown
            {
                Name = "intUpDownQuantityOnHand",
                Minimum = 0,
                Value = item.QuantityOnHand,
                Margin = new Thickness(0, 0, 0, 10)
            };
            var integerUpDownReorderLevel = new IntegerUpDown
            {
                Name = "intUpDownReorderLevel",
                Minimum = 0,
                Value = item.ReorderLevel,
                Margin = new Thickness(0, 0, 0, 10)
            };

            


            var integerUpDownWarrentyMonths = new IntegerUpDown
            {
                Name = "intUpDownWarrentyMonths",
                Minimum = 0,
                Value = item.WarrentyPeriodMonths,
                Margin = new Thickness(0, 0, 0, 10)
            };


            /*=================================
             *        DOUBLE SPINNE
             * ================================
            var doubleUpDownUnitPrice = new DoubleUpDown
            {
                Name = "doubleUpDownUnitPrice",
                Minimum = 0.00,
                Value = item.UnitPrice,
                Increment = .01,
                Margin = new Thickness(0, 0, 0, 10),
                FormatString = "C2"
            };
            var doubleUpDownItemWeight = new DoubleUpDown
            {
                Name = "doubleUpDownItemWeight",
                Minimum = 0.00,
                Value = item.ItemWeight,
                Increment = .01,
                Margin = new Thickness(0, 0, 0, 10),
                FormatString = "F3"
            };


            // Event Handlers for Integer and Double Spinners
            

            /*===========================================
             *  STYLES FOR DOUBLE AND INTEGER SPINNERS
             * =========================================
            // Define style for IntegerUpDown
            var styleIntUpDown = new Style(typeof(IntegerUpDown));

            // Background color consistent with ModernWPF light/dark themes
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.BackgroundProperty, new SolidColorBrush(Color.FromRgb(43, 43, 43)))); // Dark background
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.ForegroundProperty, new SolidColorBrush(Color.FromRgb(230, 230, 230)))); // Light text
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.BorderBrushProperty, new SolidColorBrush(Color.FromRgb(77, 77, 77))));    // Subtle border

            // Corner radius for a softer, modern appearance
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.PaddingProperty, new Thickness(8)));
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.BorderThicknessProperty, new Thickness(1)));

            // Stretch control width
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.TextAlignmentProperty, TextAlignment.Left));

            // Font and content alignment
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.FontSizeProperty, 14.0));
            styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.FontFamilyProperty, new System.Windows.Media.FontFamily("Segoe UI")));
            integerUpDownQuantity.Style = styleIntUpDown; // Apply the style
            integerUpDownReorderLevel.Style = styleIntUpDown;
            integerUpDownWarrentyMonths.Style = styleIntUpDown;

            var styleDoubleUpDown = new Style(typeof(DoubleUpDown));

            // Background color consistent with ModernWPF light/dark themes
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.BackgroundProperty, new SolidColorBrush(Color.FromRgb(43, 43, 43)))); // Dark background
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.ForegroundProperty, new SolidColorBrush(Color.FromRgb(230, 230, 230)))); // Light text
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.BorderBrushProperty, new SolidColorBrush(Color.FromRgb(77, 77, 77))));    // Subtle border

            // Corner radius for a softer, modern appearance
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.PaddingProperty, new Thickness(8)));
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.BorderThicknessProperty, new Thickness(1)));

            // Stretch control width
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.TextAlignmentProperty, TextAlignment.Left));

            // Font and content alignment
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.FontSizeProperty, 14.0));
            styleDoubleUpDown.Setters.Add(new Setter(DoubleUpDown.FontFamilyProperty, new System.Windows.Media.FontFamily("Segoe UI")));
            doubleUpDownUnitPrice.Style = styleDoubleUpDown; // Apply the style
            doubleUpDownItemWeight.Style = styleDoubleUpDown;

            /*==================================
             *              BUTTONS
             * =================================
            var btnAddVendor = new System.Windows.Controls.Button
            {
                Name = "btnAddVendor",
                Content = "Add Vendor",
                Width = 100,
                Padding = new Thickness(5),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };

            btnAddVendor.Click += btnAddVendor_Click;
            var btnEditVendor = new System.Windows.Controls.Button
            {
                Name = "btnEditVendor",
                Content = "Edit Vendor",
                Width = 100,
                Padding = new Thickness(5),
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center
            };

        var contentDialog = new Window
        {
            Title = titleString,
            PrimaryButtonText = "Save",
            SecondaryButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            Content = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, // Enable if needed
                Content = new StackPanel
                {
                    Margin = new Thickness(10),
                    Children =
                    {
                     new TextBlock { Text = "Item Name: ", Margin = new Thickness(0, 0, 0, 5) },
                    new System.Windows.Controls.TextBox {Name = "tbxItemName", Text=item.ItemName, Margin = new Thickness(0, 0, 0, 10) },
                    new TextBlock {Text = "Quantity On Hand: ", Margin = new Thickness(0, 0, 0, 5) },
                    integerUpDownQuantity,
                    new TextBlock {Text = "Item Status: ", Margin = new Thickness(0, 0, 0, 5)},
                    comboBoxStatus,
                    new TextBlock {Text = "Description: ", Margin = new Thickness(0, 0, 0, 5) },
                    new System.Windows.Controls.TextBox {Name = "tbxItemDescription", TextWrapping=TextWrapping.Wrap, AcceptsReturn=true, Text=item.Description, Margin = new Thickness(0, 0, 0, 10) },
                    new TextBlock {Text = "Unit Price: ", Margin = new Thickness(0, 0, 0, 5) },
                    doubleUpDownUnitPrice,
                    new TextBlock { Text = "Serial Number: ", Margin = new Thickness(0, 0, 0, 5) },
                    new System.Windows.Controls.TextBox {Name = "tbxSerialNumber", Text=item.SerialNumber, Margin = new Thickness(0, 0, 0, 10) },
                    new TextBlock { Text = "Warrenty Period Months: ", Margin = new Thickness(0, 0, 0, 5)},
                    integerUpDownWarrentyMonths,
                    new TextBlock {Text = "Reorder Level: ", Margin = new Thickness(0, 0, 0, 5 ) },
                    integerUpDownReorderLevel,
                    new TextBlock {Text = "Item Weight: ", Margin = new Thickness(0, 0, 0, 5 ) },
                    doubleUpDownItemWeight,
                    new TextBlock {Text = "Item Active: ", Margin = new Thickness(0, 0, 0, 5 ) },
                    new System.Windows.Controls.CheckBox{Name = "ckbItemActive",  Content = "Active", IsChecked=item.Active, Margin = new Thickness(0, 0, 0, 10)},
                    new TextBlock {Text = "Inventory Location: ", Margin = new Thickness(0, 0, 0, 5) },
                    new System.Windows.Controls.TextBox {Name = "tbxLocationName", TextWrapping=TextWrapping.Wrap, AcceptsReturn=true, Text=item.Location.LocationName, Margin = new Thickness(0, 0, 0, 10) },
                    new TextBlock {Text = "Inventory GUID: ", Margin = new Thickness(0, 0, 0, 5) },
                    new System.Windows.Controls.TextBox {Name = "tbxLocationGUID", TextWrapping=TextWrapping.Wrap, AcceptsReturn=true, Text=item.Location.LocationGUID, Margin = new Thickness(0, 0, 0, 10) },
                    new TextBlock {Text = "Vendor: ", Margin = new Thickness(0, 0, 0, 5) },
                    comboBoxVendor,
                    new Grid
                    {
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                        Margin = new Thickness(0, 0, 0, 10),
                        ColumnDefinitions =
                        {
                        new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                        new ColumnDefinition { Width = GridLength.Auto},
                        new ColumnDefinition { Width = new GridLength(10)},
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                    },
                    Children =
                    {
                        btnAddVendor,
                        btnEditVendor
                    }
                }      
            }
                }
            }
        };
        Grid.SetColumn(btnAddVendor, 1);
        Grid.SetColumn(btnAddVendor, 1);

        var result = await contentDialog.ShowAsync();
    }
    private T FindChild<T>(DependencyObject parent) where T : DependencyObject
    {
        if (parent == null) return null;

        int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            if (child is T foundChild)
            {
                return foundChild;
            }

            var childOfChild = FindChild<T>(child);
            if (childOfChild != null)
            {
                return childOfChild;
            }
        }
        return null;
    }
    private bool IsDialogOpen<T>() where T : FrameworkElement
    {
        foreach (Window window in System.Windows.Application.Current.Windows)
        {
            if (FindChild<T>(window) != null)
            {
                return true;
            }
        }
        return false;
    }
    private void btnAddVendor_Click(object sender, RoutedEventArgs e)
    {
        if (IsDialogOpen<ContentControl>()) // Adjust 'ContentControl' to the type of your dialog if different
        {
            page.CurrentDialog.Hide();
            System.Windows.MessageBox.Show("A dialog is already open."); // Optional notification
            return; // Prevent opening another dialog
        }

        AddEditVendor(new Vendor(), true);
    }

    private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        if (!char.IsDigit(e.Text, 0))
        {
            e.Handled = true;
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
    
    public async void AddEditVendor(Vendor selectedVendor, bool isNew)
    {
        string titleString;

        if (isNew == false)
        {
            titleString = "Editing Vendor...";
        }
        else
        {
            titleString = "Adding Vendor...";
        }
        Vendor vendor = new Vendor
        {
            VendorID = -1,
            VendorName = "",
            AccountOwner = -1,
            ContactPerson = "",
            PhoneNumber = "",
            Email = "",
            LeadTimeDays = 0
        };
        int vendorSelectedIndex = -1;
        int comboBoxSelected = -1;
        if (isNew == false)
        {
            vendor = selectedVendor;
            vendor.VendorID = selectedVendor.VendorID;
            vendor.VendorName = selectedVendor.VendorName;
            vendor.AccountOwner = selectedVendor.AccountOwner;
            vendor.ContactPerson = selectedVendor.ContactPerson;
            vendor.PhoneNumber = selectedVendor.PhoneNumber;
            vendor.Email = selectedVendor.Email;
            vendor.LeadTimeDays = selectedVendor.LeadTimeDays;
            comboBoxSelected = userManager.GetAllUserFirstAndLastName(userManager.GetAllUsers()).IndexOf(userManager.GetUserFirstNameLastName(vendor.VendorID));
        }
        else
        {

        }



        ==========================
         *       COMBO BOXES
         * =========================

        var comboBoxAccountOwner = new System.Windows.Controls.ComboBox
        {
            Name = "cbxAccountOwner",
            ItemsSource = userManager.GetAllUserFirstAndLastName(userManager.GetAllUsers()),
            SelectedIndex = vendorSelectedIndex,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
            Margin = new Thickness(0, 0, 0, 10)
        };


        /*===========================
         *     INTEGER SPINNERS
         * =========================
        var integerUpDownLeadTimeDays = new IntegerUpDown
        {
            Name = "intUpDownLeadTimeDays",
            Minimum = 0,
            Value = vendor.LeadTimeDays,
            Margin = new Thickness(0, 0, 0, 10)
        };

        integerUpDownLeadTimeDays.PreviewTextInput += IntegerUpDown_PreviewTextInput;

        /*===========================================
         *  STYLES FOR DOUBLE AND INTEGER SPINNERS
         * =========================================
        // Define style for IntegerUpDown
        var styleIntUpDown = new Style(typeof(IntegerUpDown));

        // Background color consistent with ModernWPF light/dark themes
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.BackgroundProperty, new SolidColorBrush(Color.FromRgb(43, 43, 43)))); // Dark background
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.ForegroundProperty, new SolidColorBrush(Color.FromRgb(230, 230, 230)))); // Light text
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.BorderBrushProperty, new SolidColorBrush(Color.FromRgb(77, 77, 77))));    // Subtle border

        // Corner radius for a softer, modern appearance
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.PaddingProperty, new Thickness(8)));
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.BorderThicknessProperty, new Thickness(1)));

        // Stretch control width
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.TextAlignmentProperty, TextAlignment.Left));

        // Font and content alignment
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.FontSizeProperty, 14.0));
        styleIntUpDown.Setters.Add(new Setter(IntegerUpDown.FontFamilyProperty, new System.Windows.Media.FontFamily("Segoe UI")));
        integerUpDownLeadTimeDays.Style = styleIntUpDown;

        var styleDoubleUpDown = new Style(typeof(DoubleUpDown));

        /*==================================
         *              BUTTONS
         * =================================
        var btnAddUser = new System.Windows.Controls.Button
        {
            Name = "btnAddUser",
            Content = "Add User",
            Width = 100,
            Padding = new Thickness(5),
            HorizontalAlignment = System.Windows.HorizontalAlignment.Center
        };
        var btnEditVendor = new System.Windows.Controls.Button
        {
            Name = "btnEditUser",
            Content = "Edit User",
            Width = 100,
            Padding = new Thickness(5),
            HorizontalAlignment = System.Windows.HorizontalAlignment.Center
        };

        var contentDialog = new ContentDialog
        {
            Title = titleString,
            PrimaryButtonText = "Save",
            SecondaryButtonText = "Cancel",
            DefaultButton = ContentDialogButton.Primary,
            Content = new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled, // Enable if needed
                Content = new StackPanel
                {
                    Margin = new Thickness(10),
                    Children =
                    {
                        new TextBlock { Text = "Vendor Name: ", Margin = new Thickness(0, 0, 0, 5) },
                        new System.Windows.Controls.TextBox {Name = "tbxItemName", Text=vendor.VendorName, Margin = new Thickness(0, 0, 0, 10) },
                        new TextBlock {Text = "Account Owner: ", Margin = new Thickness(0, 0, 0, 5) },
                        comboBoxAccountOwner,

                            new TextBlock {Text = "Description: ", Margin = new Thickness(0, 0, 0, 5) },
                        new System.Windows.Controls.TextBox {Name = "tbxItemDescription", TextWrapping=TextWrapping.Wrap, AcceptsReturn=true, Text=item.Description, Margin = new Thickness(0, 0, 0, 10) },
                        new TextBlock {Text = "Unit Price: ", Margin = new Thickness(0, 0, 0, 5) },
                        doubleUpDownUnitPrice,
                        new TextBlock { Text = "Serial Number: ", Margin = new Thickness(0, 0, 0, 5) },
                        new System.Windows.Controls.TextBox {Name = "tbxSerialNumber", Text=item.SerialNumber, Margin = new Thickness(0, 0, 0, 10) },
                        new TextBlock { Text = "Warrenty Period Months: ", Margin = new Thickness(0, 0, 0, 5)},
                        integerUpDownWarrentyMonths,
                        new TextBlock {Text = "Reorder Level: ", Margin = new Thickness(0, 0, 0, 5 ) },
                        integerUpDownReorderLevel,
                        new TextBlock {Text = "Item Weight: ", Margin = new Thickness(0, 0, 0, 5 ) },
                        doubleUpDownItemWeight,
                        new TextBlock {Text = "Item Active: ", Margin = new Thickness(0, 0, 0, 5 ) },
                        new System.Windows.Controls.CheckBox{Name = "ckbItemActive",  Content = "Active", IsChecked=item.Active, Margin = new Thickness(0, 0, 0, 10)},
                        new TextBlock {Text = "Inventory Location: ", Margin = new Thickness(0, 0, 0, 5) },
                        new System.Windows.Controls.TextBox {Name = "tbxLocationName", TextWrapping=TextWrapping.Wrap, AcceptsReturn=true, Text=item.Location.LocationName, Margin = new Thickness(0, 0, 0, 10) },
                        new TextBlock {Text = "Inventory GUID: ", Margin = new Thickness(0, 0, 0, 5) },
                        new System.Windows.Controls.TextBox {Name = "tbxLocationGUID", TextWrapping=TextWrapping.Wrap, AcceptsReturn=true, Text=item.Location.LocationGUID, Margin = new Thickness(0, 0, 0, 10) },
                        new TextBlock {Text = "Vendor: ", Margin = new Thickness(0, 0, 0, 5) },
                        comboBoxVendor,
                        new Grid
                        {
                            HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                            Margin = new Thickness(0, 0, 0, 10),
                            ColumnDefinitions =
                            {
                            new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                            new ColumnDefinition { Width = GridLength.Auto},
                            new ColumnDefinition { Width = new GridLength(10)},
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) },
                        },
                        Children =
                        {
                            btnAddVendor,
                            btnEditVendor
                        }
                    }
                }
                    }
                }
            };

            var result = await contentDialog.ShowAsync();
        }
    }*/    
}

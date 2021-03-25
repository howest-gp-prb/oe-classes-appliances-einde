using System;
using System.Windows;
using System.Windows.Controls;
using Prb.ElectricalAppliances.Core;

namespace Prb.ElectricalAppliances.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Store store;
        bool isNew;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            store = new Store();
            PopulateCmbFilter();
            PopulateCmbApplianceType();
            PopulateLstAppliances();
            ClearControls();
            EnableLeftSide();
            DoStats();
        }

        private void EnableLeftSide()
        {
            grpAppliances.IsEnabled = true;
            grpDetails.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }
        private void EnableRightSide()
        {
            grpAppliances.IsEnabled = false;
            grpDetails.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }
        private void PopulateCmbFilter()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("<alle toestellen>");
            //cmbFilter.Items.Add(ToestelSoort.Diepvries);
            foreach (ApplianceType applianceType in Enum.GetValues(typeof(ApplianceType)))
            {
                cmbFilter.Items.Add(applianceType);
            }
            cmbFilter.SelectedIndex = 0;
        }
        private void PopulateLstAppliances()
        {
            lstAppliances.ItemsSource = null;
            if (cmbFilter.SelectedIndex == 0)
            {
                lstAppliances.ItemsSource = store.Appliances;
            }
            else
            {
                ApplianceType applianceType = (ApplianceType)cmbFilter.SelectedItem;
                lstAppliances.ItemsSource = store.FilterByType(applianceType);
            }
        }
        private void PopulateCmbApplianceType()
        {
            cmbApplianceType.Items.Clear();
            foreach (ApplianceType applianceType in Enum.GetValues(typeof(ApplianceType)))
            {
                cmbApplianceType.Items.Add(applianceType);
            }
        }
        private void ClearControls()
        {
            txtBrand.Text = "";
            txtSeries.Text = "";
            cmbApplianceType.SelectedIndex = -1;
            txtSellingPrice.Text = "";
            txtStock.Text = "";
            txtWatt.Text = "";
            txtVoltage.Text = "";
            lblAmpere.Content = "";

        }
        private void PopulateControls(Appliance appliance)
        {
            txtBrand.Text = appliance.Brand;
            txtSeries.Text = appliance.Series;
            cmbApplianceType.SelectedItem = appliance.ApplianceType;
            txtSellingPrice.Text = appliance.SellingPrice.ToString("#,##0.00");
            txtStock.Text = appliance.Stock.ToString();
            txtWatt.Text = appliance.Watt.ToString();
            txtVoltage.Text = appliance.Voltage.ToString();
            lblAmpere.Content = appliance.Ampere.ToString("0.00");
        }
        private void DoStats()
        {
            lblTotalApplianceCount.Content = store.TotalApplianceCount;
            lblTotalStockValue.Content = store.TotalStockValue.ToString("#,##0.00");
        }
        private void LstAppliances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstAppliances.SelectedItem != null)
            {
                Appliance appliance = (Appliance)lstAppliances.SelectedItem;
                PopulateControls(appliance);
            }
        }
        private void CmbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateLstAppliances();
        }
        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ClearControls();
            EnableRightSide();
            cmbApplianceType.SelectedIndex = 0;
            txtBrand.Focus();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstAppliances.SelectedItem != null)
            {
                isNew = false;
                lblAmpere.Content = "";
                EnableRightSide();
                txtBrand.Focus();
            }
            else
            {
                MessageBox.Show("Selecteer eerst een toestel!", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstAppliances.SelectedItem != null)
            {
                if (MessageBox.Show("Ben je zeker?", "Wissen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Appliance appliance = (Appliance)lstAppliances.SelectedItem;
                    store.DeleteAppliance(appliance);
                    ClearControls();
                    PopulateLstAppliances();
                    DoStats();
                }
            }
            else
            {
                MessageBox.Show("Selecteer eerst een toestel!", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
            EnableLeftSide();
            LstAppliances_SelectionChanged(null, null);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string brand = txtBrand.Text.Trim();
            string series = txtSeries.Text.Trim();
            ApplianceType applianceType = (ApplianceType)cmbApplianceType.SelectedItem;
            decimal sellingPrice;
            decimal.TryParse(txtSellingPrice.Text, out sellingPrice);
            int stock;
            int.TryParse(txtStock.Text, out stock);
            int watt;
            int.TryParse(txtWatt.Text, out watt);
            int voltage;
            int.TryParse(txtVoltage.Text, out voltage);

            Appliance appliance;
            if (isNew)
            {
                appliance = new Appliance(brand, series, applianceType, sellingPrice, stock, watt, voltage);
                store.AddAppliance(appliance);
            }
            else
            {
                appliance = (Appliance)lstAppliances.SelectedItem;
                appliance.Brand = brand;
                appliance.Series = series;
                appliance.ApplianceType = applianceType;
                appliance.SellingPrice = sellingPrice;
                appliance.Stock = stock;
                appliance.Watt = watt;
                appliance.Voltage = voltage;
            }
            PopulateLstAppliances();
            lstAppliances.SelectedItem = appliance;
            LstAppliances_SelectionChanged(null, null);
            EnableLeftSide();
            DoStats();
        }
    }
}

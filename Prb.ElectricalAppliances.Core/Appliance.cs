using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.ElectricalAppliances.Core
{
    public enum ApplianceType { Vaatwas, Oven, Wasmachine, Droogkast, Koelkast, Diepvries }

    public class Appliance
    {
        private string brand;
        private string series;
        private ApplianceType applianceType;
        private decimal sellingPrice;
        private int stock;
        private int watt;
        private int voltage;

        public string Brand
        {
            get { return brand; }
            set
            {
                value = value.Trim();
                if (value == "")
                    value = "Onbekend merk";
                brand = value;
            }
        }
        public string Series
        {
            get { return series; }
            set
            {
                value = value.Trim();
                if (value == "")
                    value = "Onbekende serie";
                series = value;
            }
        }
        public ApplianceType ApplianceType
        {
            get { return applianceType; }
            set { applianceType = value; }
        }
        public decimal SellingPrice
        {
            get { return sellingPrice; }
            set
            {
                if (value < 0)
                    value = 0m;
                sellingPrice = value;
            }
        }
        public int Stock
        {
            get { return stock; }
            set
            {
                if (value < 0)
                    value = 0;
                stock = value;
            }
        }
        public int Watt
        {
            get { return watt; }
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 5000)
                    value = 5000;
                watt = value;
            }
        }
        public int Voltage
        {
            get { return voltage; }
            set
            {
                if (value < 110)
                    value = 110;
                if (value > 400)
                    value = 400;
                voltage = value;
            }
        }
        public double Ampere
        {
            get
            {
                return 1.0 * watt / voltage;
            }
        }

        public Appliance()
        { }
        public Appliance(string brand, string series, ApplianceType applianceType, decimal sellingPrice, int stock, int watt, int voltage)
        {
            Brand = brand;
            Series = series;
            ApplianceType = applianceType;
            SellingPrice = sellingPrice;
            Stock = stock;
            Watt = watt;
            Voltage = voltage;
        }
        public override string ToString()
        {
            return $"{Brand} - {Series}";
        }


    }
}

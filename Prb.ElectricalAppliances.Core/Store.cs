using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Prb.ElectricalAppliances.Core
{
    public class Store
    {
        public List<Appliance> Appliances { get; private set; }
        public int TotalApplianceCount
        {
            get
            {
                int count = 0;
                foreach (Appliance appliance in Appliances)
                {
                    count += appliance.Stock;
                }
                return count;
            }
        }
        public decimal TotalStockValue
        {
            get
            {
                decimal total = 0m;
                foreach (Appliance appliance in Appliances)
                {
                    total += appliance.SellingPrice;
                }
                return total;
            }
        }

        public Store()
        {
            Appliances = new List<Appliance>();
            DoSeeding();
            Sort();
        }
        private void DoSeeding()
        {
            Appliances.Add(new Appliance("Siemens", "ABC", ApplianceType.Oven, 399.99m, 7, 2500, 220));
            Appliances.Add(new Appliance("Siemens", "BCD", ApplianceType.Oven, 450m, 6, 2500, 220));
            Appliances.Add(new Appliance("Zanusi", "XU", ApplianceType.Oven, 299.99m, 12, 2000, 230));
            Appliances.Add(new Appliance("Zanusi", "YZ", ApplianceType.Oven, 320m, 9, 2200, 230));
            Appliances.Add(new Appliance("AEG", "XZ123", ApplianceType.Koelkast, 375m, 2, 1000, 220));
            Appliances.Add(new Appliance("AEG", "XYZ45", ApplianceType.Koelkast, 450m, 6, 1100, 220));
            Appliances.Add(new Appliance("Zanusi", "KK101", ApplianceType.Koelkast, 199.99m, 12, 1200, 230));
            Appliances.Add(new Appliance("Zanusi", "KK102", ApplianceType.Koelkast, 220m, 9, 1200, 230));

            Appliances.Add(new Appliance("Siemens", "VW1", ApplianceType.Vaatwas, 599.99m, 7, 1500, 220));
            Appliances.Add(new Appliance("Siemens", "VW2", ApplianceType.Vaatwas, 650m, 6, 1500, 220));
            Appliances.Add(new Appliance("Zanusi", "WMXU", ApplianceType.Wasmachine, 499.99m, 12, 1600, 230));
            Appliances.Add(new Appliance("Zanusi", "WMYZ", ApplianceType.Wasmachine, 420m, 9, 1700, 230));
            Appliances.Add(new Appliance("AEG", "DK123", ApplianceType.Droogkast, 475m, 2, 3000, 220));
            Appliances.Add(new Appliance("AEG", "DKZ45", ApplianceType.Droogkast, 650m, 6, 3500, 220));
            Appliances.Add(new Appliance("Zanusi", "DV101", ApplianceType.Diepvries, 299.99m, 12, 900, 230));
            Appliances.Add(new Appliance("Zanusi", "DV102", ApplianceType.Diepvries, 470m, 9, 1100, 230));
        }
        private void Sort()
        {
            Appliances = Appliances.OrderBy(t => t.Brand).ThenBy(t => t.Series).ToList();
        }
        public void AddAppliance(Appliance appliance)
        {
            Appliances.Add(appliance);
            Sort();
        }
        public void DeleteAppliance(Appliance appliance)
        {
            Appliances.Remove(appliance);
        }
        public List<Appliance> FilterByType(ApplianceType applianceType)
        {
            List<Appliance> filteredList = new List<Appliance>();
            foreach (Appliance appliance in Appliances)
            {
                if (appliance.ApplianceType == applianceType)
                {
                    filteredList.Add(appliance);
                }
            }
            return filteredList;
        }
    }
}

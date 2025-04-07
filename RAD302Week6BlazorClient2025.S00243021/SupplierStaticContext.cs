using RAD302Week6Lab2025.DataModel.S00243021;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAD302Week6BlazorClient.S00243021
{
    public static class SupplierStaticContext
    {
        public static Supplier CurrentSupplier { get; set; }
        public static Product[] Products { get; set; }
        public static Supplier[] Suppliers { get; set; }

        public static Supplier GetNewSupplier(int supplierID)
        {
            return CurrentSupplier = Suppliers.FirstOrDefault(s => s.SupplierID == supplierID);
        }
    }
}
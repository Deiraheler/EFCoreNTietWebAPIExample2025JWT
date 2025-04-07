using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week7SupplierDataModelS00243021;

namespace Week7SupplierDataServiceS00243021
{
    public interface ISupplierService
    {
        Task<List<Supplier>> getSuppliers();
        Task<Supplier> PostSupplier(string endpoint, Supplier s);
        Task<bool> login(string username, string password);
    }
}

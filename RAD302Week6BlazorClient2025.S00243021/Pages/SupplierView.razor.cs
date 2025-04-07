using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using RAD302Week6Lab2025.DataModel.S00243021;
using RAD302Week6BlazorClient.S00243021;
using System.Net.Http.Json;

namespace RAD302Week6BlazorClient2025.S00243021.Pages
{
    public partial class SupplierView
    {
        private Supplier[] suppliers;
        private Product[] products;

        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // Loading page for the first time
            if (SupplierStaticContext.Suppliers == null)
            {
                SupplierStaticContext.Suppliers = await Http.GetFromJsonAsync<Supplier[]>("sample-data/Supplier.json");
                SupplierStaticContext.Products = await Http.GetFromJsonAsync<Product[]>("sample-data/Product.json");
                products = SupplierStaticContext.Products;
                suppliers = SupplierStaticContext.Suppliers;

                InitialiseSupplierProducts();
            }
            else
            {
                suppliers = SupplierStaticContext.Suppliers;
                products = SupplierStaticContext.Products;
            }
            await base.OnInitializedAsync();
        }

        private void InitialiseSupplierProducts()
        {
            if (suppliers != null && products != null)
            {
                foreach (var item in suppliers)
                {
                    List<Product> toadd = products
                        .OrderBy(o => new Random().Next(2372))
                        .Take(3)
                        .ToList();

                    item.SupplierProducts = new List<Product>();
                    item.SupplierProducts.AddRange(toadd);
                }
            }
            else
            {
                Console.WriteLine("Suppliers or products are null.");
            }
        }

    }
}

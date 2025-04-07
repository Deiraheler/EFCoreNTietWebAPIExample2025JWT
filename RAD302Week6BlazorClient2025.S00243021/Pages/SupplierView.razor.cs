using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;
using RAD302Week6Lab2025.DataModel.S00243021;

namespace RAD302Week6BlazorClient2025.S00243021.Pages
{
    public partial class SupplierView
    {
        [Inject]
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync()
        {
            // Load data using HttpClient
        }
    }
}

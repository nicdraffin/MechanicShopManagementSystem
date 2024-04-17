using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MechanicAPP_OOP2.Data;
using MechanicAPP_OOP2.Model;

namespace Utility
{

    public partial class ProductsViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;

        public ProductsViewModel(DatabaseContext context)
        {
            _context = context;
        }

        [ObservableProperty]
        private ObservableCollection<Customer> _products = new();

        [ObservableProperty]
        private Customer _operatingProduct = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        public async Task LoadProductsAsync()
        {
            await ExecuteAsync(async () =>
            {
                var products = await _context.GetAllAsync<Customer>();
                if (products is not null && products.Any())
                {
                    Products ??= new ObservableCollection<Customer>();

                    foreach (var product in products)
                    {
                        Products.Add(product);
                    }
                }
            }, "Fetching products...");
        }

        [RelayCommand]
        private void SetOperatingProduct(Customer? product) => OperatingProduct = product ?? new();

        [RelayCommand]
        private async Task SaveProductAsync()
        {
            if (OperatingProduct is null)
                return;

            var (isValid, errorMessage) = OperatingProduct.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "OK");
                return;
            }

            var busyText = OperatingProduct.Id == 0 ? "Creating product..." : "Updating product...";
            await ExecuteAsync(async () =>
            {
                if (OperatingProduct.Id == 0)
                {
                    // Create product
                    await _context.AddItemAsync<Customer>(OperatingProduct);
                    Products.Add(OperatingProduct);
                }
                else
                {
                    // Update product
                    if (await _context.UpdateItemAsync<Customer>(OperatingProduct))
                    {
                        var productCopy = OperatingProduct.Clone();
                        var index = Products.IndexOf(OperatingProduct);
                        Products.RemoveAt(index);
                        Products.Insert(index, productCopy);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Product updation error", "OK");
                        return;
                    }
                }
                SetOperatingProductCommand.Execute(new());
            }, busyText);
        }

        [RelayCommand]
        private async Task DeleteProductAsync(int id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<Customer>(id))
                {
                    var product = Products.FirstOrDefault(p => p.Id == id);
                    Products.Remove(product);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Product was not deleted", "OK");
                }
            }, "Deleting product...");
        }

        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }
}

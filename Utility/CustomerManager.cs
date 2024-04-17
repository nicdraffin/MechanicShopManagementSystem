﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MechanicAPP_OOP2.Data;
using MechanicAPP_OOP2.Model;

namespace MechanicAPP_OOP2.Utility
{
    public partial class CustomerViewModel : ObservableObject
    {
        private readonly DatabaseContext _context;

        public CustomerViewModel(DatabaseContext context)
        {
            _context = context;
        }

        [ObservableProperty]
        private ObservableCollection<Customer> _customers = new();

        [ObservableProperty]
        private Customer _operatingCustomer = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        public async Task LoadcustomersAsync()
        {
            await ExecuteAsync(async () =>
            {
                var customers = await _context.GetAllAsync<Customer>();
                if (customers is not null && customers.Any())
                {
                    Customers ??= new ObservableCollection<Customer>();

                    foreach (var customer in customers)
                    {
                        Customers.Add(customer);
                    }
                }
            }, "Fetching customers...");
        }

        [RelayCommand]
        private void SetOperatingCustomer(Customer? customer) => OperatingCustomer = customer ?? new();

        [RelayCommand]
        private async Task SavecustomerAsync()
        {
            if (OperatingCustomer is null)
                return;

            var (isValid, errorMessage) = OperatingCustomer.Validate();
            if (!isValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", errorMessage, "OK");
                return;
            }

            var busyText = string.IsNullOrWhiteSpace(OperatingCustomer.Id) ? "Creating customer..." : "Updating customer...";
            await ExecuteAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(OperatingCustomer.Id))
                {
                    await _context.AddItemAsync<Customer>(OperatingCustomer);
                    Customers.Add(OperatingCustomer);
                }
                else
                {
                    // Update customer
                    if (await _context.UpdateItemAsync<Customer>(OperatingCustomer))
                    {
                        var customerCopy = OperatingCustomer.Clone();
                        var index = Customers.IndexOf(OperatingCustomer);
                        Customers.RemoveAt(index);
                        Customers.Insert(index, customerCopy);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Customer updation error", "OK");
                        return;
                    }
                }
                SetOperatingCustomerCommand.Execute(new());
            }, busyText);
        }

        [RelayCommand]
        private async Task DeleteCustomerAsync(string id)
        {
            await ExecuteAsync(async () =>
            {
                if (await _context.DeleteItemByKeyAsync<Customer>(id))
                {
                    var customer = Customers.FirstOrDefault(p => p.Id == id);
                    Customers.Remove(customer);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Delete Error", "Customer was not deleted", "OK");
                }
            }, "Deleting customer...");
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

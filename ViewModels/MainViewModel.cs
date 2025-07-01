using HydroOffice.Commands;
using HydroOffice.Database.Models;
using HydroOffice.Database.Repositories.BaseImplementation;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HydroOffice.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly IServiceProvider _provider;

    public RelayCommand ShowEmployeesCommand { get; }
    public RelayCommand ShowContractorsCommand { get; }
    public RelayCommand ShowOrdersCommand { get; }

    public object CurrentViewModel { get; private set; }

    public MainViewModel(IServiceProvider provider)
    {
        _provider = provider;

        ShowEmployeesCommand = new RelayCommand(_ => ShowEmployees());
        ShowContractorsCommand = new RelayCommand(_ => ShowContractors());
        ShowOrdersCommand = new RelayCommand(_ => ShowOrders());

        ShowEmployees();
    }

    private void ShowEmployees()
    {
        CurrentViewModel = new EmployeeListViewModel(
            _provider.GetRequiredService<IRepository<Employee>>(),
            ShowEmployeeForm);
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void ShowEmployeeForm(Employee? employee)
    {
        CurrentViewModel = new EmployeeFormViewModel(
            _provider.GetRequiredService<IRepository<Employee>>(),
            ShowEmployees,
            employee);
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void ShowContractors()
    {
        CurrentViewModel = new ContractorListViewModel(
            _provider.GetRequiredService<IRepository<Contractor>>(),
            ShowContractorForm);
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void ShowContractorForm(Contractor? contractor)
    {
        CurrentViewModel = new ContractorFormViewModel(
            _provider.GetRequiredService<IRepository<Contractor>>(),
            _provider.GetRequiredService<IRepository<Employee>>(),
            ShowContractors,
            contractor);
        OnPropertyChanged(nameof(CurrentViewModel));
    }
    private void ShowOrderForm(Order? order)
    {
        CurrentViewModel = new OrderFormViewModel(
            _provider.GetRequiredService<IRepository<Order>>(),
            _provider.GetRequiredService<IRepository<Employee>>(),
            _provider.GetRequiredService<IRepository<Contractor>>(),
            ShowOrders,
            order);
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void ShowOrders()
    {
        CurrentViewModel = new OrderListViewModel(
            _provider.GetRequiredService<IRepository<Order>>(),
            ShowOrderForm);
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
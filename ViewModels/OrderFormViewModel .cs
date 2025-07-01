using HydroOffice.Commands;
using HydroOffice.Database.Models;
using HydroOffice.Database.Repositories.BaseImplementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HydroOffice.ViewModels;

public class OrderFormViewModel : INotifyPropertyChanged
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Employee> _employeeRepository;
    private readonly IRepository<Contractor> _contractorRepository;
    private readonly Action _onSaved;

    public Order Order { get; }

    public ObservableCollection<Employee> Employees { get; } = new();
    public ObservableCollection<Contractor> Contractors { get; } = new();

    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); }
    }

    public RelayCommand SaveCommand { get; }
    public RelayCommand ClearEmployeeCommand { get; }
    public RelayCommand ClearContractorCommand { get; }

    public OrderFormViewModel(
        IRepository<Order> orderRepository,
        IRepository<Employee> employeeRepository,
        IRepository<Contractor> contractorRepository,
        Action onSaved,
        Order? order = null)
    {
        _orderRepository = orderRepository;
        _employeeRepository = employeeRepository;
        _contractorRepository = contractorRepository;
        _onSaved = onSaved;

        Order = order ?? new Order { Date = DateTime.Today };

        SaveCommand = new RelayCommand(async _ => await Save());
        ClearContractorCommand = new RelayCommand(_ => Order.Contractor = null);
        ClearEmployeeCommand = new RelayCommand(_ => Order.Employee = null);
        _ = LoadData();
    }

    private async Task LoadData()
    {
        var emps = await _employeeRepository.GetAllAsync();
        var conts = await _contractorRepository.GetAllAsync();

        App.Current.Dispatcher.Invoke(() =>
        {
            Employees.Clear();
            Contractors.Clear();
            foreach (var e in emps) Employees.Add(e);
            foreach (var c in conts) Contractors.Add(c);
        });
    }

    private async Task Save()
    {
        // Валидация
        if (Order.Amount <= 0)
        {
            ErrorMessage = "Сумма должна быть положительной.";
            return;
        }

        try
        {
            if (Order.Id == 0)
                await _orderRepository.AddAsync(Order);
            else
                await _orderRepository.UpdateAsync(Order);

            _onSaved();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Ошибка при сохранении: {ex.Message}";
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

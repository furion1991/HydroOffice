using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HydroOffice.Commands;
using HydroOffice.Database.Models;
using HydroOffice.Database.Repositories.BaseImplementation;

namespace HydroOffice.ViewModels;

public class EmployeeListViewModel : INotifyPropertyChanged
{
    private readonly IRepository<Employee> _repository;
    private readonly Action<Employee?> _onEditRequested;


    public ObservableCollection<Employee> Employees { get; set; } = [];

    public RelayCommand AddCommand { get; }
    public RelayCommand DeleteCommand { get; }
    public RelayCommand EditCommand { get; }

    private Employee? _selectedEmployee;

    public Employee? SelectedEmployee
    {
        get => _selectedEmployee;
        set { _selectedEmployee = value; OnPropertyChanged(); }
    }

    public EmployeeListViewModel(IRepository<Employee> repository, Action<Employee?> onEditRequested)
    {
        _repository = repository;
        _onEditRequested = onEditRequested;
        AddCommand = new RelayCommand(_ => _onEditRequested(null));
        EditCommand = new RelayCommand(_ => _onEditRequested(SelectedEmployee), _ => SelectedEmployee != null);
        DeleteCommand = new RelayCommand(async _ => await DeleteEmployee(), _ => SelectedEmployee != null);

        _ = LoadEmployees();
    }

    private async Task LoadEmployees()
    {
        var list = await _repository.GetAllAsync();
        App.Current.Dispatcher.Invoke(() =>
        {
            Employees.Clear();
            foreach (var employee in list)
            {
                Employees.Add(employee);
            }

        });

    }

    private async Task DeleteEmployee()
    {
        if (SelectedEmployee == null)
        {
            return;
        }
        await _repository.DeleteAsync(SelectedEmployee);
        await LoadEmployees();

    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
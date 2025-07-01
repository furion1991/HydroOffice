using HydroOffice.Commands;
using HydroOffice.Database.Models;
using HydroOffice.Database.Repositories.BaseImplementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HydroOffice.ViewModels;

public class ContractorFormViewModel : INotifyPropertyChanged
{
    private readonly IRepository<Contractor> _contractorRepository;
    private readonly IRepository<Employee> _employeeRepository;
    private readonly Action _onSaved;
    private string? _errorMessage;

    public string? ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); }
    }

    public void ShowError(string message)
    {
        ErrorMessage = message;
    }
    public Contractor Contractor { get; }

    public ObservableCollection<Employee> Employees { get; } = new();

    public RelayCommand SaveCommand { get; }

    public ContractorFormViewModel(
        IRepository<Contractor> contractorRepository,
        IRepository<Employee> employeeRepository,
        Action onSaved,
        Contractor? contractor = null)
    {


        _contractorRepository = contractorRepository;
        _employeeRepository = employeeRepository;
        _onSaved = onSaved;

        Contractor = contractor ?? new Contractor();
        SaveCommand = new RelayCommand(async _ => await Save());

        _ = LoadEmployees();
    }

    private async Task Save()
    {
        ErrorMessage = null;
        if (string.IsNullOrWhiteSpace(Contractor.Name))
        {
            ShowError($"Имя не может быть пустым");
            return;
        }

        if (Contractor.INN is null || !(Contractor.INN.Length is 10 or 12))
        {
            ShowError("Неправильный ИНН.\nИНН должен содержать 10 или 12 цифр");
            return;
        }

        if (Contractor.Curator == null)
        {
            ShowError("Выберите куратора");
            return;
        }

        try
        {
            if (Contractor.Id == 0)
                await _contractorRepository.AddAsync(Contractor);
            else
                await _contractorRepository.UpdateAsync(Contractor);

            _onSaved();
        }
        catch (Exception e)
        {

            ShowError($"{e.Message}");
        }
    }

    private async Task LoadEmployees()
    {
        var list = await _employeeRepository.GetAllAsync();
        App.Current.Dispatcher.Invoke(() =>
        {
            Employees.Clear();
            foreach (var emp in list)
                Employees.Add(emp);
        });
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
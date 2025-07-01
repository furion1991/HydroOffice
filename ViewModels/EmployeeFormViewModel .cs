using HydroOffice.Commands;
using HydroOffice.Database.Models;
using HydroOffice.Database.Repositories.BaseImplementation;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using HydroOffice.Utility;

namespace HydroOffice.ViewModels;

public class EmployeeFormViewModel : INotifyPropertyChanged
{
    private readonly IRepository<Employee> _repository;
    private readonly Action _onSaved;
    private string? _errorMessage;
    public string? ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); }
    }
    public Employee Employee { get; }

    public RelayCommand SaveCommand { get; }
    public IEnumerable<EnumDisplayItem<Position>> EnumValues =>
        Enum.GetValues(typeof(Position))
            .Cast<Position>()
            .Select(pos => new EnumDisplayItem<Position>(pos, pos.GetDescription()));


    public EmployeeFormViewModel(IRepository<Employee> repository, Action onSaved, Employee? employee = null)
    {
        _repository = repository;
        _onSaved = onSaved;

        Employee = employee ?? new Employee { DateOfBirth = DateTime.Today };
        SaveCommand = new RelayCommand(async _ => await Save());
    }

    private async Task Save()
    {
        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(Employee.FullName))
        {
            ShowError("ФИО не может быть пустым.");
            return;
        }

        if (Employee.DateOfBirth > DateTime.Today)
        {
            ShowError("Дата рождения не может быть в будущем.");
            return;
        }

        var age = DateTime.Today.Year - Employee.DateOfBirth.Year;
        if (age < 14)
        {
            ShowError("Сотрудник должен быть старше 14 лет.");
            return;
        }

        try
        {
            if (Employee.Id == 0)
                await _repository.AddAsync(Employee);
            else
                await _repository.UpdateAsync(Employee);

            _onSaved();
        }
        catch (Exception ex)
        {
            ShowError($"Ошибка при сохранении: {ex.Message}");
        }
        HideError();
    }

    private void HideError()
    {
        ErrorMessage = null;
    }
    private void ShowError(string message)
    {
        ErrorMessage = message;
    }
    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class EnumDisplayItem<T>(T value, string description)
{
    public T Value { get; set; } = value;
    public string Description { get; set; } = description;
}

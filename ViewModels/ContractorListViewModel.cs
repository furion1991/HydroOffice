using HydroOffice.Commands;
using HydroOffice.Database.Models;
using HydroOffice.Database.Repositories.BaseImplementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HydroOffice.ViewModels;

public class ContractorListViewModel : INotifyPropertyChanged
{
    private readonly IRepository<Contractor> _contractorRepository;
    private readonly Action<Contractor?> _onEditRequested;

    public ObservableCollection<Contractor> Contractors { get; set; } = new();

    public RelayCommand AddCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }

    private Contractor? _selectedContractor;
    public Contractor? SelectedContractor
    {
        get => _selectedContractor;
        set { _selectedContractor = value; OnPropertyChanged(); }
    }

    public ContractorListViewModel(IRepository<Contractor> contractorRepository, Action<Contractor?> onEditRequested)
    {
        _contractorRepository = contractorRepository;
        _onEditRequested = onEditRequested;

        AddCommand = new RelayCommand(_ => _onEditRequested(null));
        EditCommand = new RelayCommand(_ => _onEditRequested(SelectedContractor), _ => SelectedContractor != null);
        DeleteCommand = new RelayCommand(async _ => await Delete(), _ => SelectedContractor != null);

        _ = Load();
    }

    private async Task Load()
    {
        var list = await _contractorRepository.GetAllAsync();
        App.Current.Dispatcher.Invoke(() =>
        {
            Contractors.Clear();
            foreach (var c in list)
                Contractors.Add(c);
        });
    }

    private async Task Delete()
    {
        if (SelectedContractor == null) return;
        await _contractorRepository.DeleteAsync(SelectedContractor);
        await Load();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
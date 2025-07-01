using HydroOffice.Commands;
using HydroOffice.Database.Repositories.BaseImplementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using HydroOffice.Database.Models;

namespace HydroOffice.ViewModels;

public class OrderListViewModel : INotifyPropertyChanged
{
    private readonly IRepository<Order> _repository;
    private readonly Action<Order?> _onEditRequested;
    public ObservableCollection<Order> Orders { get; set; } = new();
    public RelayCommand AddCommand { get; }
    public RelayCommand EditCommand { get; }
    public RelayCommand DeleteCommand { get; }

    private Order? _selectedOrder;
    public Order? SelectedOrder
    {
        get => _selectedOrder;
        set { _selectedOrder = value; OnPropertyChanged(); }
    }

    public OrderListViewModel(IRepository<Order> repository, Action<Order?> onEditRequested)
    {
        _repository = repository;
        _onEditRequested = onEditRequested;

        AddCommand = new RelayCommand(_ => _onEditRequested(null));
        EditCommand = new RelayCommand(_ => _onEditRequested(SelectedOrder), _ => SelectedOrder != null);
        DeleteCommand = new RelayCommand(async _ => await Delete(), _ => SelectedOrder != null);

        _ = Load();
    }

    private async Task Load()
    {
        var list = await _repository.GetAllAsync();
        App.Current.Dispatcher.Invoke(() =>
        {
            Orders.Clear();
            foreach (var o in list)
                Orders.Add(o);
        });
    }

    private async Task Delete()
    {
        var result = MessageBox.Show(
            $"Удалить заказ от {SelectedOrder.Date:dd.MM.yyyy} на сумму {SelectedOrder.Amount}?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
        );

        if (result != MessageBoxResult.Yes)
            return;
        if (SelectedOrder == null) return;
        await _repository.DeleteAsync(SelectedOrder);
        await Load();
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
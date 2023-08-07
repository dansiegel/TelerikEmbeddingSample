using System.Collections.ObjectModel;
using TelerikEmbeddingSample.Business.Services;

namespace TelerikEmbeddingSample.Presentation;

internal class DataGridControlViewModel
{
    private const string OrdersPath = "OrdersDataSource.xml";
    private const string PeoplePath = "PeopleDataSource.xml";

    public DataGridControlViewModel(DataGenerator generator)
    {
        OrderDetails = generator.GetItems<ObservableCollection<Order>>(OrdersPath);
        People = generator.GetItems<ObservableCollection<SalesPerson>>(PeoplePath);
    }

    public ObservableCollection<Order> OrderDetails { get; }
    public ObservableCollection<SalesPerson> People { get; }
}

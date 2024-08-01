using TradingDataModel;

namespace DataAccess;

internal class InMemoryDataSource : IDataSource
{
    private List<ShareLot> _lots = new();

    public void AddNewLot(ShareLot lot)
    {
        _lots.Add(lot);
    }

    public List<ShareLot> GetLots()
    {
        return _lots;
    }

    public int GetRemainingStockQuantity()
    {
        return _lots.Sum(l => l.RemainingQuantity);
    }
}

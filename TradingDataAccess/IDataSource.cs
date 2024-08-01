using TradingDataModel;

namespace DataAccess;

public interface IDataSource
{
    public int GetRemainingStockQuantity();
    public List<ShareLot> GetLots();
    public void AddNewLot(ShareLot lot);
}

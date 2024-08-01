using DataAccess;
using TradingDataModel;

namespace Controllers;

/// <summary>
/// Methods related to opening and closing trades.
/// </summary>
public static class TradingController
{
    public static void AddTestTradingData(IDataSource dataSource)
    {
        CreateLot(100, 20, dataSource);
        CreateLot(150, 30, dataSource);
        CreateLot(120, 10, dataSource);
    }

    public static void CreateLot(int shareQuantity, decimal openPrice, IDataSource dataSource)
    {
        var lotInfo = new ShareLot(openPrice, shareQuantity);
        dataSource.AddNewLot(lotInfo);
    }

    public static SaleResult SellStocksFifo(int quantity, decimal price, IDataSource dataSource)
    {
        var totalSoldShareCost = 0m;
        var unsoldShareQuantity = 0;
        var unsoldShareCost = 0m;
        var remainingSharesToSell = quantity;

        foreach (var lot in dataSource.GetLots())
        {
            if (remainingSharesToSell == 0)
            {
                unsoldShareQuantity += lot.RemainingQuantity;
                unsoldShareCost += lot.RemainingQuantity * lot.OpenPrice;
            } 
            else if(remainingSharesToSell <= lot.RemainingQuantity)
            {
                // Sold cost
                totalSoldShareCost += remainingSharesToSell * lot.OpenPrice;
                // Register sold shares
                lot.RemainingQuantity -= remainingSharesToSell;

                // Remaining shares calculation
                unsoldShareQuantity += lot.RemainingQuantity;
                unsoldShareCost += lot.RemainingQuantity * lot.OpenPrice;

                remainingSharesToSell = 0;
            } 
            else
            {
                totalSoldShareCost += lot.RemainingQuantity * lot.OpenPrice;
                remainingSharesToSell -= lot.RemainingQuantity;
                lot.RemainingQuantity = 0;
            }
        }

        var totalSoldShareValue = quantity * price;

        return new SaleResult()
        {
            SoldShares = quantity,
            SoldSharesCostBasis = quantity == 0 ? 0 : decimal.Round(totalSoldShareCost / quantity, 2),
            SaleProfit = totalSoldShareValue - totalSoldShareCost,
            RemainingSharesCostBasis = unsoldShareQuantity == 0 ? 0 : decimal.Round(unsoldShareCost / unsoldShareQuantity, 2),
            RemainingShares = unsoldShareQuantity
        };
    }
}

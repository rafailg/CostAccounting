namespace TradingDataModel;

public class SaleResult
{
    public required int SoldShares { get; init; }
    public required decimal SoldSharesCostBasis { get; init; }
    public required int RemainingShares { get; init; }
    public required decimal RemainingSharesCostBasis { get; init; }
    public required decimal SaleProfit { get; init; }
}

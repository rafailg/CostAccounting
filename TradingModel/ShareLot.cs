namespace TradingDataModel;

public class ShareLot
{
    public decimal OpenPrice { get; init; }
    public DateTime OpenDateTime { get; init; } = DateTime.Now;
    public int InitialQuantity { get; init; }
    public int RemainingQuantity { get; set; }

    public ShareLot(decimal openPrice, int initialQuantity)
    {
        OpenPrice = openPrice;
        InitialQuantity = initialQuantity;
        RemainingQuantity = initialQuantity;
    }
}

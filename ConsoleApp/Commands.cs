using Controllers;
using DataAccess;
using TradingDataModel;

namespace ConsoleApp;

internal static class Commands
{
    public static void ShowStartInfo()
    {
        Console.WriteLine("Cost Accounting Application V1\n" +
            "Simple stock trades can be performed.\n" +
            "Cost basis is calculated using the FIFO method.");
    }

    public static void SellStock(IDataSource dataSource)
    {
        Console.WriteLine("Enter the amount of shares to be sold:");
        var shareCount = ConsoleUtilities.GetIntegerInput(0);
        if(shareCount == 0)
        {
            Console.WriteLine("Operation cancelled.");
            return;
        }

        if(shareCount > dataSource.GetRemainingStockQuantity())
        {
            Console.WriteLine($"Can't sell more than {dataSource.GetRemainingStockQuantity()}");
            return;
        }

        Console.WriteLine("Enter the sale price for the shares \n(enter a negative value to cancel the trade)");
        var price = ConsoleUtilities.GetDecimalInput();
        if(price < 0)
        {
            Console.WriteLine("Operation cancelled.");
            return;
        }

        var result = TradingController.SellStocksFifo(shareCount, price, dataSource);
        PrintSaleResults(result);
    }

    private static void PrintSaleResults(SaleResult saleResult)
    {
        Console.WriteLine("Sale complete.\n" +
            $"- {saleResult.SoldShares} shares sold.\n" +
            $"- Sold shares cost basis is ${saleResult.SoldSharesCostBasis}\n" +
            $"- {saleResult.RemainingShares} shares remaining.\n" +
            $"- Remaining share cost basis is ${saleResult.RemainingSharesCostBasis}\n" +
            $"- Total sale profit is ${saleResult.SaleProfit}");
    }

    public static void Overview(IDataSource dataSource)
    {
        Console.WriteLine($"There are {dataSource.GetRemainingStockQuantity()} stocks available for sale.");
    }

    public static void ShowHelp()
    {
        Console.WriteLine("The following commands are available:\n" +
            "'quit' to quit the application\n" +
            "'sell' to sell a number of stocks from the portfolio\n" +
            "'overview' to see the amount of stocks available for sale\n");
    }
}

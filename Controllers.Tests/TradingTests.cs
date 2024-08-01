using DataAccess;

namespace Controllers.Tests
{
    public class TradingTests
    {
        [Fact]
        public void TestCreateLot_InitialAndRemainingQuantityMatch()
        {
            var dataSource = DataSourceRetriever.NewInMemoryDataSource();
            TradingController.CreateLot(20, 40, dataSource);
            var lot = dataSource.GetLots().First();
            Assert.Equal(20, lot.InitialQuantity);
            Assert.Equal(20, lot.RemainingQuantity);
            Assert.Equal(40, lot.OpenPrice);
        }

        [Fact]
        public void TestCreateLot_LotsOrderedByCreationOrder()
        {
            var dataSource = DataSourceRetriever.NewInMemoryDataSource();
            TradingController.CreateLot(50, 40, dataSource);
            TradingController.CreateLot(20, 30, dataSource);
            TradingController.CreateLot(30, 20, dataSource);

            var firstLot = dataSource.GetLots()[0];
            var secondLot = dataSource.GetLots()[1];
            var thirdLot = dataSource.GetLots()[2];

            Assert.Equal(50, firstLot.InitialQuantity);
            Assert.Equal(20, secondLot.InitialQuantity);
            Assert.Equal(30, thirdLot.InitialQuantity);
        }

        [Fact]
        public void TestSellStocksFifo_ProfitIsCorrect()
        {
            var dataSource = DataSourceRetriever.NewInMemoryDataSource();
            TradingController.CreateLot(20, 50, dataSource);
            TradingController.CreateLot(10, 30, dataSource);
            var result = TradingController.SellStocksFifo(30, 50, dataSource);
            Assert.Equal(200, result.SaleProfit);
        }

        [Fact]
        public void TestSellStocksFifo_CostBasisCorrectForOneLot()
        {
            var dataSource = DataSourceRetriever.NewInMemoryDataSource();
            TradingController.CreateLot(20, 50, dataSource);
            var result = TradingController.SellStocksFifo(10, 50, dataSource);
            Assert.Equal(50, result.SoldSharesCostBasis);
            Assert.Equal(50, result.RemainingSharesCostBasis);
        }

        [Fact]
        public void TestSellStocksFifo_CostBasisCorrectForThreeLots()
        {
            var dataSource = DataSourceRetriever.NewInMemoryDataSource();
            TradingController.CreateLot(10, 10, dataSource);
            TradingController.CreateLot(10, 20, dataSource);
            TradingController.CreateLot(10, 50, dataSource);

            var results = TradingController.SellStocksFifo(25, 50, dataSource);
            Assert.Equal(22, results.SoldSharesCostBasis);
            Assert.Equal(50, results.RemainingSharesCostBasis);
        }

        [Fact]
        public void TestSellStocksFifo_CostBasisCorrectForDecimalValues()
        {
            var dataSource = DataSourceRetriever.NewInMemoryDataSource();
            TradingController.CreateLot(10, 22.56m, dataSource);
            TradingController.CreateLot(12, 36.90m, dataSource);
            TradingController.CreateLot(5, 11.32m, dataSource);

            var results = TradingController.SellStocksFifo(20, 31, dataSource);

            Assert.Equal(29.73m, results.SoldSharesCostBasis);
            Assert.Equal(18.63m, results.RemainingSharesCostBasis);
        }
    }
}
namespace DataAccess;

public static class DataSourceRetriever
{
    public static IDataSource NewInMemoryDataSource()
    {
        return new InMemoryDataSource();
    }
}

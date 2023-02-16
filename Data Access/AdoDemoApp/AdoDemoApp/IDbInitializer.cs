namespace AdoDemoApp;

public interface IDbInitializer
{
    internal void CreateTables();

    public void DropTables();

    public void SeedData();
}

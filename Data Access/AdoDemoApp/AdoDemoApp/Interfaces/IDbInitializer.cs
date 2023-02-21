namespace AdoDemoApp.Interfaces;

public interface IDbInitializer
{
    internal void CreateTables();

    public void DropTables();

    public void SeedData();
}
